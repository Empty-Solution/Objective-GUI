using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Generic;
using EH.Builder.Config;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhButtonBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhBaseTextBuilder textBuilder,
    EhBaseButtonBuilder buttonBuilder)
{
    public IOgInteractableElement<IOgVisualElement> Build(IDkGetProvider<string> name, Action action, float x, float y)
    {
        EhButtonConfig             buttonConfig   = provider.ButtonConfig;
        DkScriptableObserver<bool> actionObserver = new();
        actionObserver.OnUpdate += state =>
        {
            if(state) return;
            action.Invoke();
        };
        IOgInteractableElement<IOgVisualElement> button = buttonBuilder.Build(name.Get(), new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(buttonConfig.Width, buttonConfig.Height))
                   .SetOption(new OgMarginTransformerOption(x, y))
                   .SetOption(new OgMarginTransformerOption(0, (provider.InteractableElementConfig.Height - buttonConfig.Height) / 2));
        }));
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? buttonConfig.BackgroundHoverColor.Get() : buttonConfig.BackgroundColor.Get();
        });
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? buttonConfig.BackgroundInteractColor.Get() :
                                    button.IsHovering ? buttonConfig.BackgroundHoverColor.Get() : buttonConfig.BackgroundColor.Get();
        });
        button.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
        button.IsInteractingObserver?.AddObserver(backgroundInteractObserver);
        button.IsHoveringObserver?.Notify(false);
        button.IsInteractingObserver?.Notify(false);
        button.IsInteractingObserver?.AddObserver(actionObserver);
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgTextureElement background = backgroundBuilder.Build(name.Get(), backgroundGetter, buttonConfig.Width, buttonConfig.Height, 0, 0,
            new(buttonConfig.BackgroundBorder, buttonConfig.BackgroundBorder, buttonConfig.BackgroundBorder, buttonConfig.BackgroundBorder), context =>
            {
                backgroundGetter.Speed            = provider.AnimationSpeed;
                backgroundHoverObserver.Getter    = backgroundGetter;
                backgroundInteractObserver.Getter = backgroundGetter;
                backgroundGetter.RenderCallback   = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        OgTextElement buttonText = textBuilder.Build($"{name}Text", buttonConfig.TextColor, name, buttonConfig.TextFontSize, buttonConfig.TextAlignment,
            buttonConfig.Width, buttonConfig.Height);
        button.Add(background);
        button.Add(buttonText);
        return button;
    }
}