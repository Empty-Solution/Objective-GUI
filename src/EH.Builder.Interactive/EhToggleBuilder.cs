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
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhToggleBuilder(IEhVisualOption context) : IEhToggleBuilder
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly EhFillBuilder           m_FillBuilder       = new();
    private readonly EhOptionsProvider       m_OptionsProvider   = new();
    private readonly EhTextBuilder           m_TextBuilder       = new(context);
    private readonly EhThumbBuilder          m_ThumbBuilder      = new();
    private readonly EhInternalToggleBuilder m_ToggleBuilder     = new();
    public IOgContainer<IOgElement> Build(string name, bool initial, DkObserver<bool>? observer = null) =>
        Build(name, initial, observer, m_OptionsProvider);
    private IOgContainer<IOgElement> Build(string name, bool initial, DkObserver<bool>? observer, EhOptionsProvider provider)
    {
        EhToggleOption option = provider.ToggleOption;
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgMinSizeTransformerOption(provider.InteractableElementOption.Width, provider.InteractableElementOption.Height))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, provider.InteractableElementOption.Padding));
            }));
        float offset = (option.Height - option.ThumbSize) / 2;
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
            new(value ? option.Width - option.ThumbSize - offset : offset, 0, 0, 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, option.ThumbSize / 6, option.ThumbSize / 6);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver = new((_, value) =>
            new(0, 0, option.ThumbSize + offset + (value ? option.Width - option.ThumbSize - offset : offset), 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> fillInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
        });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> thumbHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.ThumbHoverColor.Get() : option.ThumbColor.Get();
        });
        OgEventHandlerProvider thumbEventHandler = new();
        OgAnimationColorGetter thumbGetter       = new(thumbEventHandler);
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", thumbGetter, thumbObserver, thumbInteractObserver, option.ThumbSize, 0, offset,
            option.ThumbBorder, provider.AnimationSpeed, thumbEventHandler, context =>
            {
                thumbGetter.Speed          = provider.AnimationSpeed;
                thumbHoverObserver.Getter  = thumbGetter;
                thumbGetter.RenderCallback = context.RectGetProvider;
                thumbEventHandler.Register(thumbGetter);
            });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> fillHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.FillHoverColor.Get() : option.FillColor.Get();
        });
        OgEventHandlerProvider fillEventHandler = new();
        OgAnimationColorGetter fillGetter       = new(thumbEventHandler);
        OgTextureElement fill = m_FillBuilder.Build(name, fillGetter, 0, option.Height, 0, 0, option.BackgroundBorder, provider.AnimationSpeed, context =>
        {
            fillObserver.Getter         = context.RectGetProvider;
            fillInteractObserver.Getter = context.RectGetProvider;
            fillGetter.Speed            = provider.AnimationSpeed;
            fillHoverObserver.Getter    = fillGetter;
            fillGetter.RenderCallback   = context.RectGetProvider;
            fillEventHandler.Register(fillGetter);
        }, fillEventHandler);
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? option.BackgroundHoverColor.Get() : option.BackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgTextureElement background = m_BackgroundBuilder.Build(name, backgroundGetter, option.Width, option.Height, 0, 0, option.BackgroundBorder,
            context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        IOgToggle<IOgVisualElement> toggle = m_ToggleBuilder.Build(name, initial, new([fillObserver, thumbObserver]),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                if(observer is not null) context.Observable.AddObserver(observer);
                context.RectGetProvider.Options.SetOption(new OgMinSizeTransformerOption(option.Width, option.Height))
                       .SetOption(new OgFlexiblePositionTransformerOption()).SetOption(new OgAlignmentTransformerOption(TextAnchor.MiddleLeft));
                context.Element.IsInteractingObserver?.AddObserver(fillInteractObserver);
                context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                context.Element.IsHoveringObserver?.AddObserver(fillHoverObserver);
                context.Element.IsHoveringObserver?.AddObserver(thumbHoverObserver);
                context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
                context.Observable.Notify(initial);
                context.Element.IsHoveringObserver?.Notify(false);
            }));
        toggle.Add(background);
        toggle.Add(fill);
        toggle.Add(thumb);
        OgTextElement text = m_TextBuilder.BuildStaticText(name, option.TextColor, name, option.FontSize, option.NameAlignment,
            provider.InteractableElementOption.Width - option.Width, option.Height);
        container.Add(text);
        container.Add(toggle);
        return container;
    }
}