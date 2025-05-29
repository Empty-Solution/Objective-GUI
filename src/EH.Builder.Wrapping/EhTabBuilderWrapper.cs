using EH.Builder.Interactive;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using EH.Builder.Wrapping.DataTypes;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Wrapping;
public class EhTabBuilderWrapper
{
    private readonly EhTabBuilder m_TabBuilder;
    public EhTabBuilderWrapper(EhConfigProvider configProvider, IEhVisualProvider visualProvider)
    {
        EhBackgroundBuilder backgroundBuilder = new();
        EhContainerBuilder  containerBuilder  = new();
        EhTextBuilder       textBuilder       = new(visualProvider);
        m_TabBuilder = new(configProvider, backgroundBuilder, containerBuilder, textBuilder);
    }
    public EhTab BuildTab(string leftContainerName, string rightContainerName, IOgContainer<IOgElement> sourceTab)
    {
        m_TabBuilder.Build(leftContainerName, rightContainerName, sourceTab, out IOgContainer<IOgElement> leftContainer,
            out IOgContainer<IOgElement> rightContainer);
        return new([leftContainer, rightContainer]);
    }
}