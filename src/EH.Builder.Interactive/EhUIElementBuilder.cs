using DK.Getting.Abstraction.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Transformer;
using UnityEngine;
namespace EH.Builder.Interactive;
public abstract class EhUIElementBuilder<TValue>
{
    protected readonly EhTextureBuilder m_TextureBuilder;
    protected EhUIElementBuilder() => m_TextureBuilder = new();
    protected void FillAnimationContext(OgTextureBuildContext context, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, TValue> valueObserver,
        OgAnimationGetterObserver<OgTransformerRectGetter, Rect, bool> interactObserver, IDkGetProvider<float> animationSpeed)
    {
        context.RectGetProvider.Speed = animationSpeed;
        interactObserver.Getter       = context.RectGetProvider;
        valueObserver.Getter          = context.RectGetProvider;
    }
}