using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Graphics;
public class OgGraphicsContext : IOgGraphicsContext
{
    private readonly List<int>      m_Indices  = [];
    private readonly List<OgVertex> m_Vertices = [];
    public           int            VerticesCount                   => m_Vertices.Count;
    public           int            IndicesCount                    => m_Indices.Count;
    public           Vector3        Position                        { get; set; } = Vector3.zero;
    public           Quaternion     Rotation                        { get; set; } = Quaternion.identity;
    public           Vector3        Scale                           { get; set; } = Vector3.one;
    public           Rect           Rect                            { get; set; }
    public           Rect           ViewPort                        { get; set; } = new(0.0f, 0.0f, Screen.width, Screen.height);
    public           Texture        Texture                         { get; set; } = Texture2D.whiteTexture;
    public           void           CopyVertices(OgVertex[] array)  => m_Vertices.CopyTo(array);
    public           void           CopyIndices(int[]       array)  => m_Indices.CopyTo(array);
    public           void           AddVertex(OgVertex      vertex) => m_Vertices.Add(vertex);
    public           void           AddIndex(int            index)  => m_Indices.Add(index);
    public void Clear()
    {
        m_Indices.Clear();
        m_Vertices.Clear();
    }
    public void AddVertices(IEnumerable<OgVertex> vertices) => m_Vertices.AddRange(vertices);
    public void AddIndices(IEnumerable<int>       indices)  => m_Indices.AddRange(indices);
}