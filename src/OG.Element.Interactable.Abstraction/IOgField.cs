using OG.Element.Abstraction;
using OG.Element.Control.Focusable.Abstraction;
using OG.Event.Abstraction.Handlers;
namespace OG.Element.Interactable.Abstraction;
public interface IOgField<TElement> : IOgFocusableControl<TElement, string>, IOgKeyDownEventHandler where TElement : IOgElement;