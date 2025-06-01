using DK.Getting.Abstraction.Generic;
namespace EH.Builder.DataTypes;
public interface IEhDropdown : IEhContainer
{
    void AddItem(IDkGetProvider<string> name);
}