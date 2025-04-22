using DK.Scoping.Extensions;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element;

public abstract class OgElement<TScope>(string name, TScope scope, IOgTransform transform) : IOgScopedElement<TScope> where TScope : IOgTransformScope
{
    public string Name => name;

    public bool Active { get; set; } = true;

    public IOgTransform Transform => transform;

    public TScope Scope => scope;

    public void OnGUI(OgEvent reason)
    {
        if(!Active) return;
        Scope.Focus(Transform);
        using(Scope.OpenContext()) InternalOnGUI(reason);
    }

    protected abstract void InternalOnGUI(OgEvent reason);
}