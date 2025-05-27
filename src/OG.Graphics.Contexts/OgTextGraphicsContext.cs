using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Graphics.Contexts;
public class OgTextGraphicsContext(string text) : IOgTextGraphicsContext
{
    public Rect         RenderRect   { get; set; }
    public int          ZOrder       { get; set; }
    public Color        Color        { get; set; }
    public string       Text         { get; set; } = text;
    public Font?        Font         { get; set; }
    public int          FontSize     { get; set; }
    public FontStyle    FontStyle    { get; set; }
    public TextAnchor   Alignment    { get; set; }
    public TextClipping TextClipping { get; set; }
    public bool         WordWrap     { get; set; }
}