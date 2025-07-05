using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;

public class OgLineGraphics : OgBaseGraphics<IOgLineGraphicsContext>
{
    private static Texture2D? antiAliasingTexture;

    public override void ProcessContext(IOgLineGraphicsContext ctx)
    {
        if((ctx.StartPosition.x < 0.05f || ctx.StartPosition.y < 0.05f || ctx.StartPosition.x > Screen.width || ctx.StartPosition.y > Screen.height) && 
           ctx.EndPosition.x   < 0.05f || ctx.EndPosition.y   < 0.05f || ctx.EndPosition.x   > Screen.width || ctx.EndPosition.y   > Screen.height) return;
        if(antiAliasingTexture is null)
        {
            antiAliasingTexture = new(1, 3, TextureFormat.RGBA32, 1, true);
            antiAliasingTexture.SetPixel(0, 0, new(1f, 1f, 1f, 0f));
            antiAliasingTexture.SetPixel(0, 1, new(1f, 1f, 1f, 1f));
            antiAliasingTexture.SetPixel(0, 2, new(1f, 1f, 1f, 0f));
            antiAliasingTexture.Apply(true);
        }

        GL.PushMatrix();
        GL.MultMatrix(GetMatrix(ctx.StartPosition, ctx.EndPosition, ctx.LineWidth));
        GUI.DrawTexture(new(0, 0, 1, 1), antiAliasingTexture, ScaleMode.StretchToFill, true, 1, ctx.Color, Vector4.zero, Vector4.zero);
        GL.PopMatrix();
    }

    private static Matrix4x4 GetMatrix(Vector2 startPosition, Vector2 endPosition, float width = 1.0f)
    {
        float deltaX      = endPosition.x - startPosition.x;
        float deltaY      = endPosition.y - startPosition.y;
        float length      = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
        float widthDeltaX = width * deltaY / length;
        float widthDeltaY = width * deltaX / length;
        var   matrix      = Matrix4x4.identity;
        matrix.m00 = deltaX;
        matrix.m01 = -widthDeltaX;
        matrix.m03 = startPosition.x + (0.5f * widthDeltaX);
        matrix.m10 = deltaY;
        matrix.m11 = widthDeltaY;
        matrix.m13 = startPosition.y - (0.5f * widthDeltaY);
        return matrix;
    }
}