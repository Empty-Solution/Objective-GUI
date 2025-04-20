using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public class OgToggle<TElement, TStyle, TScope>(string name, TStyle style, bool value, TScope scope, IOgTransform transform)
    : OgValueView<TElement, TStyle, TScope, bool>(name, style, value, scope, transform)
    where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgStyle
{
    protected override bool DoChangeValueElement(OgEvent reason, Rect rect, TStyle style, bool original) =>
        GUI.Toggle(rect, original, GUIContent.none, GUIStyle.none);
}