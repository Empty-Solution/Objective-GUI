#region

using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;

#endregion

namespace OG.Graphics.Abstraction;

public interface IOgGraphicsTool
{
    DkScopeContext Clip(OgRectangle rectangle);

    DkScopeContext Inline(OgRectangle rectangle);
}