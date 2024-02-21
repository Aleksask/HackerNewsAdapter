using System.Globalization;
using Mapster;

namespace HackerNewsAdapter.Models;

public class MapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<HackerNewsItem, HackerNewsAdapterItem>()
            .Map(d => d.Uri, s => s.Url)
            .Map(d => d.CommentCount, s => s.Descendants)
            .Map(d => d.PostedBy, s => s.By)
            .Map(d => d.Time, s => DateTimeOffset.FromUnixTimeSeconds(s.Time));
    }
}