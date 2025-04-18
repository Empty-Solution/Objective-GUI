using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public class OgLabel<TElement, TStyle, TScope>(string name, TStyle style, string text, TScope rootScope, IOgTransform transform)
    : OgText<TElement, TStyle, TScope>(name, style, text, rootScope, transform)
    where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgTextStyle
{
    protected override void DoTextElement(OgEvent reason, Rect rect, GUIStyle style, string text) => GUI.Label(Transform.LocalRect, text, style);
}