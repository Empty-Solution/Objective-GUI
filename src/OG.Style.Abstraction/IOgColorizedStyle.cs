using UnityEngine;

namespace OG.Style.Abstraction;

public interface IOgColorizedStyle : IOgStyle
{
    Color Color { get; set; }
}