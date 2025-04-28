using DK.Common.DataTypes.Abstraction;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgVector<TElement> : IOgValueView<TElement, OgVector2> where TElement : IOgElement
{
    IDkRange<OgVector2>? Range { get; set; }
}