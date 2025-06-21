using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Transformer.Options;
public class OgOptionsContainer : IOgOptionsContainer
{
    private readonly List<IOgTransformerOption> m_Options = [];
    public IEnumerable<IOgTransformerOption> Options => m_Options;
    public IOgOptionsContainer SetOption(IOgTransformerOption option)
    {
        if(m_Options.IndexOf(option) == -1) m_Options.Add(option);
        return this;
    }
    public IOgOptionsContainer RemoveOption(IOgTransformerOption option)
    {
        if(m_Options.IndexOf(option) == -1) return this;
        _ = m_Options.Remove(option);
        return this;
    }
}