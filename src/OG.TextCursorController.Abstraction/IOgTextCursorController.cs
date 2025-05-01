using DK.Property.Abstraction.Generic;
using OG.Event.Abstraction;

namespace OG.TextCursorController.Abstraction;

public interface IOgTextCursorController
{
    IDkProperty<int> CursorPosition { get; }

    IDkProperty<int> SelectionPosition { get; }

    void ChangeCursorPosition(string text, IOgMouseEvent reason, OgTextRepaintContext context);
    void ChangeSelectionPosition(string text, IOgMouseEvent reason, OgTextRepaintContext context);
    void ChangeCursorAndSelectionPositions(string text, IOgMouseEvent reason, OgTextRepaintContext context);

    void ChangeCursorAndSelectionPositions(int position);
}