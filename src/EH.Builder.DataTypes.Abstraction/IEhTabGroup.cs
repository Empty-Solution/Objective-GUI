using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhTabGroup : IEhContainer
{
    IOgContainer<IOgElement> GroupContainer { get; }
}