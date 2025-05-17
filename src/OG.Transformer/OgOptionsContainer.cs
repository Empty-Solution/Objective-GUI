using OG.Transformer.Abstraction;
using System.Collections.Generic;
namespace OG.Transformer;
public class OgOptionsContainer : IOgOptionsContainer
{
    private readonly Dictionary<string, object> m_Options = new();
    public IOgOptionsContainer SetOption<TValue>(string name, TValue value)
    {
        m_Options[name] = value!;
        return this;
    }
    public IOgOptionsContainer RemoveOption(string name)
    {
        m_Options.Remove(name);
        return this;
    }
    public bool TryGetValue<TValue>(string name, out TValue value)
    {
        value = default!;
        if(!m_Options.TryGetValue(name, out object? option)) return false;
        value = (TValue)option;
        return true;
    }
}