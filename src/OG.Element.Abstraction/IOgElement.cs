using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Element.Abstraction;
public interface IOgElement
{
    string              Name        { get; set; }
    bool                IsActive    { get; }
    Rect                ElementRect { get; }
    IOgOptionsContainer Options     { get; set; }
    bool ProcessEvent(IOgEvent reason);
}