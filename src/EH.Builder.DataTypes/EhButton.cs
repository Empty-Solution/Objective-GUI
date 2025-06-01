using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
namespace EH.Builder.DataTypes;
public class EhButton(IOgInteractableElement<IOgVisualElement> button, IOgOptionsContainer optionsContainer)
    : EhElement(button, optionsContainer), IEhButton
{
}