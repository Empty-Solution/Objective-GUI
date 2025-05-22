using DK.Observing.Generic;
using EH.Builder.Interactive.ElementBuilders;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation.Extensions;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhSliderBuilder(IEhVisualOption context)
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly EhFillBuilder           m_FillBuilder       = new();
    private readonly EhSliderOption          m_Options           = new();
    private readonly EhInternalSliderBuilder m_SliderBuilder     = new();
    private readonly EhTextBuilder           m_TextBuilder       = new(context);
    private readonly EhThumbBuilder          m_ThumbBuilder      = new();
    public IOgElement Build(string name, float initial, float min, float max, string textFormat, bool roundToInt = true) =>
        Build(name, initial, min, max, textFormat, roundToInt, m_Options);
    private IOgElement Build(string name, float initial, float min, float max, string textFormat, bool roundToInt, EhSliderOption options)
    {
        // Создаем контейнер
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgFlexibleSizeTransformerOption(EOgOrientation.ALL));
            }));
        float elementY = (options.SubTabOption.TabHeight - (options.SliderHeight * 2)) / 2;
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            float offset = options.SliderThumbSize / 6;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbOutlineInteractObserver = new((getter, value) =>
        {
            float offset = options.SliderThumbOutlineSize / 8;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * options.SliderWidth) - (options.SliderThumbSize / 2);
            return rect;
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbOutlineObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * options.SliderWidth) - (options.SliderThumbOutlineSize / 2);
            return rect;
        });
        (OgTextElement valueText, DkScriptableObserver<float> textObserver) = m_TextBuilder.BuildValueText(name, options.TextColorProperty, textFormat,
            initial, roundToInt, options.ValueFontSize, options.ValueAlignment, options.SliderWidth, options.SliderHeight * 2, 0, -14,
            options.m_TextColorBindings);
        OgTextElement nameText = m_TextBuilder.BuildStaticText(name, options.TextColorProperty, name, options.NameFontSize, options.NameAlignment,
            options.SubTabOption.TabWidth - options.SliderWidth, options.SubTabOption.TabHeight, 0, 0, options.m_TextColorBindings);
        container.Add(nameText);
        OgTextureElement thumbOutline = m_ThumbBuilder.Build($"{name}ThumbOutline", options.ThumbOutlineColorProperty, thumbOutlineObserver,
            thumbOutlineInteractObserver, options.SliderThumbOutlineSize, 0, ((options.SliderHeight * 2) - options.SliderThumbOutlineSize) / 2,
            options.ThumbBorder, options.AnimationSpeed, options.m_ThumbOutlineColorBindings);
        OgTextureElement background = m_BackgroundBuilder.Build(name, options.BackgroundColorProperty, options.SliderWidth, options.SliderHeight, 0,
            ((options.SliderHeight * 2) - options.SliderHeight) / 2, options.BackgroundBorder, options.m_BackgroundColorBindings);
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", options.ThumbColorProperty, thumbObserver, thumbInteractObserver,
            options.SliderThumbSize, 0, ((options.SliderHeight * 2) - options.SliderThumbSize) / 2, options.ThumbBorder, options.AnimationSpeed,
            options.m_ThumbColorBindings);
        OgTextureElement backgroundFill = m_FillBuilder.Build(name, options.BackgroundFillColorProperty, 0, options.SliderHeight, 0,
            ((options.SliderHeight * 2) - options.SliderHeight) / 2, options.BackgroundBorder, options.AnimationSpeed,
            options.m_BackgroundFillColorBindings, context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgScriptableTransformerOption((rect, _, _, _) =>
                {
                    Rect thumbRect = thumb.ElementRect.Get();
                    rect.width = thumbRect.x + thumbRect.width;
                    return rect;
                }));
            });
        IOgSlider<IOgVisualElement> slider = m_SliderBuilder.Build(name, new([thumbObserver, thumbOutlineObserver, textObserver]), initial, min, max,
            new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(options.SliderWidth, options.SliderHeight * 2))
                       .SetOption(new OgFlexiblePositionTransformerOption()).SetOption(new OgMarginTransformerOption(0, elementY));
                context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                context.Element.IsInteractingObserver?.AddObserver(thumbOutlineInteractObserver);
                context.Observable.Notify(initial);
            }));
        slider.Add(background);
        slider.Add(backgroundFill);
        slider.Add(thumbOutline);
        slider.Add(thumb);
        slider.Add(valueText);
        container.Add(slider);
        return container;
    }
}