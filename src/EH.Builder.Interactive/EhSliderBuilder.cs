using DK.Getting.Generic;
using DK.Observing.Generic;
using EH.Builder.Abstraction;
using EH.Builder.Interactive.Base;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
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
using OG.Event;
using OG.Event.Extensions;
using OG.Event.Prefab.Abstraction;
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
    public IOgContainer<IOgElement> Build(string name, float initial, float min, float max, string textFormat, int round = 0,
        DkObserver<float>? observer = null) =>
        Build(name, initial, min, max, textFormat, round, observer, m_OptionsProvider);
    private IOgContainer<IOgElement> Build(string name, float initial, float min, float max, string textFormat, int round, DkObserver<float>? observer,
        EhOptionsProvider provider)
    {
        EhSliderOption option = provider.SliderOption;
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(provider.SubTabOption.SubTabWidth, provider.SubTabOption.SubTabHeight))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL));
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
        (OgTextElement valueText, DkScriptableObserver<float> textObserver) = m_TextBuilder.BuildSliderValueText(name, option.TextColor,
            textFormat, initial, round, option.ValueFontSize, option.ValueAlignment, option.SliderWidth, option.SliderHeight * 2, 0, -14);
        OgTextElement nameText = m_TextBuilder.BuildStaticText(name, option.TextColor, name, option.NameFontSize, option.NameAlignment,
            provider.SubTabOption.SubTabWidth - option.SliderWidth, provider.SubTabOption.SubTabHeight);
        container.Add(nameText);
        
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbOutlineHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.ThumbOutlineHoverColor.Get() : option.ThumbOutlineColor.Get();
        });
        OgEventHandlerProvider thumbOutlineEventHandler = new();
        OgAnimationColorGetter thumbOutlineGetter       = new(thumbOutlineEventHandler);
        OgTextureElement thumbOutline = m_ThumbBuilder.Build($"{name}ThumbOutline", thumbOutlineGetter, thumbOutlineObserver,
            thumbOutlineInteractObserver, option.SliderThumbOutlineSize, 0, ((option.SliderHeight * 2) - option.SliderThumbOutlineSize) / 2,
            option.ThumbBorder, provider.AnimationSpeed, thumbOutlineEventHandler, context =>
            {
                thumbOutlineGetter.Speed = provider.AnimationSpeed;
                thumbOutlineGetter.RenderCallback = context.RectGetProvider;
                thumbOutlineHoverObserver.Getter = thumbOutlineGetter;
                thumbOutlineEventHandler.Register(thumbOutlineGetter);
            });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.ThumbHoverColor.Get() : option.ThumbColor.Get();
        });
        OgEventHandlerProvider thumbEventHandler = new();
        OgAnimationColorGetter thumbGetter       = new(thumbEventHandler);
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", thumbGetter, thumbObserver, thumbInteractObserver,
            option.SliderThumbSize, 0, ((option.SliderHeight * 2) - option.SliderThumbSize) / 2, option.ThumbBorder, provider.AnimationSpeed, thumbEventHandler,
            context =>
            {
                thumbGetter.Speed          = provider.AnimationSpeed;
                thumbGetter.RenderCallback = context.RectGetProvider;
                thumbHoverObserver.Getter  = thumbGetter;
                thumbEventHandler.Register(thumbGetter);
            });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> fillHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.FillHoverColor.Get() : option.FillColor.Get();
        });
        OgEventHandlerProvider fillEventHandler = new();
        OgAnimationColorGetter fillGetter       = new(fillEventHandler);
        OgTextureElement fill = m_FillBuilder.Build(name, fillGetter, 0, option.SliderHeight, 0,
            ((option.SliderHeight * 2) - option.SliderHeight) / 2, option.BackgroundBorder, provider.AnimationSpeed, context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgScriptableTransformerOption((rect, _, _, _) =>
                {
                    Rect thumbRect = thumb.ElementRect.Get();
                    rect.width = thumbRect.x + thumbRect.width;
                    return rect;
                }));
                fillGetter.Speed          = provider.AnimationSpeed;
                fillGetter.RenderCallback = context.RectGetProvider;
                fillHoverObserver.Getter  = fillGetter;
                fillEventHandler.Register(fillGetter);
            }, fillEventHandler);
        OgTextureElement background = m_BackgroundBuilder.Build(name, option.BackgroundColor, option.SliderWidth, option.SliderHeight, 0,
            ((option.SliderHeight * 2) - option.SliderHeight) / 2, option.BackgroundBorder);
        IOgSlider<IOgVisualElement> slider = m_SliderBuilder.Build(name, new([thumbObserver, thumbOutlineObserver, textObserver]), initial, min, max,
            new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
            {
                if(observer is not null) context.Observable.AddObserver(observer);
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.SliderWidth, option.SliderHeight * 2))
                       .SetOption(new OgFlexiblePositionTransformerOption()).SetOption(new OgMarginTransformerOption(0, elementY));
                context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                context.Element.IsInteractingObserver?.AddObserver(thumbOutlineInteractObserver);
                context.Element.IsHoveringObserver?.AddObserver(thumbHoverObserver);
                context.Element.IsHoveringObserver?.AddObserver(thumbOutlineHoverObserver);
                context.Element.IsHoveringObserver?.AddObserver(fillHoverObserver);
                context.Observable.Notify(initial);
                context.Element.IsHoveringObserver?.Notify(false);
            }));
        slider.Add(background);
        slider.Add(fill);
        slider.Add(thumbOutline);
        slider.Add(thumb);
        slider.Add(valueText);
        container.Add(slider);
        return container;
    }
}