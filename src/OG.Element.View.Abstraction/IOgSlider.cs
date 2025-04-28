using OG.Element.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgSlider<TElement> : IOgValueView<TElement, float> where TElement : IOgElement;