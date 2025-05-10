using OG.Event.Abstraction;
using OG.Graphics;
using UnityEngine;
namespace OG.Element.Visual;
public class OgQuadElement(string name, IOgEventHandlerProvider provider) : OgVisualElement(name, provider)
{
    public Material? Material
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            MarkDirty();
        }
    }
    protected override void BuildContext(OgGraphicsContext context)
    {
        FillContext(context);
        context.Material = Material;
        context.Rect     = ElementRect;
    }
    protected virtual void FillContext(OgGraphicsContext context)
    {
        int verticesCount = context.VerticesCount;
        FillVertex(context, Color);
        FillIndices(context, verticesCount);
    }
    protected virtual void FillVertex(OgGraphicsContext context, Color color)
    {
        // Quad
        context.AddVertex(new(new(0, 0, 0), color, new(0, 1)));
        context.AddVertex(new(new(1, 0, 0), color, new(1, 1)));
        context.AddVertex(new(new(1, 1, 0), color, new(1, 0)));
        context.AddVertex(new(new(0, 1, 0), color, new(0, 0)));
    }
    protected virtual void FillIndices(OgGraphicsContext context, int startIndex)
    {
        // Triangle 0
        context.AddIndex(startIndex + 0);
        context.AddIndex(startIndex + 1);
        context.AddIndex(startIndex + 2);
        // Triangle 1
        context.AddIndex(startIndex + 0);
        context.AddIndex(startIndex + 2);
        context.AddIndex(startIndex + 3);
    }
}