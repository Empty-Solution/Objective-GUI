using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public class OgFocusableControl<TElement, TScope, TValue>(string name, TScope scope, IOgTransform transform, TValue value) :
    OgValueView<TElement, TScope, TValue>(name, scope, transform, value) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgFocusHandler(OgFocusableControl<TElement, TScope, TValue> instance, OgEvent reason);

    public delegate void OgFocusLostHandler(OgFocusableControl<TElement, TScope, TValue> instance, OgEvent reason);

    public bool IsFocused { get; private set; }
    public bool IsFocusInteracting => IsInteracting && IsFocused;
    public event OgFocusHandler? OnFocus;
    public event OgFocusLostHandler? OnFocusLost;

    protected override void HandleMouseDown(OgEvent reason)
    {
        base.HandleMouseDown(reason);
        UpdateFocusState(reason);
    }

    protected virtual void Focus(OgEvent reason) => OnFocus?.Invoke(this, reason);

    protected virtual void LostFocus(OgEvent reason) => OnFocusLost?.Invoke(this, reason);

    private void UpdateFocusState(OgEvent reason)
    {
        if(IsFocused == IsHovered) return;

        IsFocused = IsHovered;
        InternalInvokeFocusEvent(IsHovered, reason);
    }

    private void InternalInvokeFocusEvent(bool isFocused, OgEvent reason)
    {
        if(isFocused)
        {
            Focus(reason);
            return;
        }

        LostFocus(reason);
    }
}