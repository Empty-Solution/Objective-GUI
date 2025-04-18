using UnityEngine;

namespace OG.Common.Scoping.Abstraction;

public interface IOgClipTransformScope : IOgTransformScope
{
    Vector2 ScrollPosition { get; set; }
}