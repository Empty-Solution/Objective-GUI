using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;

namespace OG.Element.Interactive;

public class OgField<TElement, TScope>(string name, TScope scope, IOgTransform transform, string value, IOgTextStyle style, IOgTextEditor editor)
    : OgFocusableControl<TElement, TScope, string>(name, scope, transform, value) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override void Focus(OgEvent reason)
    {
        base.Focus(reason);
        editor.TextCursorController.ChangeCursorAndSelectionPositions(reason, Value, Transform.LocalRect);
    }

    protected override void HandleMouseDown(OgEvent reason)
    {
        base.HandleMouseDown(reason);
        editor.TextCursorController.ChangeCursorAndSelectionPositions(reason, Value, Transform.LocalRect);
        reason.Use();
    }

    protected override void BeginInteract(OgEvent reason)
    {
        base.BeginInteract(reason);
        editor.TextCursorController.ChangeCursorPosition(reason, Value, Transform.LocalRect);
    }

    protected override void EndInteract(OgEvent reason)
    {
        base.EndInteract(reason);
        editor.TextCursorController.ChangeSelectionPosition(reason, Value, Transform.LocalRect);
    }

    protected override void HandleMouseDrag(OgEvent reason)
    {
        base.HandleMouseDrag(reason);
        if(!IsFocusInteracting) return;
        editor.TextCursorController.ChangeSelectionPosition(reason, Value, Transform.LocalRect);
        reason.Use();
    }

    protected override void HandleKeyDown(OgEvent reason)
    {
        base.HandleKeyDown(reason);

        if(!IsFocused) return;

        UpdateTextIfNeeded(reason, editor.HandleKeyEvent(reason, Value, Transform.LocalRect, out bool handled));
        ;
        if(handled)
        {
            reason.Use();
            return;
        }

        char chr = reason.Character;
        if(style.Font.HasCharacter(chr))
            UpdateTextIfNeeded(reason, editor.HandleCharacter(reason, Value, chr, Transform.LocalRect));
        reason.Use();
    }

    private void UpdateTextIfNeeded(OgEvent reason, string newValue)
    {
        if(Equals(Value, newValue)) return;
        ChangeValue(newValue, reason);
    }
}