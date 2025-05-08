using DK.Matching;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Element;
public class OgElement : IOgElement
{
    private readonly DkCacheMatcherProvider<IOgTransformer, IOgTransformerOption> m_DkMatchProvider;
    private readonly IOgEventHandlerProvider                                      m_Provider;
    public OgElement(string name, IOgEventHandlerProvider provider)
    {
        m_Provider         = provider;
        Name               = name;
        ElementRect        = Rect.zero;
        TransformerOptions = [];
        m_DkMatchProvider  = new(TransformerOptions);
    }
    public bool                              IsActive           { get; set; }
    public Rect                              ElementRect        { get; private set; }
    public string                            Name               { get; }
    public IEnumerable<IOgTransformerOption> TransformerOptions { get; }
    public bool ProcessEvent(IOgEvent reason) => IsActive && m_Provider.Handle(reason);
    public void ProcessTransformers(IEnumerable<IOgTransformer> transformers, Rect parentRect, Rect lastRect)
    {
        ElementRect = Rect.zero;
        foreach(IOgTransformer transformer in transformers)
        {
            if(!m_DkMatchProvider.TryGetMatcher(transformer, out IOgTransformerOption option)) continue;
            ElementRect = transformer.Transform(ElementRect, parentRect, lastRect, option);
        }
    }
}