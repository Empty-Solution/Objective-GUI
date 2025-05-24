using DK.Getting.Abstraction.Generic;
using DK.Property.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhThumbBuilder
{
    private readonly EhInternalTextureBuilder m_TextureBuilder = new();
    public OgTextureElement Build<TValue>(string name, IDkGetProvider<Color> colorProperty,
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, TValue> valueObserver,
        OgAnimationGetterObserver<OgTransformerRectGetter, Rect, bool> interactObserver, float size, float x = 0, float y = 0, float border = 90f,
        IDkGetProvider<float>? animationSpeed = null, IOgEventHandlerProvider? provider = null, Action<OgTextureBuildContext>? action = null)
    {
        OgTextureElement thumb = m_TextureBuilder.Build($"{name}Thumb", colorProperty, provider, new(), new(border, border, border, border),
            new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
            {
                if(animationSpeed != null) context.RectGetProvider.Speed = animationSpeed;
                interactObserver.Getter = context.RectGetProvider;
                valueObserver.Getter    = context.RectGetProvider;
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(size, size))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }), Texture2D.whiteTexture);
        return thumb;
    }
}