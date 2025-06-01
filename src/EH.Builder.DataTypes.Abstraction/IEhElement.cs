using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhElement
{
    bool LinkSelf(IOgContainer<IOgElement> container);
    bool UnlinkSelf(IOgContainer<IOgElement> container);
}