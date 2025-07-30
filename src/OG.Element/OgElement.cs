using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Element;
public class OgElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : IOgElement
{
    public bool                 HasLayoutChanged { get; set; }
    public IOgElement?          Parent           { get; set; }
    public IDkGetProvider<Rect> ElementRect      => rectGetter;
    public string               Name             => name;

    public bool IsActive
    {
        get;
        set
        {
            HasLayoutChanged = true;
            field            = value;
        }
    } = true;

    public virtual long                 Order            { get; set; }
    public bool ProcessEvent(IOgEvent reason) => IsActive && provider.Handle(reason);
    public virtual void Resort()
    {
    }
    public virtual int CompareTo(IOgElement other) => other.Order.CompareTo(Order);
}