using DK.Getting.Abstraction.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhBaseBackgroundBuilder
{
    private readonly EhInternalTextureBuilder m_TextureBuilder = new();
    public OgTextureElement Build(string name, IDkGetProvider<Color> colorGetter, float width, float height, float x = 0, float y = 0,
        Vector4 corners = new(), Action<OgTextureBuildContext>? action = null, IOgEventHandlerProvider? provider = null, Vector4 borders = new(),
        Texture2D? texture = null)
    {
        OgTextureElement background = m_TextureBuilder.Build($"{name}Background", colorGetter, provider, borders, corners,
            new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }), texture ?? Texture2D.whiteTexture);
        return background;
    }
}