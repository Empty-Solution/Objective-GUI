using DK.Getting.Generic;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgReadOnlyTextBuildContext(OgTextElement element, DkReadOnlyGetter<Rect> rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgTextElement, DkReadOnlyGetter<Rect>>(element, rectGetter, transformerOptions);