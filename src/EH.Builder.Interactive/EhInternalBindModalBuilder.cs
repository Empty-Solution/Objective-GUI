using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Generic;
using DK.Property.Observing.Abstraction.Generic;
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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhInternalBindModalBuilder<TValue>(EhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder, 
    EhBaseTextBuilder textBuilder, EhBaseModalInteractableBuilder interactableBuilder, EhBaseButtonBuilder buttonBuilder, EhBaseBindableBuilder<TValue> bindableBuilder)
{
    public IOgModalInteractable<IOgElement> Build(string name, float x, float y, float width, float height, IDkValueOverride<TValue> valueOverride, 
        IDkObservableProperty<TValue> overrideValue, Func<IOgContainer<IOgVisualElement>> process)
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
            container.Add(BuildBind($"{name}Bind", container.Elements.Count(), interactableConfig, valueOverride, overrideValue, process));
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
    private IOgContainer<IOgElement> BuildBind(string name, float bindIndex, EhInteractableElementConfig interactableConfig, IDkValueOverride<TValue> valueOverride, IDkObservableProperty<TValue> overrideValue, 
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
        float modalHeight = (interactableConfig.VerticalPadding * 2) + ((interactableConfig.BindModalItemHeight + interactableConfig.VerticalPadding) * 2);
        button.Add(backgroundBuilder.Build($"{name}{bindIndex}Interactable", interactableConfig.ModalBackgroundColor, interactableConfig.BindModalWidth, modalHeight,
            interactableConfig.ModalWidth, 0, new(interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder), context =>
            {
                context.Element.ZOrder = 3;
            }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options.SetOption(new OgSizeTransformerOption(interactableConfig.BindModalWidth, modalHeight))
              .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth));
        IOgContainer<IOgElement> interactable = new OgInteractableElement<IOgElement>($"{name}{bindIndex}Interactable", eventProvider, getter);
        getter.LayoutCallback = interactable;
        button.Add(interactable);

        IOgContainer<IOgElement> bindContainer = containerBuilder.Build($"{name}BindContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(
                new OgSizeTransformerOption(interactableConfig.BindModalWidth - (interactableConfig.HorizontalPadding * 2), interactableConfig.BindModalItemHeight))
                   .SetOption(new OgMarginTransformerOption(interactableConfig.HorizontalPadding, interactableConfig.VerticalPadding));
        }));
        OgTextElement bindNameText = textBuilder.BuildStaticText($"{name}{bindIndex}BindText", interactableConfig.BindModalTextColor, "Key",
            interactableConfig.BindModalTextFontSize, interactableConfig.BindModalTextAlignment, interactableConfig.BindModalWidth - interactableConfig.BindWidth,
            interactableConfig.BindModalItemHeight, 0, 0, context =>
            {
                context.Element.ZOrder = 4;
            });
        bindContainer.Add(bindNameText);
        DkProperty<KeyCode?>       keycode           = new(KeyCode.F1);
        DkScriptableGetter<string> bindNameGetter = new(() => keycode.Get() is null ? "No bind" : keycode.Get().ToString());
        IOgInteractableValueElement<IOgVisualElement, TValue> bind = bindableBuilder.Build($"{name}{bindIndex}Bind", overrideValue, valueOverride, keycode,
            new OgScriptableBuilderProcess<OgBindableBuildContext<TValue>>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.BindWidth, interactableConfig.BindHeight))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.BindModalWidth - interactableConfig.BindWidth,
                           (interactableConfig.Height - interactableConfig.BindHeight) / 2));
            }));

        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> bindHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.BindBackgroundHoverColor.Get() : interactableConfig.BindBackgroundColor.Get();
        });
        OgEventHandlerProvider bindEventHandler = new();
        OgAnimationColorGetter bindBackgroundGetter = new(bindEventHandler);
        OgTextureElement bindBackground = backgroundBuilder.Build($"{name}{bindIndex}BindBackground", bindBackgroundGetter, interactableConfig.BindWidth,
            interactableConfig.BindHeight, 0, 0, 
            new(interactableConfig.BindBorder, interactableConfig.BindBorder, interactableConfig.BindBorder, interactableConfig.BindBorder), 
            context =>
            {
                bindBackgroundGetter.Speed = provider.AnimationSpeed;
                bindHoverObserver.Getter = bindBackgroundGetter;
                bindBackgroundGetter.RenderCallback = context.RectGetProvider;
                bindEventHandler.Register(bindBackgroundGetter);
                context.Element.ZOrder = 5;
            }, bindEventHandler);
        OgTextElement bindText = textBuilder.BuildBindableText($"{name}{bindIndex}BindText", interactableConfig.BindTextColor, bindNameGetter,
            interactableConfig.BindTextFontSize, interactableConfig.BindTextAlignment, interactableConfig.BindWidth,
            interactableConfig.BindHeight, 0, 0, context =>
            {
                context.Element.ZOrder = 5;
            });

        bind.Add(bindBackground);
        bind.Add(bindText);

        IOgContainer<IOgElement> elementContainer = containerBuilder.Build($"{name}ElementContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(
                       new OgSizeTransformerOption(interactableConfig.BindModalWidth - (interactableConfig.HorizontalPadding * 2), interactableConfig.BindModalItemHeight))
                   .SetOption(new OgMarginTransformerOption(interactableConfig.HorizontalPadding, interactableConfig.Height + interactableConfig.VerticalPadding));
        }));
        OgTextElement elementLabel = textBuilder.BuildStaticText($"{name}{bindIndex}ElementLabel", interactableConfig.ModalButtonTextColor, "Value",
            interactableConfig.BindModalTextFontSize, interactableConfig.BindModalTextAlignment, interactableConfig.Width - 100,
            interactableConfig.BindModalItemHeight, 0, 0, context =>
            {
                context.Element.ZOrder = 4;
            });
        elementContainer.Add(elementLabel);
        IOgContainer<IOgVisualElement>? processedElement = process();
        foreach(IOgVisualElement element in processedElement.Elements)
                element.ZOrder = 4;
        elementContainer.Add(processedElement);
        interactable.Add(bindContainer);
        interactable.Add(elementContainer);
        container.Add(button);
        return container;
    }
}