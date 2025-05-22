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
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive.ElementBuilders;
public class EhThumbBuilder
{
    private readonly EhTextureBuilder m_TextureBuilder;
    public EhThumbBuilder() => m_TextureBuilder = new();
    public OgTextureElement Build<TValue>(string name, DkScriptableProperty<Color> colorProperty,
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, TValue> valueObserver,
        OgAnimationGetterObserver<OgTransformerRectGetter, Rect, bool> interactObserver, float width, float? height = null, float x = 0, float y = 0,
        float border = 90f, IDkGetProvider<float>? animationSpeed = null, List<DkBinding<Color>>? bindings = null)
    {
        float thumbHeight = height ?? width;
        OgTextureElement thumb = m_TextureBuilder.Build($"{name}Thumb", colorProperty, new(), new(border, border, border, border),
                                                        new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                        {
                                                            if(animationSpeed != null) context.RectGetProvider.Speed = animationSpeed;
                                                            interactObserver.Getter = context.RectGetProvider;
                                                            valueObserver.Getter    = context.RectGetProvider;
                                                            context.RectGetProvider.OriginalGetter.Options
                                                                   .SetOption(new OgSizeTransformerOption(width, thumbHeight))
                                                                   .SetOption(new OgMarginTransformerOption(x, y));
                                                        }), out DkBinding<Color> thumbBinding);
        bindings?.Add(thumbBinding);
        return thumb;
    }
}