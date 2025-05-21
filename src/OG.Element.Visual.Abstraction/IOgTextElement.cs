using OG.Graphics.Abstraction;
namespace OG.Element.Visual.Abstraction;
public interface IOgTextElement : IOgVisualElement
{
    string                  Text    { get; set; }
    IOgTextGraphicsContext? Context { get; }
}