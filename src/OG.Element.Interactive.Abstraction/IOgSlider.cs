using DK.DataTypes.Abstraction;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgSlider<TElement> : IOgInteractableValueElement<TElement, float> where TElement : IOgElement
{
    IDkReadOnlyRange<float>? Range { get; }
}