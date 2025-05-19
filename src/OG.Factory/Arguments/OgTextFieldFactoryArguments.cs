using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.TextController.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextFieldFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IDkFieldProvider<string> valueProvider,
    IOgTextController textController, Font font) : OgValueFactoryArguments<string>(name, rectGetProvider, valueProvider)
{
    public IOgTextController TextController => textController;
    public Font              Font           => font;
}