using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
namespace EH.Builder.DataTypes;
public class EhButton(IOgInteractableElement<IOgVisualElement> button) : EhElement(button), IEhButton
{
}