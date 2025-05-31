using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Wrapping.DataTypes;
public class EhTab(IEnumerable<IOgContainer<IOgElement>> groups)
{
    public IEnumerable<IOgContainer<IOgElement>> Groups { get; set; } = groups;
}
public class EhSourceTab(IEnumerable<EhTab> tabs, IOgContainer<IOgElement> sourceContainer, IOgContainer<IOgElement> toolbar,
    IOgToggle<IOgVisualElement> button)
{
    public IEnumerable<EhTab>          Tabs            { get; set; } = tabs;
    public IOgContainer<IOgElement>    Toolbar         { get; set; } = toolbar;
    public IOgContainer<IOgElement>    SourceContainer { get; set; } = sourceContainer;
    public IOgToggle<IOgVisualElement> Button          { get; set; } = button;
}