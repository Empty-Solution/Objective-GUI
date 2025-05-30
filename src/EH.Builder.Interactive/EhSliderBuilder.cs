using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using System;
using System.Linq;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhInternalBindModalBuilder<TValue>(EhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder, 
    EhBaseTextBuilder textBuilder, EhBaseModalInteractableBuilder interactableBuilder, EhBaseButtonBuilder buttonBuilder/*, EhBaseBindableBuilder<TValue> bindableBuilder*/)
{
    public IOgModalInteractable<IOgElement> Build(string name, float x, float y, float width, float height, 
        Action<IDkProperty<TValue>, float> process)
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
        IOgContainer<IOgElement>  container    = new OgInteractableElement<IOgElement>($"{name}Container", eventProvider, getter);
        DkScriptableGetter<float> heightGetter = new(() => interactableConfig.VerticalPadding + (container.Elements.Count() * (interactableConfig.ModalItemHeight + interactableConfig.VerticalPadding)));
        getter.Options.SetOption(new OgGettableSizeTransformerOption(null, heightGetter));
        button.Add(backgroundBuilder.Build($"{name}InteractableBackground", interactableConfig.ModalBackgroundColor, interactableConfig.ModalWidth, 0,
            0, 0, new(), context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgGettableSizeTransformerOption(null, heightGetter));
                context.Element.ZOrder = 2;
            }));
        DkScriptableObserver<bool> addButtonObserver = new();
        addButtonObserver.OnUpdate += state =>
        {
            if(!state) return;
            container.Add(BuildBind($"{name}Bind", container.Elements.Count(), interactableConfig, process));
        };
        container.Add(buttonBuilder.Build($"{name}InteractableAddButton", new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f, interactableConfig.ModalItemHeight))
                   .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth * 0.05f));
            context.Element.IsInteractingObserver?.AddObserver(addButtonObserver);
            context.Element.IsInteractingObserver?.Notify(false);
            context.Element.SortOrder = 2;
        })));
        button.Add(container);
        return button;
    }
    private IOgContainer<IOgElement> BuildBind(string name, float bindIndex, EhInteractableElementConfig interactableConfig, 
        Action<IDkProperty<TValue>, float> process)
    {
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}{bindIndex}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f, interactableConfig.ModalItemHeight))
                       .SetOption(new OgMarginTransformerOption(interactableConfig.ModalWidth * 0.05f, 
                           interactableConfig.VerticalPadding + (bindIndex * (interactableConfig.ModalItemHeight + interactableConfig.VerticalPadding))));
            }));
        IOgModalInteractable<IOgElement> button = interactableBuilder.Build($"{name}{bindIndex}Interactable", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(interactableConfig.ModalWidth * 0.9f,
                    interactableConfig.ModalItemHeight));
            }));
        
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? interactableConfig.ModalButtonBackgroundHoverColor.Get() : interactableConfig.ModalButtonBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        container.Add(backgroundBuilder.Build($"{name}{bindIndex}InteractableBackground", backgroundGetter, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0, new(), context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
                context.Element.ZOrder = 3;
            }, backgroundEventHandler));
        container.Add(textBuilder.BuildStaticText($"{name}{bindIndex}InteractableText", interactableConfig.ModalButtonTextColor, $"Bind {bindIndex}",
            interactableConfig.ModalButtonTextFontSize, interactableConfig.ModalButtonTextAlignment, interactableConfig.ModalWidth * 0.9f,
            interactableConfig.ModalItemHeight, 0, 0, context =>
            {
                context.Element.ZOrder = 3;
            }));
        button.Add(backgroundBuilder.Build($"{name}{bindIndex}InteractableBackground", interactableConfig.ModalBindBackgroundColor, interactableConfig.BindModalWidth, 
            (interactableConfig.VerticalPadding * 2) + ((interactableConfig.BindModalItemHeight + interactableConfig.VerticalPadding) * 3),
            interactableConfig.ModalWidth, 0, new(), context =>
            {
                context.Element.ZOrder = 4;
            }));

        container.Add(button);
        return container;
    }
}
public class EhSliderBuilder(EhConfigProvider provider,
    EhContainerBuilder containerBuilder, EhBaseTextBuilder textBuilder, EhInternalSliderBuilder sliderBuilder, EhInternalBindModalBuilder<float> bindModalBuilder)
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
        container.Add(sliderBuilder.Build(name, value, min, max, textFormat, round));
        container.Add(bindModalBuilder.Build(name, provider.InteractableElementConfig.Width - sliderConfig.Width, 
            (provider.InteractableElementConfig.Height - (sliderConfig.Height * 2)) / 2, provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height,
            (property, itemY) =>
            {
                
            }));
        return container;
    }
}