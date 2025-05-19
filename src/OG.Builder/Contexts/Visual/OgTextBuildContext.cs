using OG.DataKit.Animation;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgTextBuildContext<TGetter>(OgTextElement element, TGetter rectGetter, IOgOptionsContainer transformerOptions)
    : OgTransformerElementBuildContext<OgTextElement, TGetter>(element, rectGetter, transformerOptions) where TGetter : OgAnimationGetter<Rect>;