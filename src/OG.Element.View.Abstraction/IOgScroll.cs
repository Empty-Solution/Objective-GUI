using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using System.Numerics;

namespace OG.Element.View.Abstraction;

public interface IOgScroll<TElement> : IOgValueView<TElement, OgVector2> where TElement : IOgElement;