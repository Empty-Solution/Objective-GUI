using OG.Element.Abstraction;
namespace OG.Element.Visual.Abstraction;
public interface IOgVisualElement : IOgElement
{
    public int ZOrder { get; set; }
}