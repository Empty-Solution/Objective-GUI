using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgModalInteractable<TElement> : IOgInteractableElement<TElement> where TElement : IOgElement
{
    public bool ShouldProcess { get; set; }
}