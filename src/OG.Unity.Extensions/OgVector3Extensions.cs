using OG.DataTypes.Vector;
using OG.DataTypes.Vector.Float;
using UnityEngine;
namespace OG.Unity.Extensions;
public static class OgVector3Extensions
{
    public static Vector3 ToUnity(this OgVector3 pos) => new(pos.X, pos.Y, pos.Z);
    public static Vector3 ToUnity(this OgVector3F pos) => new(pos.X, pos.Y, pos.Z);
}