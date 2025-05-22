using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Property.Generic;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Visual;
using OG.Transformer.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive.ElementBuilders;
public class EhFillBuilder
{
    private readonly EhTextureBuilder m_TextureBuilder = new();
    public OgTextureElement Build(string name, DkScriptableProperty<Color> colorProperty, float width, float height, float x = 0, float y = 0,
        float border = 90f, IDkGetProvider<float>? animationSpeed = null, List<DkBinding<Color>>? bindings = null,
        Action<OgTextureBuildContext>? action = null)
    {
        OgTextureElement fill = m_TextureBuilder.Build($"{name}Fill", colorProperty, new(), new(border, border, border, border),
                                                       new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                       {
                                                           if(animationSpeed != null) context.RectGetProvider.Speed = animationSpeed;
                                                           context.RectGetProvider.OriginalGetter.Options
                                                                  .SetOption(new OgSizeTransformerOption(width, height))
                                                                  .SetOption(new OgMarginTransformerOption(x, y));
                                                           action?.Invoke(context);
                                                       }), out DkBinding<Color> fillBinding);
        bindings?.Add(fillBinding);
        return fill;
    }
}