using OG.Graphics;
using OG.Graphics.Abstraction.Contexts;
using OG.Unity.Extensions;
using UnityEngine;
namespace OG.Unity.Graphics;
public class OgTextureRepaintHandler : OgBaseRepaintHandler<OgTextureRepaintContext>
{
    protected override bool Handle(OgTextureRepaintContext reason)
    {
        // TODO: Texture2D.whiteTexture to reason bitmap.
        GUI.DrawTexture(reason.RepaintRect.ToUnity(), Texture2D.whiteTexture, (ScaleMode)reason.ScaleMode, reason.AlphaBlend,
                        reason.ImageAspect, reason.Color.ToUnity(), reason.Widths.ToUnity(), reason.Radiuses.ToUnity());
        return true;
    }
}