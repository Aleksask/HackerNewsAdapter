using System.ComponentModel;
using System.Text.Json.Serialization;

namespace HackerNewsAdapter.Models;

public class HackerNewsItem
{
    public string By { get; set; }
    public int Descendants { get; set; } // total comments
    public int Id { get; set; }
    public int Score { get; set; }
    public long Time { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
}