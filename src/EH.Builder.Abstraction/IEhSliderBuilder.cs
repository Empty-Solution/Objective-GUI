using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Abstraction;
public interface IEhSliderBuilder
{
    IOgContainer<IOgElement> Build(string name, IDkObservableProperty<float> value, float min, float max, string textFormat, int round = 0);
}