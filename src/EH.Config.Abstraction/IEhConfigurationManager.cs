using EH.Builder.DataTypes;
namespace EH.Config.Abstraction;
public interface IEhConfigurationManager
{
    IEhProperty<TValue> GetProperty<TValue>(string name, TValue initial = default!);
    bool Load(string dbPath);
    bool Save(byte[] data);
}