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
    private readonly List<IOgTransformerOption>                                   m_Options;
    private readonly IOgEventHandlerProvider                                      m_Provider;
    public OgElement(string name, IOgEventHandlerProvider provider)
    {
        m_Provider        = provider;
        Name              = name;
        ElementRect       = Rect.zero;
        m_Options         = [];
        m_DkMatchProvider = new(TransformerOptions);
    }
    public virtual bool                              IsActive           { get; set; }
    public         Rect                              ElementRect        { get; set; }
    public         string                            Name               { get; }
    public         IEnumerable<IOgTransformerOption> TransformerOptions => m_Options;
    public void AddOption(IOgTransformerOption option) => m_Options.Add(option);
    public bool RemoveOption(IOgTransformerOption option) => m_Options.Remove(option);
    public bool ProcessEvent(IOgEvent reason) => IsActive && m_Provider.Handle(reason);
    public bool TryGetOption(IOgTransformer transformer, out IOgTransformerOption option) => m_DkMatchProvider.TryGetMatcher(transformer, out option);
}