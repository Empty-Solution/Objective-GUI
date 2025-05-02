using OG.DataTypes.Vector;
using OG.DataTypes.Vector.Float;
using UnityEngine;
namespace OG.Unity.Extensions;
public static class OgVector4Extensions
{
    public static Vector4 ToUnity(this OgVector4 pos) => new(pos.X, pos.Y, pos.Z, pos.W);
    public static Vector4 ToUnity(this OgVector4F pos) => new(pos.X, pos.Y, pos.Z, pos.W);
}