using DK.Getting.Abstraction.Generic;
using OG.Event.Prefab.Abstraction;
using System;
using UnityEngine;
namespace OG.Element.Abstraction;
public interface IOgElement : IComparable<IOgElement>
{
    IDkGetProvider<Rect> ElementRect { get; }
    string               Name        { get; }
    bool                 IsActive    { get; set; }
    long                  Order       { get; set; }
    bool ProcessEvent(IOgEvent reason);
}