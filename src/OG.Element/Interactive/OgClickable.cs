using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public class OgClickable<TElement, TScope>(string name, TScope scope, IOgTransform transform)
    : OgControl<TElement, TScope>(name, scope, transform), IOgClickable<TElement, TScope> where TElement : IOgElement where TScope : IOgTransformScope
{
    public event IOgClickable<TElement, TScope>.OgClickHandler? OnClicked;

    protected override void EndInteract(OgEvent reason)
    {
        base.EndInteract(reason);
        Click(reason);
    }

    protected virtual void Click(OgEvent reason) => OnClicked?.Invoke(this, reason);
}