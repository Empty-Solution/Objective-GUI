using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Property.Generic;
using EH.Builder.Option.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhAnimatedColorTextBuilder(IEhVisualOption context)
{
    private readonly EhInternalAnimatedColorTextBuilder m_TextBuilder = new(context);
    public OgTextElement BuildStaticText<T>(string name, IDkGetProvider<Color> colorProperty, string text, int fontSize, TextAnchor alignment, float width,
        float height, float x, float y, OgAnimationGetterObserver<DkReadOnlyGetter<Color>, Color, T> observer, DkProperty<float> animationSpeed)
    {
        OgTextElement textElement = m_TextBuilder.Build($"{name}Text", colorProperty, fontSize, alignment, text,
            new OgScriptableBuilderProcess<OgAnimatedColorTextBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
                context.ColorGetter.Speed = animationSpeed;
                observer.Getter           = context.ColorGetter;
            }), out DkBinding<Color> textNameBinding);
        return textElement;
    }
}