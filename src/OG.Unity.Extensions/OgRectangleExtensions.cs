using OG.DataTypes.Rectangle;
using OG.DataTypes.Rectangle.Float;
using UnityEngine;
namespace OG.Unity.Extensions;
public static class OgRectangleExtensions
{
    public static Rect ToUnity(this OgRectangle  rect) => new(rect.X, rect.Y, rect.Width, rect.Height);
    public static Rect ToUnity(this OgRectangleF rect) => new(rect.X, rect.Y, rect.Width, rect.Height);
}