using DK.Getting.Abstraction.Generic;
using DK.Property.Generic;
using DK.Property.Observing.Abstraction.Generic;
using EH.Builder.Options.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhBaseTextBuilder(IEhVisualProvider visualProvider)
{
    private readonly EhInternalTextBuilder m_TextBuilder = new(visualProvider);
    public OgTextElement BuildSliderValueText(string name, IDkGetProvider<Color> colorGetter, string textFormat, IDkObservableProperty<float> value,
        int round, int fontSize, TextAnchor alignment, float width, float height, float x = 0, float y = 0, IOgEventHandlerProvider? provider = null)
    {
        DkScriptableProperty<string> textProperty = new(() => string.Format(textFormat, value.Get()), s => value.Set(s));
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", colorGetter, provider, fontSize, alignment, textProperty,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
            }));
        return text;
    }
    public OgTextElement BuildStaticText(string name, IDkGetProvider<Color> colorGetter, string text, int fontSize, TextAnchor alignment, float width,
        float height, float x = 0, float y = 0, Action<OgTextBuildContext>? action = null, IOgEventHandlerProvider? provider = null)
    {
        OgTextElement textElement = m_TextBuilder.BuildStatic($"{name}Text", colorGetter, provider, fontSize, alignment, text,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgMinSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }));
        return textElement;
    }
    public OgTextElement BuildBindableText(string name, IDkGetProvider<Color> colorGetter, IDkObservableProperty<string> value, int fontSize,
        TextAnchor alignment, float width, float height, float x = 0, float y = 0, Action<OgTextBuildContext>? action = null,
        IOgEventHandlerProvider? provider = null)
    {
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", colorGetter, provider, fontSize, alignment, value,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }));
        return text;
    }
}