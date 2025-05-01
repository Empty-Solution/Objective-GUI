using OG.Element.Abstraction;
namespace OG.Layout.Abstraction;
public interface IOgLayoutTool<TElement> where TElement : IOgElement
{
    void ResetLayout();
    void ProcessElement(TElement element);
}