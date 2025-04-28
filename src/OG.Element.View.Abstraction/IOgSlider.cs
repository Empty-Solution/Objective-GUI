using DK.Common.DataTypes.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgSlider<TElement> : IOgValueView<TElement, float> where TElement : IOgElement
{
    IDkRange<float>? Range { get; set; }
    float ScrollStep { get; set; }
}