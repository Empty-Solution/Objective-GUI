using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Property.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhAnimatedColorBackgroundBuilder
{
    private readonly EhAnimatedColorTextureBuilder m_TextureBuilder = new();
    public OgTextureElement Build<T>(string name, IDkGetProvider<Color> colorProperty, Texture2D texture, float width, float height, float x, float y,
        float border, OgAnimationGetterObserver<DkReadOnlyGetter<Color>, Color, T> observer, DkProperty<float> animationSpeed,
        Action<OgAnimatedColorTextureBuildContext>? action = null)
    {
        OgTextureElement background = m_TextureBuilder.Build($"{name}Background", colorProperty, texture, new(), new(border, border, border, border),
            new OgScriptableBuilderProcess<OgAnimatedColorTextureBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
                observer.Getter           = context.ColorGetter;
                context.ColorGetter.Speed = animationSpeed;
                action?.Invoke(context);
            }), out DkBinding<Color> backgroundBinding);
        return background;
    }
}