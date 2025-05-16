using DK.Getting.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element.Abstraction;
public interface IOgElement
{
    IDkGetProvider<Rect> ElementRect { get; }
    string               Name        { get; }
    bool                 IsActive    { get; set; }
    bool ProcessEvent(IOgEvent reason);
}