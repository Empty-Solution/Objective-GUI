using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgQuadElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement<OgQuadGraphicsContext>(name, provider, rectGetter)
{
    public Material?              Material         { get; set; }
    public IDkGetProvider<Color>? TopLeftColor     { get; set; }
    public IDkGetProvider<Color>? TopRightColor    { get; set; }
    public IDkGetProvider<Color>? BottomLeftColor  { get; set; }
    public IDkGetProvider<Color>? BottomRightColor { get; set; }
    public Vector4                Radius           { get; set; }
    protected override void FillContext()
    {
        m_RenderContext ??= new();
        m_RenderContext.Clear();
        int verticesCount = m_RenderContext!.VerticesCount;
        FillVertex();
        FillIndices(verticesCount);
        Rect rect = ElementRect.Get();
        Material?.SetVector("_Radius", Radius);
        Material?.SetFloat("_AspectRatio", rect.width / rect.height);
        m_RenderContext.Material = Material;
    }
    protected virtual void FillVertex()
    {
        m_RenderContext?.AddVertex(new(new(0, 0, 0), TopLeftColor?.Get() ?? Color.white, new(0, 1)));
        m_RenderContext?.AddVertex(new(new(1, 0, 0), TopRightColor?.Get() ?? Color.white, new(1, 1)));
        m_RenderContext?.AddVertex(new(new(1, 1, 0), BottomRightColor?.Get() ?? Color.white, new(1, 0)));
        m_RenderContext?.AddVertex(new(new(0, 1, 0), BottomLeftColor?.Get() ?? Color.white, new(0, 0)));
    }
    protected virtual void FillIndices(int startIndex)
    {
        //triangle 0
        m_RenderContext?.AddIndex(startIndex + 0);
        m_RenderContext?.AddIndex(startIndex + 1);
        m_RenderContext?.AddIndex(startIndex + 2);
        //triangle 1
        m_RenderContext?.AddIndex(startIndex + 0);
        m_RenderContext?.AddIndex(startIndex + 2);
        m_RenderContext?.AddIndex(startIndex + 3);
    }
}