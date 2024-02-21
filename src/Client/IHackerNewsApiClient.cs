using HackerNewsAdapter.Models;
using Refit;

namespace HackerNewsAdapter.Client;

public interface IHackerNewsApiClient
{
    [Get("/v0/beststories.json")]
    Task<int[]> GetBestStoryIdsAsync();

    [Get("/v0/item/{itemId}.json")]
    Task<HackerNewsItem> GetStoryDetailsAsync(int itemId);
}