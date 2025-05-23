using DK.Observing.Generic;
using EH.Builder.Abstraction;
using EH.Builder.Interactive.Base;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
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
                context.RectGetProvider.Options.SetOption(new OgFlexibleSizeTransformerOption(EOgOrientation.ALL));
            }));
        float offset = (option.ToggleHeight - option.ThumbSize) / 2;
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
            new(value ? option.ToggleWidth - option.ThumbSize - offset : offset, 0, 0, 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, option.ThumbSize / 6, option.ThumbSize / 6);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver = new((_, value) =>
            new(0, 0, option.ThumbSize + offset + (value ? option.ToggleWidth - option.ThumbSize - offset : offset), 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> fillInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
        });
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", option.ThumbColorProperty, thumbObserver, thumbInteractObserver, option.ThumbSize, 0,
            offset, option.ThumbBorder, provider.AnimationSpeed, option.m_ThumbColorBindings);
        OgTextureElement fill = m_FillBuilder.Build(name, option.BackgroundFillColorProperty, 0, option.ToggleHeight, 0, 0, option.BackgroundBorder,
            provider.AnimationSpeed, option.m_BackgroundFillColorBindings, context =>
            {
                fillObserver.Getter         = context.RectGetProvider;
                fillInteractObserver.Getter = context.RectGetProvider;
            });
        IOgToggle<IOgVisualElement> toggle = m_ToggleBuilder.Build(name, initial, new([fillObserver, thumbObserver]),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                if(observer is not null) context.Observable.AddObserver(observer);
                context.RectGetProvider.Options.SetOption(new OgMinSizeTransformerOption(option.ToggleWidth, option.ToggleHeight))
                       .SetOption(new OgFlexiblePositionTransformerOption());
                context.Element.IsInteractingObserver?.AddObserver(fillInteractObserver);
                context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                context.Observable.Notify(initial);
            }));
        OgTextureElement background = m_BackgroundBuilder.Build(name, option.BackgroundColorProperty, option.ToggleWidth, option.ToggleHeight, 0, 0,
            option.BackgroundBorder, option.m_BackgroundColorBindings);
        toggle.Add(background);
        toggle.Add(fill);
        toggle.Add(thumb);
        OgTextElement text = m_TextBuilder.BuildStaticText(name, option.TextColorProperty, name, option.FontSize, option.NameAlignment,
            provider.SubTabOption.SubTabWidth - option.ToggleWidth, option.ToggleHeight, 0, 0, option.m_TextColorBindings);
        container.Add(text);
        container.Add(toggle);
        return container;
    }
}