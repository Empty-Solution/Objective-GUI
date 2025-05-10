using System.Collections.Generic;
using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsContext
{
    int         VerticesCount { get; }
    int         IndicesCount  { get; }
    Vector3     Position      { get; set; }
    Quaternion  Rotation      { get; set; }
    Vector3     Scale         { get; set; }
    Rect        ViewPort      { get; set; }
    public Rect Rect          { get; set; }
    Material?   Material      { get; set; }
    void CopyVertices(OgVertex[] array);
    void CopyIndices(int[] array);
    void AddVertices(IEnumerable<OgVertex> vertices);
    void AddIndices(IEnumerable<int> indices);
    void AddVertex(OgVertex vertex);
    void AddIndex(int index);
    void Clear();
}