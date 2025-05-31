using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
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
namespace EH.Builder.Interactive.Internal;
public class EhInternalBindModalBuilder<TValue>(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseTextBuilder textBuilder, EhBaseModalInteractableBuilder interactableBuilder, EhBaseButtonBuilder buttonBuilder,
    EhBaseBindableBuilder<TValue> bindableBuilder)
{
    public IOgModalInteractable<IOgElement> Build(string name, float x, float y, float width, float height, IEhProperty<TValue> value,
        Func<IDkObservableProperty<TValue>, IOgContainer<IOgVisualElement>> process)
    {
        EhInteractableElementConfig interactableConfig = provider.InteractableElementConfig;
        IOgModalInteractable<IOgElement> button = interactableBuilder.Build($"{name}Interactable", true,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
            }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth)).SetOption(new OgMarginTransformerOption(width, height));
        IOgContainer<IOgElement> container = new OgInteractableElement<IOgElement>($"{name}Container", eventProvider, getter);
        getter.LayoutCallback = container;
        DkScriptableGetter<float> heightGetter = new(() =>
            interactableConfig.VerticalPadding + (container.Elements.Count() * (interactableConfig.ModalItemHeight + interactableConfig.VerticalPadding)));
        getter.Options.SetOption(new OgGettableSizeTransformerOption(null, heightGetter));
        button.Add(backgroundBuilder.Build($"{name}Interactable", interactableConfig.ModalBackgroundColor, interactableConfig.ModalWidth, 0, width, height,
            new(interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder,
                interactableConfig.ModalBackgroundBorder), context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgGettableSizeTransformerOption(null, heightGetter));
                context.Element.ZOrder = 2;
            }));
        DkScriptableObserver<bool> addButtonObserver = new();
        addButtonObserver.OnUpdate += state =>
        {
            if(state) return;
            if(container.Elements.Count() > 10) return;
            container.Add(BuildBind($"{name}Bind", container.Elements.Count(), interactableConfig, container, value.ValueOverride,
                value.CreateKeybind(KeyCode.None), process));
        };
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.ModalButtonBackgroundHoverColor.Get() : interactableConfig.ModalButtonBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        OgTextureElement addButtonBackground = backgroundBuilder.Build($"{name}InteractableAddButton", backgroundGetter,
            interactableConfig.ModalWidth * 0.9f, interactableConfig.ModalItemHeight, 0, 0,
            new(interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder,
                interactableConfig.ModalButtonBorder), context =>
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
                context.Element.Order = int.MinValue;
                context.Element.Sort  = false;
            }));
        addButton.Add(addButtonBackground);
        addButton.Add(addButtonText);
        button.Add(container);
        foreach(IDkProperty<KeyCode> keybind in value.Keybinds)
            container.Add(BuildBind($"{name}Bind", container.Elements.Count(), interactableConfig, container, value.ValueOverride, keybind, process));
        container.Add(addButton);
        return button;
    }
    private IOgContainer<IOgElement> BuildBind(string name, int bindIndex, EhInteractableElementConfig interactableConfig,
        IOgContainer<IOgElement> sourceContainer, IEhValueOverride<TValue> valueOverride, IDkProperty<KeyCode> keybind,
        Func<IDkObservableProperty<TValue>, IOgContainer<IOgVisualElement>> process)
    {
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}{bindIndex}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f, interactableConfig.ModalItemHeight))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth * 0.05f))
                       .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, interactableConfig.VerticalPadding));
            }));
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.ModalButtonBackgroundHoverColor.Get() : interactableConfig.ModalButtonBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        container.Add(backgroundBuilder.Build($"{name}{bindIndex}Interactable", backgroundGetter, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0,
            new(interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder,
                interactableConfig.ModalButtonBorder), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
                context.Element.ZOrder = 3 + bindIndex;
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
                context.Element.ZOrder = 3 + bindIndex;
            }));
        float modalHeight = interactableConfig.VerticalPadding + ((interactableConfig.Height + interactableConfig.VerticalPadding) * 3);
        button.Add(backgroundBuilder.Build($"{name}{bindIndex}Interactable", interactableConfig.ModalBackgroundColor, interactableConfig.BindModalWidth,
            modalHeight, interactableConfig.ModalWidth, 0,
            new(interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder, interactableConfig.ModalBackgroundBorder,
                interactableConfig.ModalBackgroundBorder), context =>
            {
                context.Element.ZOrder = 3 + bindIndex;
            }));
        OgEventHandlerProvider  eventProvider = new();
        OgTransformerRectGetter getter        = new(eventProvider, new OgOptionsContainer());
        getter.Options.SetOption(new OgSizeTransformerOption(interactableConfig.BindModalWidth, modalHeight))
              .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth));
        IOgContainer<IOgElement> interactable = new OgInteractableElement<IOgElement>($"{name}{bindIndex}Interactable", eventProvider, getter);
        getter.LayoutCallback = interactable;
        IOgContainer<IOgElement> bindContainer = containerBuilder.Build($"{name}BindContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(interactableConfig.BindModalWidth - (interactableConfig.HorizontalPadding * 2),
                           interactableConfig.Height))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.HorizontalPadding, interactableConfig.VerticalPadding));
            }));
        OgTextElement bindNameText = textBuilder.BuildStaticText($"{name}{bindIndex}BindText", interactableConfig.BindModalTextColor, "Key",
            interactableConfig.BindModalTextFontSize, interactableConfig.BindModalTextAlignment,
            interactableConfig.BindModalWidth - interactableConfig.BindWidth - (interactableConfig.HorizontalPadding * 2), interactableConfig.Height, 0, 0,
            context =>
            {
                context.Element.ZOrder = 4 + bindIndex;
            });
        bindContainer.Add(bindNameText);
        DkScriptableGetter<string> bindNameGetter = new(() => keybind.Get().ToString());
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> bindHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.BindBackgroundHoverColor.Get() : interactableConfig.BindBackgroundColor.Get();
        });
        OgEventHandlerProvider bindEventHandler     = new();
        OgAnimationColorGetter bindBackgroundGetter = new(bindEventHandler);
        OgTextureElement bindBackground = backgroundBuilder.Build($"{name}{bindIndex}BindBackground", bindBackgroundGetter, interactableConfig.BindWidth,
            interactableConfig.BindHeight, 0, 0,
            new(interactableConfig.BindBorder, interactableConfig.BindBorder, interactableConfig.BindBorder, interactableConfig.BindBorder), context =>
            {
                bindBackgroundGetter.Speed          = provider.AnimationSpeed;
                bindHoverObserver.Getter            = bindBackgroundGetter;
                bindBackgroundGetter.RenderCallback = context.RectGetProvider;
                bindEventHandler.Register(bindBackgroundGetter);
                context.Element.ZOrder = 4 + bindIndex;
            }, bindEventHandler);
        OgTextElement bindText = textBuilder.BuildBindableText($"{name}{bindIndex}BindText", interactableConfig.BindTextColor, bindNameGetter,
            interactableConfig.BindTextFontSize, interactableConfig.BindTextAlignment, interactableConfig.BindWidth, interactableConfig.BindHeight, 0, 0,
            context =>
            {
                context.Element.ZOrder = 4 + bindIndex;
            });
        DkScriptableObserver<TValue> observer = new();
        observer.OnUpdate += valueOverride.Notify;
        DkObservableProperty<TValue> overrideValue = new(new DkObservable<TValue>([observer]), default!);
        IOgInteractableValueElement<IOgVisualElement, TValue> bind = bindableBuilder.Build($"{name}{bindIndex}Bind", overrideValue, valueOverride, keybind,
            new OgScriptableBuilderProcess<OgBindableBuildContext<TValue>>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.BindWidth, interactableConfig.BindHeight))
                       .SetOption(new OgMarginTransformerOption(
                           interactableConfig.BindModalWidth - interactableConfig.BindWidth - (interactableConfig.HorizontalPadding * 2),
                           (interactableConfig.Height - interactableConfig.BindHeight) / 2));
                context.Element.IsHoveringObserver?.AddObserver(bindHoverObserver);
                context.Element.IsHoveringObserver?.Notify(false);
            }));
        bind.Add(bindBackground);
        bind.Add(bindText);
        bindContainer.Add(bind);
        IOgContainer<IOgElement> elementContainer = containerBuilder.Build($"{name}ElementContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(interactableConfig.BindModalWidth - (interactableConfig.HorizontalPadding * 2),
                           interactableConfig.Height)).SetOption(new OgMarginTransformerOption(interactableConfig.HorizontalPadding,
                           interactableConfig.Height + (interactableConfig.VerticalPadding * 2)));
            }));
        IOgContainer<IOgVisualElement>? processedElement                              = process(overrideValue);
        foreach(IOgVisualElement element in processedElement.Elements) element.ZOrder = 4 + bindIndex;
        OgSizeTransformerOption? option =
            (OgSizeTransformerOption?)((OgTransformerRectGetter)processedElement.ElementRect).Options.Options.FirstOrDefault(o =>
                o is OgSizeTransformerOption);
        OgTextElement elementLabel = textBuilder.BuildStaticText($"{name}{bindIndex}ElementLabel", interactableConfig.ModalButtonTextColor, "Value",
            interactableConfig.BindModalTextFontSize, interactableConfig.BindModalTextAlignment,
            interactableConfig.Width - option?.Width ?? 100 - (interactableConfig.HorizontalPadding * 2), interactableConfig.Height, 0, 0, context =>
            {
                context.Element.ZOrder = 4 + bindIndex;
            });
        DkScriptableObserver<bool> removeButtonObserver = new();
        removeButtonObserver.OnUpdate += state =>
        {
            if(state) return;
            sourceContainer.Remove(container);
        };
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> removeBackgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.ModalButtonBackgroundHoverColor.Get() : interactableConfig.ModalButtonBackgroundColor.Get();
        });
        OgEventHandlerProvider removeBackgroundEventHandler = new();
        OgAnimationColorGetter removeBackgroundGetter       = new(removeBackgroundEventHandler);
        OgTextureElement removeButtonBackground = backgroundBuilder.Build($"{name}{bindIndex}InteractableRemoveButton", removeBackgroundGetter,
            interactableConfig.BindModalWidth * 0.9f, interactableConfig.Height, 0, 0,
            new(interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder, interactableConfig.ModalButtonBorder,
                interactableConfig.ModalButtonBorder), context =>
            {
                removeBackgroundGetter.Speed          = provider.AnimationSpeed;
                removeBackgroundHoverObserver.Getter  = removeBackgroundGetter;
                removeBackgroundGetter.RenderCallback = context.RectGetProvider;
                removeBackgroundEventHandler.Register(removeBackgroundGetter);
                context.Element.ZOrder = 4 + bindIndex;
            }, removeBackgroundEventHandler);
        OgTextElement removeButtonText = textBuilder.BuildStaticText($"{name}{bindIndex}InteractableRemoveButtonText",
            interactableConfig.ModalButtonTextColor, "Delete", interactableConfig.ModalButtonTextFontSize, interactableConfig.ModalButtonTextAlignment,
            interactableConfig.BindModalWidth * 0.9f, interactableConfig.Height, 0, 0, context =>
            {
                context.Element.ZOrder = 4 + bindIndex;
            });
        IOgInteractableElement<IOgVisualElement> removeButton = buttonBuilder.Build($"{name}{bindIndex}InteractableRemoveButton",
            new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.BindModalWidth * 0.9f, interactableConfig.Height))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.BindModalWidth * 0.05f,
                           (interactableConfig.Height * 2) + (interactableConfig.VerticalPadding * 3)));
                context.Element.IsInteractingObserver?.AddObserver(removeButtonObserver);
                context.Element.IsHoveringObserver?.AddObserver(removeBackgroundHoverObserver);
                context.Element.IsHoveringObserver?.Notify(false);
            }));
        removeButton.Add(removeButtonBackground);
        removeButton.Add(removeButtonText);
        elementContainer.Add(elementLabel);
        elementContainer.Add(processedElement);
        interactable.Add(bindContainer);
        interactable.Add(elementContainer);
        interactable.Add(removeButton);
        button.Add(interactable);
        container.Add(button);
        return container;
    }
}