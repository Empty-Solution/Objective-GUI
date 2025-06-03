using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
using System.IO;
using UnityEngine;
namespace OG.Element;
public class OgElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter) : IOgElement
{
    public         IOgElement?          Parent      { get; set; }
    public         IDkGetProvider<Rect> ElementRect => rectGetter;
    public         string               Name        => name;
    public bool IsActive
    {
        get => field;
        set
        {
            field = value;
            File.WriteAllText("EmptyHack\\EhLog.txt", $"{Name} {value}");
        }
    } = true;
    public virtual long                 Order       { get; set; }
    public bool ProcessEvent(IOgEvent reason)
    {
        if (!IsActive && reason is not IOgKeyBoardEvent)
            return false;
        
        return provider.Handle(reason);
    }
    public virtual void Resort()
    {
    }
    public virtual int CompareTo(IOgElement other) => other.Order.CompareTo(Order);
}