using DK.Getting.Generic;
using OG.Element.Visual;
using OG.Transformer.Abstraction;
using UnityEngine;
namespace OG.Builder.Contexts.Visual;
public class OgReadOnlyTextureBuildContext(OgTextureElement element, DkReadOnlyGetter<Rect> rectGetter)
    : OgBaseElementBuildContext<OgTextureElement, DkReadOnlyGetter<Rect>>(element, rectGetter);