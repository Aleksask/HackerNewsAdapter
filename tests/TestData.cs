using HackerNewsAdapter.Models;

namespace HackerNewsAdapter.Tests;

public class TestData
{
    public static List<HackerNewsItem> HackerNewsTestItems = new()
    {
        new HackerNewsItem
        {
            By = "user1",
            Descendants = 15,
            Id = 1,
            Score = 100,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 1",
            Type = "story",
            Url = "https://example.com/news1"
        },
        new HackerNewsItem
        {
            By = "user2",
            Descendants = 20,
            Id = 2,
            Score = 150,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 2",
            Type = "story",
            Url = "https://example.com/news2"
        },
        new HackerNewsItem
        {
            By = "user3",
            Descendants = 8,
            Id = 3,
            Score = 80,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 3",
            Type = "story",
            Url = "https://example.com/news3"
        },
        new HackerNewsItem
        {
            By = "user4",
            Descendants = 12,
            Id = 4,
            Score = 120,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 4",
            Type = "story",
            Url = "https://example.com/news4"
        },
        new HackerNewsItem
        {
            By = "user5",
            Descendants = 25,
            Id = 5,
            Score = 180,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 5",
            Type = "story",
            Url = "https://example.com/news5"
        },
        new HackerNewsItem
        {
            By = "user6",
            Descendants = 10,
            Id = 6,
            Score = 90,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 6",
            Type = "story",
            Url = "https://example.com/news6"
        },
        new HackerNewsItem
        {
            By = "user7",
            Descendants = 18,
            Id = 7,
            Score = 140,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 7",
            Type = "story",
            Url = "https://example.com/news7"
        },
        new HackerNewsItem
        {
            By = "user8",
            Descendants = 22,
            Id = 8,
            Score = 160,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 8",
            Type = "story",
            Url = "https://example.com/news8"
        },
        new HackerNewsItem
        {
            By = "user9",
            Descendants = 30,
            Id = 9,
            Score = 200,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 9",
            Type = "story",
            Url = "https://example.com/news9"
        },
        new HackerNewsItem
        {
            By = "user10",
            Descendants = 16,
            Id = 10,
            Score = 110,
            Time = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Title = "Sample News 10",
            Type = "story",
            Url = "https://example.com/news10"
        }
    };
}