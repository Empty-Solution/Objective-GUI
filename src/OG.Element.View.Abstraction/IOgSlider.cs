using OG.Element.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgSlider<TElement, TValue> : IOgValueView<TElement, TValue> where TElement : IOgElement;