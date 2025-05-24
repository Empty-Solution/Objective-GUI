using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Abstraction;
public interface IEhToggleBuilder
{
    IOgContainer<IOgElement> Build(string name, IDkObservableProperty<bool> value);
}