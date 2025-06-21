using OG.Element.Abstraction;
using OG.Graphics.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgTextField<TElement> : IOgFocusableElement<TElement, string> where TElement : IOgElement
{
    IOgTextGraphicsContext? Context { get; }
}