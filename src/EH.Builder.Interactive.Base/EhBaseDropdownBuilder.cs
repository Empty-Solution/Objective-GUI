using DK.Binding.Abstraction;
using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Observing.Abstraction.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataTypes.Orientation;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive.Base;
public abstract class EhBaseDropdownBuilder(EhBaseBackgroundBuilder backgroundBuilder, EhBaseButtonBuilder buttonBuilder, EhBaseTextBuilder textBuilder)
{
    protected IOgInteractableElement<IOgVisualElement> BuildDropdownItem(IDkGetProvider<string> name, int index, IDkProperty<int> selected,
        OgAnimationColorGetter textGetter, OgEventHandlerProvider textEventHandler, IDkObserver<bool> textObserver, IDkBinding binding,
        IEhConfigProvider provider)
    {
        EhDropdownConfig           dropdownConfig = provider.DropdownConfig;
        DkScriptableObserver<bool> observer       = new();
        observer.OnUpdate += state =>
        {
            if(state) return;
            binding.Sync();
            selected.Set(index);
        };
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundHoverObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = value ? dropdownConfig.ItemBackgroundHoverColor.Get() : dropdownConfig.ItemBackgroundColor.Get();
        });
        OgEventHandlerProvider backgroundEventHandler = new();
        OgAnimationColorGetter backgroundGetter       = new(backgroundEventHandler);
        backgroundHoverObserver.Getter = backgroundGetter;
        OgTextureElement background = backgroundBuilder.Build($"{name}Background", backgroundGetter, dropdownConfig.Width * 0.9f,
            dropdownConfig.ModalItemHeight, 0, 0, new(dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border, dropdownConfig.Border),
            context =>
            {
                backgroundGetter.Speed          = provider.AnimationSpeed;
                backgroundHoverObserver.Getter  = backgroundGetter;
                backgroundGetter.RenderCallback = context.RectGetProvider;
                backgroundEventHandler.Register(backgroundGetter);
            }, backgroundEventHandler);
        background.ZOrder = 2;
        OgTextElement text = textBuilder.Build($"{name}Text", textGetter, name, dropdownConfig.ItemTextFontSize, dropdownConfig.ItemTextAlignment,
            dropdownConfig.Width * 0.9f, dropdownConfig.ModalItemHeight, 0, 0, context =>
            {
                textGetter.Speed          = provider.AnimationSpeed;
                textGetter.RenderCallback = context.RectGetProvider;
                textEventHandler.Register(textGetter);
            }, textEventHandler);
        text.ZOrder = 2;
        IOgInteractableElement<IOgVisualElement> button = buttonBuilder.Build(name.Get(), new OgScriptableBuilderProcess<OgButtonBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(dropdownConfig.Width * 0.9f, dropdownConfig.ModalItemHeight))
                   .SetOption(new OgMarginTransformerOption(dropdownConfig.Width * 0.05f))
                   .SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL, dropdownConfig.ModalItemPadding));
            context.Element.IsInteractingObserver?.AddObserver(textObserver);
            context.Element.IsInteractingObserver?.AddObserver(observer);
            context.Element.IsHoveringObserver?.AddObserver(backgroundHoverObserver);
            context.Element.IsHoveringObserver?.Notify(false);
        }));
        textObserver.Update(selected.Get() != index);
        button.Add(background);
        button.Add(text);
        return button;
    }
}