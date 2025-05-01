using DK.DataTypes.Abstraction;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.View.Abstraction;
namespace OG.Element.Interactable.Abstraction;
public interface IOgVector<TElement> : IOgValueView<TElement, OgVector2> where TElement : IOgElement
{
    IDkReadOnlyRange<OgVector2>? Range { get; set; }
}