using System;
namespace OG.DataTypes.Alignment;
[Flags]
public enum EOgAlignment
{
    LEFT = 0,
    RIGHT = 1,
    CENTER = 2,
    TOP = 4,
    MIDDLE = 8,
    BOTTOM = 16
}