using EH.Builder.Interactive.ElementBuilders;
using EH.Builder.Option;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhWindowBuilder
{
    private readonly EhBackgroundBuilder        m_BackgroundBuilder = new();
    private readonly EhInternalDraggableBuilder m_DraggableBuilder  = new();
    private readonly EhWindowOption             m_Option            = new();
    public IOgContainer<IOgElement> Build(string name) => Build(name, m_Option);
    private IOgContainer<IOgElement> Build(string name, EhWindowOption option)
    {
        IOgDraggableElement<IOgElement> window = m_DraggableBuilder.Build(name, new OgScriptableBuilderProcess<OgDraggableBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgMinSizeTransformerOption(option.WindowWidth, option.WindowHeight));
        }));
        window.Add(m_BackgroundBuilder.Build($"{name}Background", option.BackgroundColorProperty, option.WindowWidth, option.WindowHeight, 0, 0,
            option.WindowBorder, option.m_BackgroundColorBindings));
        return window;
    }
}