using DK.Binding.Generic;
using DK.Property.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Transformer.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive.ElementBuilders;
public class EhBackgroundBuilder
{
    private readonly EhTextureBuilder m_TextureBuilder = new();
    public OgTextureElement Build(string name, DkScriptableProperty<Color> colorProperty, float width, float height, float x = 0, float y = 0,
        float border = 90f, List<DkBinding<Color>>? bindings = null, Action<OgTextureBuildContext>? action = null)
    {
        OgTextureElement background = m_TextureBuilder.Build($"{name}Background", colorProperty, new(), new(border, border, border, border),
            new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }), out DkBinding<Color> backgroundBinding);
        bindings?.Add(backgroundBinding);
        return background;
    }
}