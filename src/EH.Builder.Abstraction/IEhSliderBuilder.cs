using DK.Observing.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Abstraction;
public interface IEhSliderBuilder
{
    IOgContainer<IOgElement> Build(string name, float initial, float min, float max, string textFormat, bool roundToInt,
        DkObserver<float>? observer = null);
}