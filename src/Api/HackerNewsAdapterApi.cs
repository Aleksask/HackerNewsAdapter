using System.Collections.Concurrent;
using HackerNewsAdapter.Client;
using HackerNewsAdapter.Models;
using Mapster;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsAdapter.Api;

public static class HackerNewsAdapterApi
{
    public static void Register(WebApplication app)
    {
        app.MapGet("/api/best", async (IHackerNewsApiClient hackerNewClient,
                IMemoryCache cache,
                ILogger<Program> logger,
                int? n = null) =>
            {
                try
                {
                    var bestStories = await GetOrderedBestHackerNewsStoriesAsync(hackerNewClient, cache, n);
                    return Results.Ok(bestStories);
                }
                catch (Exception e)
                {
                    const string errorTitle = "Encountered error while pulling the hacker news articles";
                    logger.LogError(e, errorTitle);
                    return Results.Problem(statusCode: 500, title: errorTitle);
                }

            })
            .WithName("BestStories")
            .WithOpenApi()
            .RequireRateLimiting(Constants.RateLimiterPolicy);
    }

    public static async Task<HackerNewsAdapterItem[]> GetOrderedBestHackerNewsStoriesAsync(
        IHackerNewsApiClient hackerNewClient,
        IMemoryCache cache,
        int? numberOfStories = null)
    {
        // Since there's no api to limit the number of stories fetched, we're pulling in all the available stories
        var bestStoryIds = await hackerNewClient.GetBestStoryIdsAsync();
        var sortedStoryList = new ConcurrentBag<HackerNewsAdapterItem>();

        // Due to the slow cold start of the API we're processing story
        foreach (var storyIds in bestStoryIds.Chunk(10))
        {
            await Task.WhenAll(
                storyIds.Select(async storyId =>
                {
                    if (cache.TryGetValue<HackerNewsAdapterItem>(storyId, out var story) && story != null)
                    {
                        sortedStoryList.Add(story);
                        return;
                    }

                    var storyDetails = await hackerNewClient.GetStoryDetailsAsync(storyId);

                    var mappedStory = storyDetails.Adapt<HackerNewsAdapterItem>();

                    var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(2));
                    cache.Set(storyId, mappedStory, cacheOptions);
                    sortedStoryList.Add(mappedStory);
                }));
        }

        return numberOfStories != null
            ? sortedStoryList.OrderByDescending(x => x.Score).Take(numberOfStories.Value).ToArray()
            : sortedStoryList.OrderByDescending(x => x.Score).ToArray();
    }
}