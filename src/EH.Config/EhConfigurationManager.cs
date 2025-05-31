using DK.Observing.Generic;
using EH.Builder.DataTypes;
using EH.Config.Abstraction;
using System.Collections.Generic;
namespace EH.Config;
public abstract class EhBaseConfigurationManager : IEhConfigurationManager
{
    private readonly Dictionary<string, IEhProperty> m_Properties = [];
    public IEhProperty<TValue> GetProperty<TValue>(string name, TValue initial = default!)
    {
        if(m_Properties.TryGetValue(name, out IEhProperty? property) && property is IEhProperty<TValue> typedProperty) return typedProperty;
        EhProperty<TValue> newProperty = new(new DkObservable<TValue>([]), initial);
        m_Properties.Add(name, newProperty);
        return newProperty;
    }
    public abstract bool Load(string dbPath);
    public abstract bool Save(byte[] data);
}