using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.DataTypes;
public interface IEhWindow : IEhContainer
{
    OgAnimationRectGetter<OgTransformerRectGetter> TabSeparatorSelectorGetter { get; }
    IOgContainer<IOgElement>                       TabContainer               { get; }
    IOgContainer<IOgElement>                       ToolbarContainer           { get; }
    bool LinkToolBarChild(IOgElement child);
    bool UnlinkToolBarChild(IOgElement child);
    bool LinkTabButton(IOgElement tabButton);
    bool UnlinkTabButton(IOgElement tabButton);
    bool LinkTab(IEhTab tab);
    bool UnlinkTab(IEhTab tab);
}