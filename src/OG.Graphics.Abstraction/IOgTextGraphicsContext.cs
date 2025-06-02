using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgTextGraphicsContext : IOgGraphicsContext
{
    string       Text         { get; }
    Font?        Font         { get; }
    int          FontSize     { get; }
    FontStyle    FontStyle    { get; }
    TextAnchor   Alignment    { get; }
    TextClipping TextClipping { get; }
    bool         WordWrap     { get; }
    Color        Color        { get; }
}