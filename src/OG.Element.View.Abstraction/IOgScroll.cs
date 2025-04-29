using OG.DataTypes.Vector;
using OG.Element.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgScroll<TElement> : IOgValueView<TElement, OgVector2> where TElement : IOgElement;