using OG.DataTypes.Vector;
using OG.DataTypes.Vector.Float;
using UnityEngine;
namespace OG.Unity.Extensions;
public static class OgVector2Extensions
{
    public static Vector2 ToUnity(this OgVector2 pos) => new(pos.X, pos.Y);
    public static Vector2 ToUnity(this OgVector2F pos) => new(pos.X, pos.Y);
}