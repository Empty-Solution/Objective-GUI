using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using System.Collections.Generic;
namespace EH.Builder.Wrapping.DataTypes;
public class EhTab(IEnumerable<IOgContainer<IOgElement>> groups)
{
    public IEnumerable<IOgContainer<IOgElement>> Groups { get; set; } = groups;
}