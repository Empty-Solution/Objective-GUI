using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction.Contexts;
public class OgInlineRepaintContext : IOgRepaintContext
{
    public DkScopeContext Scope       { get; set; }
    public OgRectangle    RepaintRect { get; set; }
}