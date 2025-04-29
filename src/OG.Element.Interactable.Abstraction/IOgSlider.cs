using DK.Common.DataTypes.Abstraction;
using OG.Element.Abstraction;
using OG.Element.View.Abstraction;

namespace OG.Element.Interactable.Abstraction;

public interface IOgSlider<TElement> : IOgValueView<TElement, float> where TElement : IOgElement
{
    IDkRange<float>? Range { get; set; }
    float ScrollStep { get; set; }
}