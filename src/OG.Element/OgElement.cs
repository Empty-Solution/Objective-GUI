using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : IOgElement
{
    public IDkGetProvider<Rect> ElementRect => rectGetter;
    public string               Name        => name;
    public bool                 IsActive    { get; set; } = true;
    public virtual bool ProcessEvent(IOgEvent reason) => IsActive && provider.Handle(reason);
    public virtual int CompareTo(IOgElement other) => 0;
}