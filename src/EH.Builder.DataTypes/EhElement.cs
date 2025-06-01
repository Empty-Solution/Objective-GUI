using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public class EhElement(IOgElement source) : IEhElement
{
    public virtual bool LinkSelf(IOgContainer<IOgElement> container) => container.Add(source);
    public virtual bool UnlinkSelf(IOgContainer<IOgElement> container) => container.Remove(source);
}