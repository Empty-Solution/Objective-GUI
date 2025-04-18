using UnityEngine;

namespace OG.Style.Abstraction;

public interface IOgStyle
{
    Vector4 Offset { get; set; }
}

public interface IOgColorizeStyle : IOgStyle
{
    Color Color { get; set; }
}