using DK.Matching;
using OG.Layout.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Layout;
public class OgLayout : IOgLayout
{
    private readonly DkTypeCacheMatcherProvider<IOgTransformerOption, IOgTransformer> m_MatcherProvider;
    protected readonly IEnumerable<IOgTransformer> m_Transformers;
    public OgLayout(IEnumerable<IOgTransformer> transformers)
    {
        m_Transformers = transformers.OrderBy(t => t.Order);
        m_MatcherProvider = new(m_Transformers);
    }
    public int RemainingLayoutItems { get; set; }
    public Rect LastLayoutRect { get; set; }
    public Rect ParentRect { get; set; }
    public Rect ProcessLayout(Rect rect, IOgOptionsContainer container)
    {
        var parentRect = ParentRect;
        int remaining = RemainingLayoutItems;
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach(var option in container.Options)
        {
            if(!m_MatcherProvider.TryGetMatcher(option, out var transformer)) continue;
            rect = transformer.Transform(rect, parentRect, LastLayoutRect, remaining, option);
        }
        return rect;
    }
}