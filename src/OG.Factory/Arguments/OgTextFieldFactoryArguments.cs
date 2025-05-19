using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.TextController.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFieldFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkFieldProvider<string> valueProvider, IOgTextController textController, Font font)
    : OgValueFactoryArguments<string>(name, rectGetProvider, eventProvider, valueProvider)
{
    public IOgTextController TextController => textController;
    public Font              Font           => font;
}