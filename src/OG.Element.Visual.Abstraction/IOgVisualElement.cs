using OG.Element.Abstraction;
namespace OG.Element.Visual.Abstraction;
public interface IOgVisualElement : IOgElement
{
    float ZOrder { get; set; }
}