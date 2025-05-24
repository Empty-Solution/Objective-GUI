using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Interactive;
public class OgScrollBuildContext(IOgVectorValueElement<IOgElement> element, OgTransformerRectField rectGetter, IDkObservableProperty<Vector2> property)
    : OgValueBuildContext<IOgVectorValueElement<IOgElement>, IOgElement, OgTransformerRectField, Vector2>(element, rectGetter, property);