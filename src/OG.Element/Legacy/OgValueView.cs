using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Element.Legacy;

public abstract class OgValueView<TElement, TStyle, TScope, TValue>(string name, TStyle style, TValue value, TScope rootScope, IOgTransform transform)
    : OgStyled<TElement, TStyle, TScope>(name, style, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope where TStyle : IOgStyle
{
    public TValue Value { get; set; } = value;

    protected override void DoStyledElement(OgEvent reason, Rect rect, TStyle style) => Value = DoChangeValueElement(reason, rect, style, Value);

    protected abstract TValue DoChangeValueElement(OgEvent reason, Rect rect, TStyle style, TValue original);
}