#region

using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.View.Abstraction;

#endregion

namespace OG.Element.Interactable.Abstraction;

public interface IOgScroll<TElement> : IOgValueView<TElement, OgVector2> where TElement : IOgElement;