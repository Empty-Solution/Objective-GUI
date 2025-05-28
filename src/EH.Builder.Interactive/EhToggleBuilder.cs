using DK.Getting.Generic;
using DK.Property.Observing.Abstraction.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
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
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhToggleBuilder(EhConfigProvider provider, IEhVisualProvider visualProvider)
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly EhFillBuilder           m_FillBuilder       = new();
    private readonly EhTextBuilder           m_TextBuilder       = new(visualProvider);
    private readonly EhThumbBuilder          m_ThumbBuilder      = new();
    private readonly EhInternalToggleBuilder m_ToggleBuilder     = new();
    public IOgContainer<IOgElement> Build(string name, IDkObservableProperty<bool> value)
    {
        EhToggleConfig           toggleConfig = provider.ToggleConfig;
        IOgContainer<IOgElement> container    = BuildContainer(name);
        OgTextElement            text         = BuildText(name, toggleConfig);
        container.Add(text);
        float offset = (toggleConfig.Height - toggleConfig.ThumbSize) / 2;
        (OgTextureElement thumb, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver,
         OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver,
         OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbHoverObserver) = BuildThumb(name, toggleConfig, offset);
        (OgTextureElement fill, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver,
         OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> fillInteractObserver,
         OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> fillHoverObserver) = BuildFill(name, toggleConfig, offset);
        (OgTextureElement background, OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver) =
            BuildBackground(name, toggleConfig);
        IOgToggle<IOgVisualElement> toggle = m_ToggleBuilder.Build(name, value, new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
        {
            context.ValueProvider.AddObserver(fillObserver);
            context.ValueProvider.AddObserver(thumbObserver);
            context.RectGetProvider.Options.SetOption(new OgMinSizeTransformerOption(toggleConfig.Width, toggleConfig.Height))
                   .SetOption(new OgFlexiblePositionTransformerOption()).SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
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
        container.Add(toggle);
        return container;
    }
    private IOgContainer<IOgElement> BuildContainer(string name) =>
        m_ContainerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, provider.InteractableElementConfig.VerticalPadding))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding));
        }));
    private OgTextElement BuildText(string name, EhToggleConfig toggleConfig) =>
        m_TextBuilder.BuildStaticText(name, toggleConfig.TextColor, name, toggleConfig.FontSize, toggleConfig.NameAlignment,
            provider.InteractableElementConfig.Width - toggleConfig.Width, provider.InteractableElementConfig.Height);
    private (OgTextureElement, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool>,
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool>,
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool>) BuildThumb(string name, EhToggleConfig toggleConfig, float offset)
    {
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
            new(value ? toggleConfig.Width - toggleConfig.ThumbSize - offset : offset, 0, 0, 0));
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
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", thumbGetter, thumbObserver, thumbInteractObserver, toggleConfig.ThumbSize, 0, offset,
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
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool>) BuildFill(string name, EhToggleConfig toggleConfig, float offset)
    {
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver = new((_, value) =>
            new(0, 0, toggleConfig.ThumbSize + offset + (value ? toggleConfig.Width - toggleConfig.ThumbSize - offset : offset), 0));
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
        OgTextureElement fill = m_FillBuilder.Build(name, fillGetter, 0, toggleConfig.Height, 0, 0, toggleConfig.BackgroundBorder, provider.AnimationSpeed,
            context =>
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
        OgTextureElement background = m_BackgroundBuilder.Build(name, backgroundGetter, toggleConfig.Width, toggleConfig.Height, 0, 0,
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