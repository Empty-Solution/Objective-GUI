using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Observing.Generic;
using DK.Property.Generic;
using EH.Builder.Option.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhTextBuilder(IEhVisualOption context)
{
    private readonly EhInternalTextBuilder m_TextBuilder = new(context);
    public (OgTextElement Element, DkScriptableObserver<float> Observer) BuildSliderValueText(string name, IDkGetProvider<Color> colorGetter,
        string textFormat, float initial, int round, int fontSize, TextAnchor alignment, float width, float height, float x = 0, float y = 0,
        IOgEventHandlerProvider? provider = null)
    {
        DkProperty<string> textProperty = new(string.Format(textFormat, initial));
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", colorGetter, provider, fontSize, alignment, textProperty,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
            }), out DkBinding<string> textValueBinding);
        DkScriptableObserver<float> textObserver = new();
        textObserver.OnUpdate += value =>
        {
            textProperty.Set(string.Format(textFormat, Math.Round(value, round)));
            textValueBinding.Sync();
        };
        return (text, textObserver);
    }
    public OgTextElement BuildStaticText(string name, IDkGetProvider<Color> colorGetter, string text, int fontSize, TextAnchor alignment, float width,
        float height, float x = 0, float y = 0, Action<OgTextBuildContext>? action = null, IOgEventHandlerProvider? provider = null)
    {
        OgTextElement textElement = m_TextBuilder.BuildStatic($"{name}Text", colorGetter, provider, fontSize, alignment, text,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                action?.Invoke(context);
            }));
        return textElement;
    }
}