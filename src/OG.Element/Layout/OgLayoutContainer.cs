using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element;

public class OgLayoutContainer<TElement, TScope>(string name, IOgLayout<TElement> layout, TScope scope, IOgTransform transform)
    : OgContainer<TElement, TScope>(name, scope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override void InternalOnGUI(OgEvent reason)
    {
        layout.Open();
        base.InternalOnGUI(reason);
        layout.Close();
    }

    protected override void ProcessChild(OgEvent reason, TElement child)
    {
        layout.ProcessItem(child);
        base.ProcessChild(reason, child);
    }
}