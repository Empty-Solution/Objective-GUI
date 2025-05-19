using OG.Layout.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace OG.Layout;
public class OgLayout : IOgLayout
{
    protected readonly IEnumerable<IOgTransformer> m_Transformers;
    protected          Rect                        m_LastLayoutRect;
    public OgLayout(IEnumerable<IOgTransformer> transformers) => m_Transformers = transformers.OrderBy(t => t.Order);
    public int  RemainingLayoutItems { get; set; }
    public Rect ParentRect           { get; set; }
    public Rect ProcessLayout(Rect rect, IOgOptionsContainer container)
    {
        Rect parentRect = ParentRect;
        int  remaining  = RemainingLayoutItems;
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach(IOgTransformerOption option in container.Options)
        {
            foreach(IOgTransformer transformer in m_Transformers)
            {
                if(!transformer.CanHandle(option)) continue;
                rect = transformer.Transform(rect, parentRect, m_LastLayoutRect, remaining, option);
            }
        }
        m_LastLayoutRect = rect;
        return rect;
    }
}