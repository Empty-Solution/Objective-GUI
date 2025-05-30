using DK.Getting.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Observing.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using System;
using System.Linq;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhInternalBindModalBuilder<TValue>(EhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder, 
    EhBaseTextBuilder textBuilder, EhBaseModalInteractableBuilder interactableBuilder, EhBaseButtonBuilder buttonBuilder, EhBaseBindableBuilder<TValue> bindableBuilder)
{
    public IOgModalInteractable<IOgElement> Build(string name, float x, float y, float width, float height, IDkValueOverride<TValue> valueOverride, Func<IOgContainer<IOgVisualElement>> process)
    {
        EhInteractableElementConfig interactableConfig = provider.InteractableElementConfig;
        IOgModalInteractable<IOgElement> button = interactableBuilder.Build($"{name}Interactable", true,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
            }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth)).SetOption(new OgMarginTransformerOption(0, height));
        IOgContainer<IOgElement> container = new OgInteractableElement<IOgElement>($"{name}Container", eventProvider, getter);
        getter.LayoutCallback = container;
        DkScriptableGetter<float> heightGetter = new(() =>
            interactableConfig.VerticalPadding + (container.Elements.Count() * (interactableConfig.ModalItemHeight + interactableConfig.VerticalPadding)));
        getter.Options.SetOption(new OgGettableSizeTransformerOption(null, heightGetter));
        button.Add(backgroundBuilder.Build($"{name}Interactable", interactableConfig.ModalBackgroundColor, interactableConfig.ModalWidth, 0, 0,
            height, new(interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder), context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgGettableSizeTransformerOption(null, heightGetter));
                context.Element.ZOrder = 2;
            }));
        DkScriptableObserver<bool> addButtonObserver = new();
        addButtonObserver.OnUpdate += state =>
        {
            if(!state) return;
            container.Add(BuildBind($"{name}Bind", container.Elements.Count(), interactableConfig, valueOverride, process));
        };
        
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.ModalButtonBackgroundHoverColor.Get() : interactableConfig.ModalButtonBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgTextureElement addButtonBackground = backgroundBuilder.Build($"{name}InteractableAddButton", backgroundGetter, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0, new(interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
                context.Element.ZOrder = 3;
            }, backgroundEventHandler);
        OgTextElement addButtonText = textBuilder.BuildStaticText($"{name}InteractableAddButtonText", interactableConfig.ModalButtonTextColor, "Add",
            interactableConfig.ModalButtonTextFontSize, interactableConfig.ModalButtonTextAlignment, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0, context =>
            {
                context.Element.ZOrder = 3;
            });
        IOgInteractableElement<IOgVisualElement> addButton = buttonBuilder.Build($"{name}InteractableAddButton",
            new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f, interactableConfig.ModalItemHeight))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth * 0.05f))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, interactableConfig.VerticalPadding));
                context.Element.IsInteractingObserver?.AddObserver(addButtonObserver);
                context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
                context.Element.IsHoveringObserver?.Notify(false);
            }));
        addButton.Add(addButtonBackground);
        addButton.Add(addButtonText);
        button.Add(container);
        container.Add(addButton);
        return button;
    }
    private IOgContainer<IOgElement> BuildBind(string name, float bindIndex, EhInteractableElementConfig interactableConfig, IDkValueOverride<TValue> valueOverride, 
        Func<IOgContainer<IOgVisualElement>> process)
    {
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}{bindIndex}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f, interactableConfig.ModalItemHeight))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth * 0.05f, 
                           interactableConfig.VerticalPadding + (bindIndex * (interactableConfig.ModalItemHeight + interactableConfig.VerticalPadding))));
            }));
        
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.ModalButtonBackgroundHoverColor.Get() : interactableConfig.ModalButtonBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        container.Add(backgroundBuilder.Build($"{name}{bindIndex}Interactable", backgroundGetter, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0, new(interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
                context.Element.ZOrder = 3;
            }, backgroundEventHandler));
        IOgModalInteractable<IOgElement> button = interactableBuilder.Build($"{name}{bindIndex}Interactable", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f,
                    interactableConfig.ModalItemHeight));
                context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
                context.Element.IsHoveringObserver?.Notify(false);
            }));
        container.Add(textBuilder.BuildStaticText($"{name}{bindIndex}InteractableText", interactableConfig.ModalButtonTextColor, $"Bind {bindIndex}",
            interactableConfig.ModalButtonTextFontSize, interactableConfig.ModalButtonTextAlignment, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0, context =>
            {
                context.Element.ZOrder = 3;
            }));
        button.Add(backgroundBuilder.Build($"{name}{bindIndex}Interactable", interactableConfig.ModalBackgroundColor, interactableConfig.BindModalWidth, 
            (interactableConfig.VerticalPadding * 2) + ((interactableConfig.BindModalItemHeight + interactableConfig.VerticalPadding) * 3),
            interactableConfig.ModalWidth, 0, new(interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder), context =>
            {
                context.Element.ZOrder = 3;
            }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options.SetOption(new OgSizeTransformerOption(interactableConfig.BindModalWidth, 
            (interactableConfig.VerticalPadding * 2) + ((interactableConfig.BindModalItemHeight + interactableConfig.VerticalPadding) * 3)));
        IOgContainer<IOgElement> interactable = new OgInteractableElement<IOgElement>($"{name}{bindIndex}Interactable", eventProvider, getter);
        getter.LayoutCallback = interactable;
        button.Add(interactable);
        
        
        
        
        
        
        
        
        
        
        container.Add(button);
        return container;
    }
}