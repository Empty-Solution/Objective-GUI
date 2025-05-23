using DK.Binding.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Generic;
using EH.Builder.Option.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Transformer.Options;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public class EhTextBuilder(IEhVisualOption context)
{
    private readonly EhInternalTextBuilder m_TextBuilder = new(context);
    public (OgTextElement Element, DkScriptableObserver<float> Observer) BuildSliderValueText(string name, DkScriptableProperty<Color> colorProperty,
        string textFormat, float initial, int round, int fontSize, TextAnchor alignment, float width, float height, float x = 0, float y = 0,
        List<DkBinding<Color>>? bindings = null)
    {
        DkProperty<string> textProperty = new(string.Format(textFormat, initial));
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", colorProperty, fontSize, alignment, textProperty,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
            }), out DkBinding<string> textValueBinding, out DkBinding<Color> colorBinding);
        DkScriptableObserver<float> textObserver = new();
        textObserver.OnUpdate += value =>
        {
            textProperty.Set(string.Format(textFormat, Math.Round(value, round)));
            textValueBinding.Sync();
        };
        bindings?.Add(colorBinding);
        return (text, textObserver);
    }
    public OgTextElement BuildStaticText(string name, IDkProperty<Color> colorProperty, string text, int fontSize, TextAnchor alignment, float width,
        float height, float x = 0, float y = 0, List<DkBinding<Color>>? bindings = null)
    {
        OgTextElement textElement = m_TextBuilder.Build($"{name}Text", colorProperty, fontSize, alignment, text,
            new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(x, y));
            }), out DkBinding<Color> textNameBinding);
        bindings?.Add(textNameBinding);
        return textElement;
    }
}