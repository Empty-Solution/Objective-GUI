using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element;

public abstract class OgElement<TScope>(string name, TScope scope, IOgTransform transform) : IOgScopedElement<TScope> where TScope : IOgTransformScope
{
    public string Name => name;

    public IOgTransform Transform => transform;

    public TScope Scope => scope;

    public void OnGUI(OgEvent reason)
    {
        Scope.Focus(Transform);
        OpenScope(scope);
        InternalOnGUI(reason);
        CloseScope(scope);
    }

    protected virtual void OpenScope(TScope targetScope)
    {
        targetScope.Focus(Transform);
        targetScope.Open();
    }

    protected virtual void CloseScope(TScope targetScope) => targetScope.Close();

    protected abstract void InternalOnGUI(OgEvent reason);
}