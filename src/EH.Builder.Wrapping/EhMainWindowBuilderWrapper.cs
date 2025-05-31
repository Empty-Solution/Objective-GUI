using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
using OG.DataKit.Animation;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Wrapping;
public class EhMainWindowBuilderWrapper
{
    private readonly IOgContainer<IOgElement> m_Window;
    public EhMainWindowBuilderWrapper(IEhConfigProvider configProvider, IEhVisualProvider visualProvider)
    {
        EhBaseBackgroundBuilder backgroundBuilder = new();
        EhContainerBuilder      containerBuilder  = new();
        EhBaseDraggableBuilder  draggableBuilder  = new();
        EhMainWindowBuilder     mainWindowBuilder = new(configProvider, backgroundBuilder, containerBuilder, draggableBuilder);
        m_Window = mainWindowBuilder.Build(visualProvider.LogoTexture, out IOgContainer<IOgElement> tabButtons, out IOgContainer<IOgElement> tabContainer,
            out IOgContainer<IOgElement> toolBar, out OgAnimationRectGetter<OgTransformerRectGetter> tabSeparator);
        ToolBar      = toolBar;
        TabButtons   = tabButtons;
        TabContainer = tabContainer;
        TabSeparator = tabSeparator;
    }
    public IOgContainer<IOgElement>                        TabButtons   { get; }
    public IOgContainer<IOgElement>                        TabContainer { get; }
    public IOgContainer<IOgElement>                        ToolBar      { get; }
    public OgAnimationRectGetter<OgTransformerRectGetter>? TabSeparator { get; }
    public IOgContainer<IOgElement> GetMainWindow() => m_Window;
}