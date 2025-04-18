using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public class OgField<TElement, TStyle, TScope>(string name, TStyle style, string value, int maxLength, TScope rootScope, IOgTransform transform)
    : OgValueView<TElement, TStyle, TScope, string>(name, style, value, rootScope, transform)
    where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgTextStyle
{
    protected override string DoChangeValueElement(OgEvent reason, Rect rect, TStyle style, string original) =>
        GUI.TextField(rect, original, maxLength, InternalLegacyGuiTool.GetUnityStyle(style));
}