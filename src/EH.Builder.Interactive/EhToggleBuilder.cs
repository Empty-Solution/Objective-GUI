using DK.Binding.Generic;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation.Extensions;
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
    private readonly EhContainerBuilder                                                          m_ContainerBuilder;
    private readonly EhToggleOption                                                              m_Options;
    private readonly EhTextBuilder                                                               m_TextBuilder;
    private readonly EhTextureBuilder                                                            m_TextureBuilder;
    private readonly OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> m_ThumbInteractObserver;
    private readonly EhInternalToggleBuilder                                                     m_ToggleBuilder;
    public EhToggleBuilder(IEhVisualOption context)
    {
        m_Options        = new();
        m_TextureBuilder = new();
        m_TextBuilder    = new(context);
        m_ThumbInteractObserver = new((getter, value) =>
        {
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, m_Options.ThumbSize / 4,
                                                      m_Options.ThumbSize / 4);
        });
        m_ContainerBuilder = new(null);
        m_ToggleBuilder    = new();
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
        #region build thumb
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = value ? options.ToggleWidth - options.ThumbSize - ((options.ToggleHeight - options.ThumbSize) / 2)
                         : (options.ToggleHeight - options.ThumbSize) / 2;
            return rect;
        });
        OgTextureElement thumb = m_TextureBuilder.Build($"{name}Thumb", options.ThumbColorProperty, new(),
                                                        new(options.ThumbBorder, options.ThumbBorder, options.ThumbBorder, options.ThumbBorder),
                                                        new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                        {
                                                            context.RectGetProvider.Speed  = options.AnimationSpeed;
                                                            thumbObserver.Getter           = context.RectGetProvider;
                                                            m_ThumbInteractObserver.Getter = context.RectGetProvider;
                                                            context.RectGetProvider.OriginalGetter.Options
                                                                   .SetOption(new OgSizeTransformerOption(options.ThumbSize, options.ThumbSize))
                                                                   .SetOption(new OgMarginTransformerOption(0,
                                                                                  (options.ToggleHeight - options.ThumbSize) / 2));
                                                        }), out DkBinding<Color> thumbBinding);
        options.m_ThumbColorBindings.Add(thumbBinding);
        #endregion
        #region build toggle
        IOgToggle<IOgVisualElement> toggle = m_ToggleBuilder.Build(name, new([thumbObserver]), initial,
                                                                   new OgScriptableBuilderProcess<OgToggleBuildContext>(context =>
                                                                   {
                                                                       context.RectGetProvider.Options
                                                                              .SetOption(new OgMinSizeTransformerOption(options.ToggleWidth,
                                                                                             options.ToggleHeight))
                                                                              .SetOption(new OgFlexiblePositionTransformerOption());
                                                                       context.Element.IsInteractingObserver?.AddObserver(m_ThumbInteractObserver);
                                                                       context.Observable.Notify(initial);
                                                                   }));
        toggle.Add(m_TextureBuilder.Build($"{name}Background", options.BackgroundColorProperty, new(),
                                          new(options.BackgroundBorder, options.BackgroundBorder, options.BackgroundBorder, options.BackgroundBorder),
                                          new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                          {
                                              context.RectGetProvider.OriginalGetter.Options
                                                     .SetOption(new
                                                                    OgSizeTransformerOption(options.ToggleWidth,
                                                                                            options.ToggleHeight));
                                          }), out DkBinding<Color> backgroundBinding));
        toggle.Add(thumb);
        options.m_BackgroundColorBindings.Add(backgroundBinding);
        #endregion
        #region build container
        container.Add(m_TextBuilder.Build($"{name}Text", options.TextColorProperty, options.FontSize, options.NameAlignment, name,
                                          new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                          {
                                              context.RectGetProvider.OriginalGetter.Options
                                                     .SetOption(new
                                                                    OgMinSizeTransformerOption(options.SubTabOption.TabWidth - options.ToggleWidth,
                                                                                               options.ToggleHeight));
                                          }), out DkBinding<Color> textNameBinding));
        container.Add(toggle);
        options.m_TextColorBindings.Add(textNameBinding);
        #endregion
        return container;
    }
}