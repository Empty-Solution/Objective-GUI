using OG.DataTypes.Color;
using OG.DataTypes.Font;
using OG.DataTypes.FontStyle;
using OG.DataTypes.Rectangle;
using OG.DataTypes.TextAnchor;
using OG.DataTypes.TextClipping;
using System.Collections.Generic;
namespace OG.Graphics.Abstraction;
public class OgTextRepaintContext : IOgRepaintContext
{
    public int                FontSize   { get; set; }
    public string             Text       { get; set; }
    public bool               WordWrap   { get; set; }
    public EOgFontStyle       FontStyle  { get; set; }
    public EOgTextAnchor      Alignment  { get; set; }
    public EOgTextClipping    Clipping   { get; set; }
    public OgFont             Font       { get; set; }
    public IEnumerable<float> CharsSizes { get; set; } = [];
    public float              LineHeight { get; set; }
    public OgRgbaColor        Color      { get; set; }
    public OgRectangle        RenderRect { get; set; }
}