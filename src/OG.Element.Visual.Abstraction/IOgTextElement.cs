using DK.Getting.Abstraction.Generic;
using OG.Graphics.Abstraction;
namespace OG.Element.Visual.Abstraction;
public interface IOgTextElement : IOgVisualElement
{
    IDkGetProvider<string>? Text    { get; set; }
    IOgTextGraphicsContext? Context { get; }
}