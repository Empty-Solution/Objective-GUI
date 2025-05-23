using DK.Observing.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Abstraction;
public interface IEhSliderBuilder
{
    IOgContainer<IOgElement> Build(string name, float initial, float min, float max, string textFormat, int round = 0, DkObserver<float>? observer = null);
}