using DK.Matching;
using DK.Matching.Abstraction;
using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Transformer;
public class OgOptionsContainer : IOgOptionsContainer
{
    private readonly DkCacheMatcherProvider<IOgTransformer, IOgTransformerOption> m_DkMatchProvider;
    private readonly List<IOgTransformerOption>                                   m_Options;
    public OgOptionsContainer()
    {
        m_Options         = [];
        m_DkMatchProvider = new(m_Options);
    }
    public IDkMatcherProvider<IOgTransformer, IOgTransformerOption> Provider => m_DkMatchProvider;
    public void AddOption(IOgTransformerOption option) => m_Options.Add(option);
    public bool RemoveOption(IOgTransformerOption option) => m_Options.Remove(option);
}