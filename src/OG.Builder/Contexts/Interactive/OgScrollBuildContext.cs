using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Interactive;
public class OgScrollBuildContext(IOgVectorValueElement<IOgElement> element, OgTransformerRectField rectGetter, IOgOptionsContainer transformerOptions,
    IDkObservableProperty<Vector2> property, IDkObservable<Vector2> observable)
    : OgValueBuildContext<IOgVectorValueElement<IOgElement>, IOgElement, OgTransformerRectField, Vector2>(element, rectGetter, transformerOptions,
        property, observable);