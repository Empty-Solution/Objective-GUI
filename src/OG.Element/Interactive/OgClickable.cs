using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public class OgClickable<TElement, TScope>(string name, TScope rootScope, IOgTransform transform)
    : OgControl<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgClickHandler(OgClickable<TElement, TScope> instance, OgEvent reason);

    public event OgClickHandler? OnClicked;

    protected override void EndInteract(OgEvent reason)
    {
        base.EndInteract(reason);
        Click(reason);
    }

    protected virtual void Click(OgEvent reason) => OnClicked?.Invoke(this, reason);
}