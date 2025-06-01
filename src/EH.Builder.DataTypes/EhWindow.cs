using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public class EhWindow(IOgContainer<IOgElement> window, OgAnimationRectGetter<OgTransformerRectGetter> tabSeparatorSelectorGetter,
    IOgContainer<IOgElement> toolbarContainer, IOgContainer<IOgElement> tabContainer, IOgContainer<IOgElement> tabButtonsContainer,
    IOgOptionsContainer optionsContainer) : EhContainer(window, optionsContainer), IEhWindow
{
    public OgAnimationRectGetter<OgTransformerRectGetter> TabSeparatorSelectorGetter => tabSeparatorSelectorGetter;
    public IOgContainer<IOgElement>                       TabContainer               => tabContainer;
    public IOgContainer<IOgElement>                       ToolbarContainer           => toolbarContainer;
    public bool LinkToolBarChild(IOgElement child) => toolbarContainer.Add(child);
    public bool UnlinkToolBarChild(IOgElement child) => toolbarContainer.Remove(child);
    public bool LinkTabButton(IOgElement tabButton) => tabButtonsContainer.Add(tabButton);
    public bool UnlinkTabButton(IOgElement tabButton) => tabButtonsContainer.Remove(tabButton);
    public bool LinkTab(IEhTab tab) => tab.LinkSelf(tabContainer);
    public bool UnlinkTab(IEhTab tab) => tab.UnlinkSelf(tabContainer);
}