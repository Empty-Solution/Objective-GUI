using DK.Observing.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataKit.Transformer;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Interactive;
public class OgVectorBuildContext(IOgVectorValueElement<IOgVisualElement> element, OgTransformerRectGetter rectGetter, 
    IDkObservableProperty<Vector2> property, IDkObservable<Vector2> observable)
    : OgValueBuildContext<IOgVectorValueElement<IOgVisualElement>, IOgVisualElement, OgTransformerRectGetter, Vector2>(element, rectGetter, property, observable);