using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using OG.Graphics.Contexts;
using UnityEngine;
namespace OG.Element.Visual;
public class OgQuadElement(string name, IOgEventHandlerProvider provider, IDkGetProvider<Rect> rectGetter)
    : OgVisualElement<OgQuadGraphicsContext>(name, provider, rectGetter)
{
    private bool m_Dirty;
    public Material? Material
    {
        get;
        set
        {
            field   = value;
            m_Dirty = true;
        }
    }
    public Color TopLeftColor
    {
        get;
        set
        {
            field   = value;
            m_Dirty = true;
        }
    }
    public Color TopRightColor
    {
        get;
        set
        {
            field   = value;
            m_Dirty = true;
        }
    }
    public Color BottomLeftColor
    {
        get;
        set
        {
            field   = value;
            m_Dirty = true;
        }
    }
    public Color BottomRightColor
    {
        get;
        set
        {
            field   = value;
            m_Dirty = true;
        }
    }
    protected override void FillContext()
    {
        m_RenderContext ??= new();
        if(!m_Dirty) return;
        FillContext(m_RenderContext);
        m_RenderContext.Material   = Material;
        m_RenderContext.RenderRect = ElementRect.Get();
    }
    protected virtual void FillContext(OgQuadGraphicsContext context)
    {
        int verticesCount = context.VerticesCount;
        FillVertex(context);
        FillIndices(context, verticesCount);
    }
    protected virtual void FillVertex(OgQuadGraphicsContext context)
    {
        context.AddVertex(new(new(0, 0, 0), BottomLeftColor, new(0, 1)));
        context.AddVertex(new(new(1, 0, 0), BottomRightColor, new(1, 1)));
        context.AddVertex(new(new(1, 1, 0), TopRightColor, new(1, 0)));
        context.AddVertex(new(new(0, 1, 0), TopLeftColor, new(0, 0)));
    }
    protected virtual void FillIndices(OgQuadGraphicsContext context, int startIndex)
    {
        context.AddIndex(startIndex + 0);
        context.AddIndex(startIndex + 1);
        context.AddIndex(startIndex + 2);
        context.AddIndex(startIndex + 0);
        context.AddIndex(startIndex + 2);
        context.AddIndex(startIndex + 3);
    }
}