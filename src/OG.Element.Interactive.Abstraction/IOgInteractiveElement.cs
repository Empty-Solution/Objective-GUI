using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgInteractiveElement : IOgElement
{
    bool IsInteracting { get; }
}