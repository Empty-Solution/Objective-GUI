using DK.Observing.Generic;
using EH.Builder.Abstraction;
using EH.Builder.Interactive.Base;
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
public class EhSliderBuilder(IEhVisualOption context) : IEhSliderBuilder
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly EhFillBuilder           m_FillBuilder       = new();
    private readonly EhOptionsProvider       m_OptionsProvider   = new();
    private readonly EhInternalSliderBuilder m_SliderBuilder     = new();
    private readonly EhTextBuilder           m_TextBuilder       = new(context);
    private readonly EhThumbBuilder          m_ThumbBuilder      = new();
    public IOgContainer<IOgElement> Build(string name, float initial, float min, float max, string textFormat, bool roundToInt = true,
        DkObserver<float>? observer = null) =>
        Build(name, initial, min, max, textFormat, roundToInt, observer, m_OptionsProvider);
    private IOgContainer<IOgElement> Build(string name, float initial, float min, float max, string textFormat, bool roundToInt,
        DkObserver<float>? observer, EhOptionsProvider provider)
    {
        EhSliderOption option = provider.SliderOption;
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL));
            }));
        float elementY = (provider.SubTabOption.SubTabHeight - (option.SliderHeight * 2)) / 2;
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            float offset = option.SliderThumbSize / 6;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbOutlineInteractObserver = new((getter, value) =>
        {
            float offset = option.SliderThumbOutlineSize / 8;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * option.SliderWidth) - (option.SliderThumbSize / 2);
            return rect;
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbOutlineObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * option.SliderWidth) - (option.SliderThumbOutlineSize / 2);
            return rect;
        });
        (OgTextElement valueText, DkScriptableObserver<float> textObserver) = m_TextBuilder.BuildSliderValueText(name, option.TextColorProperty,
            textFormat, initial, roundToInt, option.ValueFontSize, option.ValueAlignment, option.SliderWidth, option.SliderHeight * 2, 0, -14,
            option.m_TextColorBindings);
        OgTextElement nameText = m_TextBuilder.BuildStaticText(name, option.TextColorProperty, name, option.NameFontSize, option.NameAlignment,
            provider.SubTabOption.SubTabWidth - option.SliderWidth, provider.SubTabOption.SubTabHeight, 0, 0, option.m_TextColorBindings);
        container.Add(nameText);
        OgTextureElement thumbOutline = m_ThumbBuilder.Build($"{name}ThumbOutline", option.ThumbOutlineColorProperty, thumbOutlineObserver,
            thumbOutlineInteractObserver, option.SliderThumbOutlineSize, 0, ((option.SliderHeight * 2) - option.SliderThumbOutlineSize) / 2,
            option.ThumbBorder, provider.AnimationSpeed, option.m_ThumbOutlineColorBindings);
        OgTextureElement background = m_BackgroundBuilder.Build(name, option.BackgroundColorProperty, option.SliderWidth, option.SliderHeight, 0,
            ((option.SliderHeight * 2) - option.SliderHeight) / 2, option.BackgroundBorder, option.m_BackgroundColorBindings);
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", option.ThumbColorProperty, thumbObserver, thumbInteractObserver,
            option.SliderThumbSize, 0, ((option.SliderHeight * 2) - option.SliderThumbSize) / 2, option.ThumbBorder, provider.AnimationSpeed,
            option.m_ThumbColorBindings);
        OgTextureElement backgroundFill = m_FillBuilder.Build(name, option.BackgroundFillColorProperty, 0, option.SliderHeight, 0,
            ((option.SliderHeight * 2) - option.SliderHeight) / 2, option.BackgroundBorder, provider.AnimationSpeed, option.m_BackgroundFillColorBindings,
            context =>
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
                if(observer is not null) context.Observable.AddObserver(observer);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.SliderWidth, option.SliderHeight * 2))
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