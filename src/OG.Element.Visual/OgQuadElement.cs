using OG.Event.Abstraction;
using OG.Graphics;
using UnityEngine;
namespace OG.Element.Visual;
public class OgQuadElement(string name, Texture texture, IOgEventHandlerProvider provider) : OgVisualElement(name, provider)
{
    public Texture Texture
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            MarkDirty();
        }
    } = texture;
    public Color Color
    {
        get;
        set
        {
            if(field == value) return;
            field = value;
            MarkDirty();
        }
    } = Color.white;
    protected override void OnBuildContext(OgGraphicsContext context)
    {
        SetupContext(context);
        context.Texture = Texture;
        context.Rect    = GetLayoutRect();
    }
    protected virtual void SetupContext(OgGraphicsContext context)
    {
        int startVertexIndex = context.VerticesCount;
        SetupVertex(context, Color);
        SetupIndices(context, startVertexIndex);
    }
    protected virtual void SetupVertex(OgGraphicsContext context, Color color)
    {
        // Quad
        context.AddVertex(new(new(0, 0, 0), color, new(0, 1)));
        context.AddVertex(new(new(1, 0, 0), color, new(1, 1)));
        context.AddVertex(new(new(1, 1, 0), color, new(1, 0)));
        context.AddVertex(new(new(0, 1, 0), color, new(0, 0)));
    }
    protected virtual void SetupIndices(OgGraphicsContext context, int startIndex)
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