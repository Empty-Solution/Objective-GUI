using DK.Property.Observing.Abstraction.Generic;
using OG.DataTypes.ElementState;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgStatedElement : IOgElement
{
    IDkObservableProperty<EOgElementState>? State { get; }
}