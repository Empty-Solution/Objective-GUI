using EH.Builder.Interactive.ElementBuilders;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
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
public class EhToggleBuilder
{
    private readonly EhBackgroundBuilder     m_BackgroundBuilder;
    private readonly EhContainerBuilder      m_ContainerBuilder;
    private readonly EhFillBuilder           m_FillBuilder;
    private readonly EhToggleOption          m_Options;
    private readonly EhElementTextBuilder    m_TextBuilder;
    private readonly EhThumbBuilder          m_ThumbBuilder;
    private readonly EhInternalToggleBuilder m_ToggleBuilder;
    public EhToggleBuilder(IEhVisualOption context)
    {
        m_Options           = new();
        m_BackgroundBuilder = new();
        m_ThumbBuilder      = new();
        m_FillBuilder       = new();
        m_TextBuilder       = new(context);
        m_ContainerBuilder  = new();
        m_ToggleBuilder     = new();
    }
    public IOgElement Build(string name, bool initial) => Build(name, initial, m_Options);
    private IOgElement Build(string name, bool initial, EhToggleOption options)
    {
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
                                                                      new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
                                                                      {
                                                                          context.RectGetProvider.Options
                                                                                 .SetOption(new
                                                                                                OgSizeTransformerOption(options.SubTabOption.TabWidth,
                                                                                                    options.SubTabOption.TabHeight));
                                                                      }));
        float thumbY = (options.ToggleHeight - options.ThumbSize) / 2;
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
        {
            Rect  rect   = getter.TargetModifier;
            float offset = (options.ToggleHeight - options.ThumbSize) / 2;
            rect.x = value ? options.ToggleWidth - options.ThumbSize - offset : offset;
            return rect;
        });
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            getter.SetTime();
            //getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, options.ThumbSize / 8, options.ThumbSize / 8);
        });
        OgTextureElement thumb = m_ThumbBuilder.Build($"{name}Thumb", options.ThumbColorProperty, thumbObserver, thumbInteractObserver, options.ThumbSize,
                                                      options.ThumbSize, 0, thumbY, options.ThumbBorder, options.AnimationSpeed,
                                                      options.m_ThumbColorBindings);
        IOgToggle<IOgVisualElement> toggle = m_ToggleBuilder.Build(name, new([thumbObserver]), initial,
                                                                   new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
                                                                   {
                                                                       context.RectGetProvider.Options
                                                                              .SetOption(new OgMinSizeTransformerOption(options.ToggleWidth,
                                                                                             options.ToggleHeight))
                                                                              .SetOption(new OgFlexiblePositionTransformerOption());
                                                                       context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                                                                       context.Observable.Notify(initial);
                                                                   }));
        OgTextureElement background = m_BackgroundBuilder.Build(name, options.BackgroundColorProperty, options.ToggleWidth, options.ToggleHeight, 0, 0,
                                                                options.BackgroundBorder, options.m_BackgroundColorBindings);
        OgTextureElement fill = m_FillBuilder.Build(name, options.BackgroundFillColorProperty, options.ToggleWidth, options.ToggleHeight, 0, 0,
                                                    options.BackgroundBorder, options.AnimationSpeed, options.m_BackgroundFillColorBindings,
                                                    (rect, parent, last, remaining) =>
                                                    {
                                                        Rect  thumbRect = thumb.ElementRect.Get();
                                                        float offset    = (options.ToggleHeight - options.ThumbSize) / 2;
                                                        rect.width = thumbRect.x + thumbRect.width + offset;
                                                        return rect;
                                                    });
        toggle.Add(background);
        toggle.Add(fill);
        toggle.Add(thumb);
        OgTextElement text = m_TextBuilder.BuildStaticText(name, options.TextColorProperty, name, options.FontSize, options.NameAlignment,
                                                           options.SubTabOption.TabWidth - options.ToggleWidth, options.ToggleHeight, 0, 0,
                                                           options.m_TextColorBindings);
        container.Add(text);
        container.Add(toggle);
        return container;
    }
}