using OG.Transformer.Abstraction;
using OG.Transformer.Transformers;
using System.Collections.Generic;
namespace OG.Transformer.Options;
public class OgScriptableOption(string name) : IOgTransformerOption
{
    private readonly Dictionary<string, object> m_Properties = [];
    
    public object? GetProperty(string propertyName) => 
        m_Properties.TryGetValue(propertyName, out object? property) ? property : null;
    public T? GetProperty<T>(string propertyName) => (T?)GetProperty(propertyName);
    public void SetProperty(string propertyName, object value)
    {
        if(m_Properties.ContainsKey(propertyName))
            m_Properties[propertyName] = value;
        m_Properties.Add(propertyName, value);
    }
    public bool CanHandle(IOgTransformer value) => value is OgScriptableTransformer transformer && transformer.Name == name;
}