using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics;
public class OgGlGraphics(Material material) : IOgGraphics
{
    private int[]      m_IndicesBuffer = new int[32];
    private OgVertex[] m_VertexBuffer  = new OgVertex[32];
    public void Render(IOgGraphicsContext ctx)
    {
        int verticesCount = ctx.VerticesCount;
        int indicesCount  = ctx.IndicesCount;
        if(verticesCount is 0) return;
        if(indicesCount is 0) return;
        OgVertex[] vertices                          = m_VertexBuffer;
        int[]      indices                           = m_IndicesBuffer;
        if(verticesCount > vertices.Length) vertices = m_VertexBuffer  = new OgVertex[verticesCount * 2];
        if(indicesCount > indices.Length) indices    = m_IndicesBuffer = new int[indicesCount * 2];
        ctx.CopyVertices(vertices);
        ctx.CopyIndices(indices);
        material.mainTexture = ctx.Texture;
        material.SetPass(0);
        GL.PushMatrix();
        GL.Viewport(ctx.ViewPort);
        GL.MultMatrix(Matrix4x4.TRS(ctx.Position, ctx.Rotation, ctx.Scale));
        GL.Begin(GL.TRIANGLES);
        Rect rect = ctx.Rect;
        for(int i = 0; i < indicesCount; i += 3)
        {
            int      idx0 = indices[i];
            int      idx1 = indices[i + 1];
            int      idx2 = indices[i + 2];
            OgVertex v0   = vertices[idx0];
            OgVertex v1   = vertices[idx1];
            OgVertex v2   = vertices[idx2];
            Vertex(v0, rect);
            Vertex(v1, rect);
            Vertex(v2, rect);
        }
        GL.End();
        GL.PopMatrix();
    }
    private static void Vertex(OgVertex vertex, Rect rect)
    {
        GL.Color(vertex.Color);
        GL.TexCoord(vertex.Uv);
        Vector3 position = vertex.Position;
        position.x = rect.x + (position.x * rect.width);
        position.y = rect.y + (position.y * rect.height);
        GL.Vertex(position);
    }
}