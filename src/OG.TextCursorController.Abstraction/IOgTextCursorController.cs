#region

using DK.Property.Abstraction.Generic;
using OG.DataTypes.Rectangle;
using OG.Event.Abstraction;

#endregion

namespace OG.TextCursorController.Abstraction;

public interface IOgTextCursorController
{
    IDkProperty<int> CursorPosition    { get; }
    IDkProperty<int> SelectionPosition { get; }

    void ChangeSelectionPosition(string text, OgRectangle rect, IOgMouseEvent reason);
    void ChangeCursorAndSelectionPositions(string text, OgRectangle rect, IOgMouseEvent reason);
    void ChangeCursorPosition(string text, OgRectangle rect, IOgMouseEvent reason);
    void ChangeCursorAndSelectionPositions(int position);
}