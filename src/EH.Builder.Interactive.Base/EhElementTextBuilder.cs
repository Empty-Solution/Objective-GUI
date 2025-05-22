using DK.Binding.Generic;
using DK.Observing.Generic;
using DK.Property.Generic;
using EH.Builder.Option.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Visual;
using OG.Transformer.Options;
using System.Collections.Generic;
using UnityEngine;
namespace EH.Builder.Interactive.ElementBuilders;
public class EhElementTextBuilder
{
    private readonly EhTextBuilder m_TextBuilder;
    public EhElementTextBuilder(IEhVisualOption context) => m_TextBuilder = new(context);
    public (OgTextElement Element, DkScriptableObserver<float> Observer) BuildValueText(string name, DkScriptableProperty<Color> colorProperty,
        string textFormat, float initial, bool roundToInt, int fontSize, TextAnchor alignment, float width, float height, float x = 0, float y = 0,
        List<DkBinding<Color>>? bindings = null)
    {
        DkProperty<string> textProperty = new(string.Format(textFormat, initial));
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", colorProperty, fontSize, alignment, textProperty,
                                                 new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                                 {
                                                     context.RectGetProvider.OriginalGetter.Options
                                                            .SetOption(new OgSizeTransformerOption(width,
                                                                                                   height))
                                                            .SetOption(new OgMarginTransformerOption(x, y));
                                                 }), out DkBinding<string> textValueBinding,
                                                 out DkBinding<Color> colorBinding);
        DkScriptableObserver<float> textObserver = new();
        textObserver.OnUpdate += value =>
        {
            textProperty.Set(string.Format(textFormat, roundToInt ? Mathf.RoundToInt(value) : value));
            textValueBinding.Sync();
        };
        bindings?.Add(colorBinding);
        return (text, textObserver);
    }
    public OgTextElement BuildStaticText(string name, DkScriptableProperty<Color> colorProperty, string text, int fontSize, TextAnchor alignment,
        float width, float height, float x = 0, float y = 0, List<DkBinding<Color>>? bindings = null)
    {
        OgTextElement textElement = m_TextBuilder.Build($"{name}Text", colorProperty, fontSize, alignment, text,
                                                        new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                                        {
                                                            context.RectGetProvider.OriginalGetter.Options
                                                                   .SetOption(new OgSizeTransformerOption(width, height))
                                                                   .SetOption(new OgMarginTransformerOption(x, y));
                                                        }), out DkBinding<Color> textNameBinding);
        bindings?.Add(textNameBinding);
        return textElement;
    }
}