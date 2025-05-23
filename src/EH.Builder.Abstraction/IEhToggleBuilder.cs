using DK.Observing.Generic;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
namespace EH.Builder.Abstraction;
public interface IEhToggleBuilder
{
    IOgContainer<IOgElement> Build(string name, bool initial, DkObserver<bool>? observer = null);
}