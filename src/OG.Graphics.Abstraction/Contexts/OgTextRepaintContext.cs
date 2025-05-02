using OG.DataTypes.Color;
using OG.DataTypes.ElementAlignment;
using OG.DataTypes.Font.Abstraction;
using OG.DataTypes.FontStyle;
using OG.DataTypes.Rectangle;
using OG.DataTypes.TextClipping;
using System.Collections.Generic;
namespace OG.Graphics.Abstraction.Contexts;
public class OgTextRepaintContext : IOgRepaintContext
{
    public int                 FontSize    { get; set; }
    public string              Text        { get; set; }
    public bool                WordWrap    { get; set; }
    public EOgFontStyle        FontStyle   { get; set; }
    public EOgElementAlignment Alignment   { get; set; }
    public EOgTextClipping     Clipping    { get; set; }
    public IOgFont             Font        { get; set; }
    public List<float>         CharsSizes  { get; } = [];
    public float               LineHeight  { get; set; }
    public OgRgbaColor         Color       { get; set; }
    public OgRectangle         RepaintRect { get; set; }
}