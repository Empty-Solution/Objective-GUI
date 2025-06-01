using OG.Element.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhContainer : IEhElement
{
    bool LinkChild(IOgElement element);
    bool UnlinkChild(IOgElement element);
}