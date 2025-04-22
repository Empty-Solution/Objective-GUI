using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Style;

public class OgStyle(Vector4 offset) : IOgStyle
{
    public Vector4 Offset { get; set; } = offset;
}