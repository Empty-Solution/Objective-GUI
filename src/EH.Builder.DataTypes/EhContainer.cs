using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public class EhContainer(IOgContainer<IOgElement> source) : EhElement(source), IEhContainer
{
    public virtual bool LinkChild(IOgElement element) => source.Add(element);
    public virtual bool UnlinkChild(IOgElement element) => source.Remove(element);
}