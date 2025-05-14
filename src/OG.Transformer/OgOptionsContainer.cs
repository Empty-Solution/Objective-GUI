using DK.Matching;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Transformer;
public class OgOptionsContainer : IOgOptionsContainer
{
    private readonly DkCacheMatcherProvider<IOgTransformer, IOgTransformerOption> m_MatchProvider;
    private readonly List<IOgTransformerOption>                                   m_Options;
    public OgOptionsContainer()
    {
        m_Options       = [];
        m_MatchProvider = new(m_Options);
    }
    public bool TryGetOption(IOgTransformer transformer, out IOgTransformerOption option) => m_MatchProvider.TryGetMatcher(transformer, out option);
    public void AddOption(IOgTransformerOption option) => m_Options.Add(option);
    public bool RemoveOption(IOgTransformerOption option) => m_Options.Remove(option);
}