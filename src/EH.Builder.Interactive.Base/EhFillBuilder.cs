using DK.Getting.Abstraction.Generic;
using DK.Property.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhFillBuilder
{
    private readonly EhInternalTextureBuilder m_TextureBuilder = new();
    public OgTextureElement Build(string name, IDkGetProvider<Color> colorProperty, float width, float height, float x = 0, float y = 0, float border = 90f,
        IDkGetProvider<float>? animationSpeed = null, Action<OgTextureBuildContext>? action = null, IOgEventHandlerProvider? provider = null)
    {
        OgTextureElement fill = m_TextureBuilder.Build($"{name}Fill", colorProperty, provider, new(), new(border, border, border, border),
            new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
            {
                if(animationSpeed != null) context.RectGetProvider.Speed = animationSpeed;
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }), Texture2D.whiteTexture);
        return fill;
    }
}