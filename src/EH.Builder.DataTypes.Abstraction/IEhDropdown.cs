using DK.Getting.Abstraction.Generic;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhDropdown : IEhContainer
{
    IOgOptionsContainer OptionsContainer { get; }
    void AddItem(IDkGetProvider<string> name);
}