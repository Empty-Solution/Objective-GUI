using OG.Event.Prefab.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Element.Abstraction;
public interface IOgElement
{
    Rect                ElementRect { get; set; }
    string              Name        { get; }
    bool                IsActive    { get; set; }
    IOgOptionsContainer Options     { get; }
    bool ProcessEvent(IOgEvent reason);
}