using DK.Getting.Generic;
using DK.Property.Observing.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Extensions;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive.Internal;
public class EhInternalToggleBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhBaseFillBuilder baseFillBuilder,
    EhBaseThumbBuilder thumbBuilder, EhBaseToggleBuilder toggleBuilder)
{
    public IOgContainer<IOgVisualElement> Build(string name, IDkObservableProperty<bool> value, float x)
    {
        EhToggleConfig toggleConfig = provider.ToggleConfig;
        float          offset       = (toggleConfig.Height - toggleConfig.ThumbSize) / 2;
        (OgTextureElement thumb, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver,
         OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver,
         OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbHoverObserver) = BuildThumb(name, toggleConfig, value, offset);
        (OgTextureElement fill, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver,
         OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> fillInteractObserver,
         OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> fillHoverObserver) = BuildFill(name, toggleConfig, value, offset);
        (OgTextureElement background, OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver) =
            BuildBackground(name, toggleConfig);
        IOgToggle<IOgVisualElement> toggle = toggleBuilder.Build(name, value, new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
        {
            context.ValueProvider.AddObserver(fillObserver);
            context.ValueProvider.AddObserver(thumbObserver);
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(toggleConfig.Width, toggleConfig.Height))
                   .SetOption(new OgMarginTransformerOption(x)).SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
            context.Element.IsInteractingObserver?.AddObserver(fillInteractObserver);
            context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
            context.Element.IsHoveringObserver?.AddObserver(fillHoverObserver);
            context.Element.IsHoveringObserver?.AddObserver(thumbHoverObserver);
            context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
            context.ValueProvider.Set(context.ValueProvider.Get());
            context.Element.IsHoveringObserver?.Notify(false);
        }));
        toggle.Add(background);
        toggle.Add(fill);
        toggle.Add(thumb);
        return toggle;
    }
    private (OgTextureElement, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool>,
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool>,
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool>) BuildThumb(string name, EhToggleConfig toggleConfig,
            IDkObservableProperty<bool> property, float offset)
    {
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
            new(property.Get() ? toggleConfig.Width - toggleConfig.ThumbSize - offset : offset, 0, 0, 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, toggleConfig.ThumbSize / 6, toggleConfig.ThumbSize / 6);
        });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? toggleConfig.ThumbHoverColor.Get() : toggleConfig.ThumbColor.Get();
        });
        OgEventHandlerProvider thumbEventHandler = new();
        OgAnimationColorGetter thumbGetter       = new(thumbEventHandler);
        OgTextureElement thumb = thumbBuilder.Build($"{name}Thumb", thumbGetter, thumbObserver, thumbInteractObserver, toggleConfig.ThumbSize, 0, offset,
            toggleConfig.ThumbBorder, provider.AnimationSpeed, thumbEventHandler, context =>
            {
                thumbGetter.Speed          = provider.AnimationSpeed;
                thumbHoverObserver.Getter  = thumbGetter;
                thumbGetter.RenderCallback = context.RectGetProvider;
                thumbEventHandler.Register(thumbGetter);
            });
        return (thumb, thumbObserver, thumbInteractObserver, thumbHoverObserver);
    }
    private (OgTextureElement, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool>,
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool>,
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool>) BuildFill(string name, EhToggleConfig toggleConfig,
            IDkObservableProperty<bool> property, float offset)
    {
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver = new((_, value) =>
            new(0, 0, toggleConfig.ThumbSize + offset + (property.Get() ? toggleConfig.Width - toggleConfig.ThumbSize - offset : offset), 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> fillInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
        });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> fillHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? toggleConfig.FillHoverColor.Get() : toggleConfig.FillColor.Get();
        });
        OgEventHandlerProvider fillEventHandler = new();
        OgAnimationColorGetter fillGetter       = new(fillEventHandler);
        OgTextureElement fill = baseFillBuilder.Build(name, fillGetter, 0, toggleConfig.Height, 0, 0, toggleConfig.BackgroundBorder,
            provider.AnimationSpeed, context =>
            {
                fillObserver.Getter         = context.RectGetProvider;
                fillInteractObserver.Getter = context.RectGetProvider;
                fillGetter.Speed            = provider.AnimationSpeed;
                fillHoverObserver.Getter    = fillGetter;
                fillGetter.RenderCallback   = context.RectGetProvider;
                fillEventHandler.Register(fillGetter);
            }, fillEventHandler);
        return (fill, fillObserver, fillInteractObserver, fillHoverObserver);
    }
    private (OgTextureElement, OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool>) BuildBackground(string name,
        EhToggleConfig toggleConfig)
    {
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? toggleConfig.BackgroundHoverColor.Get() : toggleConfig.BackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgTextureElement background = backgroundBuilder.Build(name, backgroundGetter, toggleConfig.Width, toggleConfig.Height, 0, 0,
            new(toggleConfig.BackgroundBorder, toggleConfig.BackgroundBorder, toggleConfig.BackgroundBorder, toggleConfig.BackgroundBorder), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        return (background, backgroundHoverObserver);
    }
}