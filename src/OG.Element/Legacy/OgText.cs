using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public abstract class OgText<TElement, TStyle, TScope>(string name, TStyle style, string text, TScope rootScope, IOgTransform transform)
    : OgStyled<TElement, TStyle, TScope>(name, style, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgTextStyle
{
    public string Text { get; set; } = text;

    protected override void DoStyledElement(OgEvent reason, Rect rect, TStyle style) => DoTextElement(reason, rect, InternalLegacyGuiTool.GetUnityStyle(style), Text);

    protected abstract void DoTextElement(OgEvent reason, Rect rect, GUIStyle style, string text);
}