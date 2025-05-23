using OG.Graphics.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Graphics.Contexts;
public class OgQuadGraphicsContext : IOgQuadGraphicsContext
{
    private readonly List<int>      m_Indices = [];
    public           List<OgVertex> Vertices      { get; } = [];
    public           int            VerticesCount => Vertices.Count;
    public           int            IndicesCount  => m_Indices.Count;
    public           Vector3        Position      { get; set; } = Vector3.zero;
    public           Quaternion     Rotation      { get; set; } = Quaternion.identity;
    public           Vector3        Scale         { get; set; } = Vector3.one;
    public           Rect           ViewPort      { get; set; } = new(0.0f, 0.0f, Screen.width, Screen.height);
    public           Material?      Material      { get; set; }
    public           Rect           RenderRect    { get; set; }
    public void CopyVertices(OgVertex[] array) => Vertices.CopyTo(array);
    public void CopyIndices(int[] array) => m_Indices.CopyTo(array);
    public void AddVertex(OgVertex vertex) => Vertices.Add(vertex);
    public void AddIndex(int index) => m_Indices.Add(index);
    public void Clear()
    {
        m_Indices.Clear();
        Vertices.Clear();
    }
    public void AddVertices(IEnumerable<OgVertex> vertices) => Vertices.AddRange(vertices);
    public void AddIndices(IEnumerable<int> indices) => m_Indices.AddRange(indices);
}