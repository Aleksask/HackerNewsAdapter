using System.Reflection;
using FluentAssertions;
using HackerNewsAdapter.Api;
using HackerNewsAdapter.Client;
using HackerNewsAdapter.Models;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace HackerNewsAdapter.Tests;

public class HackerNewsAdapterApiTests
{
    private IHackerNewsApiClient _hackerNewsClient;
    private IMemoryCache _memoryCache;

    public HackerNewsAdapterApiTests()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _hackerNewsClient = Substitute.For<IHackerNewsApiClient>();
        _hackerNewsClient.GetBestStoryIdsAsync().Returns(TestData.HackerNewsTestItems.Select(x => x.Id).ToArray());
        _hackerNewsClient.GetStoryDetailsAsync(Arg.Any<int>())
            .Returns(arg => TestData.HackerNewsTestItems.First(i => i.Id == (int)arg[0]));
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(HackerNewsAdapterItem))!);
    }

    [Fact]
    public async Task GivenNoLimitToTheStoriesShouldReturnAll ()
    {
        var stories = await HackerNewsAdapterApi.GetOrderedBestHackerNewsStoriesAsync(_hackerNewsClient, _memoryCache);
        stories.Length.Should().Be(TestData.HackerNewsTestItems.Count);
    }
    [Fact]
    public async Task GivenLimitShouldReturnSpecifiedNumberOfArticles ()
    {
        var stories = await HackerNewsAdapterApi.GetOrderedBestHackerNewsStoriesAsync(_hackerNewsClient, _memoryCache, 2);
        stories.Length.Should().Be(2);
    }
    [Fact]
    public async Task ArticlesAreReturnedInSortedOrder()
    {
        var stories = await HackerNewsAdapterApi.GetOrderedBestHackerNewsStoriesAsync(_hackerNewsClient, _memoryCache);
        for (var i = 0; i < stories.Length - 1; i++)
        {
            stories[i].Score.Should().BeGreaterThan(stories[i + 1].Score);
        }
    }
    [Fact]
    public async Task WhenArticleIsInCacheDoNotInvokeHackerNewsApi()
    {
        foreach (var hackerNewsTestItem in TestData.HackerNewsTestItems)
        {
            _memoryCache.Set(hackerNewsTestItem.Id, hackerNewsTestItem.Adapt<HackerNewsAdapterItem>(), TimeSpan.FromSeconds(10));
        }
        await HackerNewsAdapterApi.GetOrderedBestHackerNewsStoriesAsync(_hackerNewsClient, _memoryCache, 2);
        await _hackerNewsClient.Received(Quantity.None()).GetStoryDetailsAsync(Arg.Any<int>());
    }

}