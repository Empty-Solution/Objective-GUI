using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgLineGraphicsContext : IOgGraphicsContext
{
    float   LineWidth     { get; }
    Vector2 StartPosition { get; }
    Vector2 EndPosition   { get; }
    Color   Color         { get; }
}