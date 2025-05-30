using DK.Getting.Generic;
using DK.Property.Observing.Abstraction.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Extensions;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public abstract class EhInternalBindableBuilder<TValue>(EhConfigProvider provider, EhBackgroundBuilder backgroundBuilder,
    EhContainerBuilder containerBuilder, EhBaseModalInteractableBuilder interactableBuilder, EhBaseBindableBuilder<TValue> bindableBuilder)
{
    public IOgContainer<IOgElement> Build(string name, IDkObservableProperty<TValue> value, float x, float y, float width, float height)
    {
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
        }));
        return container;
    }
}
public class EhSliderBuilder(EhConfigProvider provider, EhBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseFillBuilder baseFillBuilder, EhBaseTextBuilder textBuilder, EhBaseThumbBuilder thumbBuilder, EhBaseHorizontalSliderBuilder sliderBuilder)
{
    public IOgContainer<IOgElement> Build(string name, IDkObservableProperty<float> value, float min, float max, string textFormat, int round, float y)
    {
        EhSliderConfig sliderConfig = provider.SliderConfig;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
        }));
        OgTextElement nameText = textBuilder.BuildStaticText(name, sliderConfig.TextColor, name, sliderConfig.NameFontSize, sliderConfig.NameAlignment,
            provider.InteractableElementConfig.Width - sliderConfig.Width, provider.InteractableElementConfig.Height);
        container.Add(nameText);
        float elementY = (provider.InteractableElementConfig.Height - (sliderConfig.Height * 2)) / 2;
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            float offset = sliderConfig.ThumbSize / 6;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbOutlineInteractObserver = new((getter, value) =>
        {
            float offset = sliderConfig.ThumbOutlineSize / 8;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * sliderConfig.Width) - (sliderConfig.ThumbSize / 2);
            return rect;
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbOutlineObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * sliderConfig.Width) - (sliderConfig.ThumbOutlineSize / 2);
            return rect;
        });
        OgTextElement valueText = textBuilder.BuildSliderValueText(name, sliderConfig.TextColor, textFormat, value, round, sliderConfig.ValueFontSize,
            sliderConfig.ValueAlignment, sliderConfig.Width, sliderConfig.Height * 2, 0,
            (-(provider.InteractableElementConfig.Height - sliderConfig.Height) / 2) - (provider.InteractableElementConfig.VerticalPadding / 2));
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbOutlineHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? sliderConfig.ThumbOutlineHoverColor.Get() : sliderConfig.ThumbOutlineColor.Get();
        });
        OgEventHandlerProvider thumbOutlineEventHandler = new();
        OgAnimationColorGetter thumbOutlineGetter       = new(thumbOutlineEventHandler);
        OgTextureElement thumbOutline = thumbBuilder.Build($"{name}ThumbOutline", thumbOutlineGetter, thumbOutlineObserver, thumbOutlineInteractObserver,
            sliderConfig.ThumbOutlineSize, 0, ((sliderConfig.Height * 2) - sliderConfig.ThumbOutlineSize) / 2, sliderConfig.ThumbBorder,
            provider.AnimationSpeed, thumbOutlineEventHandler, context =>
            {
                thumbOutlineGetter.Speed          = provider.AnimationSpeed;
                thumbOutlineGetter.RenderCallback = context.RectGetProvider;
                thumbOutlineHoverObserver.Getter  = thumbOutlineGetter;
                thumbOutlineEventHandler.Register(thumbOutlineGetter);
            });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? sliderConfig.ThumbHoverColor.Get() : sliderConfig.ThumbColor.Get();
        });
        OgEventHandlerProvider thumbEventHandler = new();
        OgAnimationColorGetter thumbGetter       = new(thumbEventHandler);
        OgTextureElement thumb = thumbBuilder.Build($"{name}Thumb", thumbGetter, thumbObserver, thumbInteractObserver, sliderConfig.ThumbSize, 0,
            ((sliderConfig.Height * 2) - sliderConfig.ThumbSize) / 2, sliderConfig.ThumbBorder, provider.AnimationSpeed, thumbEventHandler, context =>
            {
                thumbGetter.Speed          = provider.AnimationSpeed;
                thumbGetter.RenderCallback = context.RectGetProvider;
                thumbHoverObserver.Getter  = thumbGetter;
                thumbEventHandler.Register(thumbGetter);
            });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> fillHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? sliderConfig.FillHoverColor.Get() : sliderConfig.FillColor.Get();
        });
        OgEventHandlerProvider fillEventHandler = new();
        OgAnimationColorGetter fillGetter       = new(fillEventHandler);
        OgTextureElement fill = baseFillBuilder.Build(name, fillGetter, 0, sliderConfig.Height, 0, ((sliderConfig.Height * 2) - sliderConfig.Height) / 2,
            sliderConfig.BackgroundBorder, provider.AnimationSpeed, context =>
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
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? sliderConfig.BackgroundHoverColor.Get() : sliderConfig.BackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgTextureElement background = backgroundBuilder.Build(name, backgroundGetter, sliderConfig.Width, sliderConfig.Height, 0,
            ((sliderConfig.Height * 2) - sliderConfig.Height) / 2,
            new(sliderConfig.BackgroundBorder, sliderConfig.BackgroundBorder, sliderConfig.BackgroundBorder, sliderConfig.BackgroundBorder), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        IOgSlider<IOgVisualElement> slider = sliderBuilder.Build(name, value, min, max, new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
        {
            context.ValueProvider.AddObserver(thumbObserver);
            context.ValueProvider.AddObserver(thumbOutlineObserver);
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(sliderConfig.Width, sliderConfig.Height * 2))
                   .SetOption(new OgFlexiblePositionTransformerOption()).SetOption(new OgMarginTransformerOption(0, elementY));
            context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
            context.Element.IsInteractingObserver?.AddObserver(thumbOutlineInteractObserver);
            context.Element.IsHoveringObserver?.AddObserver(thumbHoverObserver);
            context.Element.IsHoveringObserver?.AddObserver(thumbOutlineHoverObserver);
            context.Element.IsHoveringObserver?.AddObserver(fillHoverObserver);
            context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
            context.ValueProvider.Set(context.ValueProvider.Get());
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