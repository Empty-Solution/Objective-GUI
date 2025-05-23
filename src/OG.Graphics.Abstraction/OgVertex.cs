using UnityEngine;
namespace OG.Graphics.Abstraction;
public struct OgVertex(Vector3 position, Color color, Vector3 uv)
{
    public Vector3 Position { get; set; } = position;
    public Vector3 Uv       { get; set; } = uv;
    public Color   Color    { get; set; } = color;
}