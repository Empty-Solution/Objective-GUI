using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public class OgButton<TElement, TStyle, TScope>(string name, TStyle style, TScope rootScope, IOgTransform transform)
    : OgStyled<TElement, TStyle, TScope>(name, style, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgStyle
{
    public delegate void OgClickHandler(OgButton<TElement, TStyle, TScope> instance, OgEvent reason);

    public event OgClickHandler? OnClicked;

    protected override void DoStyledElement(OgEvent reason, Rect rect, TStyle style)
    {
        if(!GUI.Button(rect, GUIContent.none, GUIStyle.none)) return;
        Click(reason);
    }

    protected virtual void Click(OgEvent reason) => OnClicked?.Invoke(this, reason);
}