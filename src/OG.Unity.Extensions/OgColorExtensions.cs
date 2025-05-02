using OG.DataTypes.Color;
using OG.DataTypes.Color.Bit;
using UnityEngine;
namespace OG.Unity.Extensions;
public static class OgColorExtensions
{
    public static Color ToUnity(this OgRgbaColor color) => new(color.R, color.G, color.B, color.A);
    public static Color ToUnity(this OgRgbColor color) => new(color.R, color.G, color.B);
    public static Color32 ToUnity(this OgRgbaColor32 color) => new(color.R, color.G, color.B, color.A);
    public static Color32 ToUnity(this OgRgbColor32 color) => new(color.R, color.G, color.B, 255);
}