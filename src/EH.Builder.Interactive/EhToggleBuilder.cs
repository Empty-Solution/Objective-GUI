using EH.Builder.Interactive.ElementBuilders;
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
public class EhToggleBuilder(IEhVisualOption context)
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder = new();
    private readonly EhContainerBuilder      m_ContainerBuilder  = new();
    private readonly EhFillBuilder           m_FillBuilder       = new();
    private readonly EhToggleOption          m_Options           = new();
    private readonly EhTextBuilder           m_TextBuilder       = new(context);
    private readonly EhThumbBuilder          m_ThumbBuilder      = new();
    private readonly EhInternalToggleBuilder m_ToggleBuilder     = new();
    public IOgElement Build(string name, bool initial) => Build(name, initial, m_Options);
    private IOgElement Build(string name, bool initial, EhToggleOption options)
    {
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgFlexibleSizeTransformerOption(EOgOrientation.ALL));
            }));
        float offset = (options.ToggleHeight - options.ThumbSize) / 2;
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
            new(value ? options.ToggleWidth - options.ThumbSize - offset : offset, 0, 0, 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, options.ThumbSize / 6, options.ThumbSize / 6);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> fillObserver = new((_, value) =>
            new(0, 0, options.ThumbSize + offset + (value ? options.ToggleWidth - options.ThumbSize - offset : offset), 0));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> fillInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
        });
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", options.ThumbColorProperty, thumbObserver, thumbInteractObserver, options.ThumbSize,
            0, offset, options.ThumbBorder, options.AnimationSpeed, options.m_ThumbColorBindings);
        OgTextureElement fill = m_FillBuilder.Build(name, options.BackgroundFillColorProperty, 0, options.ToggleHeight, 0, 0, options.BackgroundBorder,
            options.AnimationSpeed, options.m_BackgroundFillColorBindings, context =>
            {
                fillObserver.Getter         = context.RectGetProvider;
                fillInteractObserver.Getter = context.RectGetProvider;
            });
        IOgToggle<IOgVisualElement> toggle = m_ToggleBuilder.Build(name, new([fillObserver, thumbObserver]), initial,
            new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgMinSizeTransformerOption(options.ToggleWidth, options.ToggleHeight))
                       .SetOption(new OgFlexiblePositionTransformerOption());
                context.Element.IsInteractingObserver?.AddObserver(fillInteractObserver);
                context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                context.Observable.Notify(initial);
            }));
        OgTextureElement background = m_BackgroundBuilder.Build(name, options.BackgroundColorProperty, options.ToggleWidth, options.ToggleHeight, 0, 0,
            options.BackgroundBorder, options.m_BackgroundColorBindings);
        toggle.Add(background);
        toggle.Add(fill);
        toggle.Add(thumb);
        OgTextElement text = m_TextBuilder.BuildStaticText(name, options.TextColorProperty, name, options.FontSize, options.NameAlignment,
            options.SubTabOption.TabWidth - options.ToggleWidth, options.ToggleHeight, 0, 0, options.m_TextColorBindings);
        container.Add(text);
        container.Add(toggle);
        return container;
    }
}