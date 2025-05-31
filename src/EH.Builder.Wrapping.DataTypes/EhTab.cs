using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Wrapping.DataTypes;
public class EhTab(IEnumerable<IOgContainer<IOgElement>> groups, IOgContainer<IOgElement> sourceContainer)
{
    public IEnumerable<IOgContainer<IOgElement>> Groups          { get; } = groups;
    public IOgContainer<IOgElement>              SourceContainer { get; } = sourceContainer;
}