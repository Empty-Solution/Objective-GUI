using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public class OgImage<TElement, TStyle, TScope>(string name, TStyle style, Texture texture, TScope scope, IOgTransform transform)
    : OgStyled<TElement, TStyle, TScope>(name, style, scope, transform) where TElement : IOgElement where TStyle : IOgTextureStyle where TScope : IOgTransformScope
{
    public Texture Texture { get; set; } = texture;

    protected override void DoStyledElement(OgEvent reason, Rect rect, TStyle style) =>
        GUI.DrawTexture(rect, Texture, style.ScaleMode, style.AlphaBlend, style.ImageAspect, style.Color, style.BorderWidths, style.BorderRadiuses);
}