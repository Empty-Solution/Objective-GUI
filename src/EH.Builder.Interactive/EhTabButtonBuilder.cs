using DK.Getting.Generic;
using EH.Builder.Abstraction;
using EH.Builder.Interactive.Base;
using EH.Builder.Option;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhTabButtonBuilder : IEhTabButtonBuilder
{
    private readonly EhAnimatedColorBackgroundBuilder m_BackgroundBuilder = new();
    private readonly EhContainerBuilder               m_ContainerBuilder  = new();
    private readonly EhOptionsProvider                m_OptionsProvider   = new();
    private readonly EhInternalToggleBuilder          m_ToggleBuilder     = new();
    public IOgContainer<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> tabContainer, float x = 0, float y = 0) =>
        Build(name, texture, separatorSelectorGetter, source, out tabContainer, x, y, m_OptionsProvider);
    private IOgContainer<IOgVisualElement> Build(string name, Texture2D texture, OgAnimationRectGetter<OgTransformerRectGetter> separatorSelectorGetter,
        IOgContainer<IOgElement> source, out IOgContainer<IOgElement> tabContainer, float x, float y, EhOptionsProvider provider)
    {
        EhTabButtonOption option             = provider.TabButtonOption;
        float             tabContainerHeight = provider.WindowOption.WindowHeight - provider.WindowOption.ToolbarContainerHeight;
        OgAnimationArbitraryScriptableObserver<DkReadOnlyGetter<Color>, Color, bool> backgroundObserver = new((getter, state) =>
        {
            getter.SetTime();
            getter.TargetModifier = state ? option.BackgroundInteractColorProperty.Get() : option.BackgroundColorProperty.Get();
        });
        tabContainer = m_ContainerBuilder.Build($"{name}TabContainer", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.TabContainerWidth, tabContainerHeight));
        }));
        OgTransformerRectGetter rectGetter = null!;
        OgTextureElement image = m_BackgroundBuilder.Build($"{name}Background", provider.BaseAnimationColor, texture, option.TabButtonSize,
            option.TabButtonSize, 0, 0, option.TabButtonBorder, backgroundObserver, provider.AnimationSpeed, context =>
            {
                rectGetter = context.RectGetProvider;
            });
        EhTabObserver tabObserver = new(source, tabContainer, option.TabButtonSize, rectGetter, separatorSelectorGetter);
        IOgToggle<IOgVisualElement> button = m_ToggleBuilder.Build($"{name}Button", false, new([backgroundObserver]),
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.TabButtonSize, option.TabButtonSize));
                context.Observable.AddObserver(tabObserver);
                context.Observable.Notify(false);
            }));
        button.Add(image);
        return button;
    }
}