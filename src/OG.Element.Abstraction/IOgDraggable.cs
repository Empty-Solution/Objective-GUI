using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using UnityEngine;

namespace OG.Element.Abstraction;

public interface IOgDraggable<TElement, TScope> : IOgContainer<TElement> where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgDragEnterHandler(IOgDraggable<TElement, TScope> instance, Rect rect, OgEvent reason);

    public delegate void OgDragExitHandler(IOgDraggable<TElement, TScope> instance, Rect rect, OgEvent reason);

    public delegate void OgDragPerformHandler(IOgDraggable<TElement, TScope> instance, Rect rect, Vector2 delta, OgEvent reason);

    public event OgDragEnterHandler? OnBeginDrag;
    public event OgDragPerformHandler? OnPerformDrag;
    public event OgDragExitHandler? OnEndDrag;
}