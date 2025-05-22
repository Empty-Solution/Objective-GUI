using DK.Binding.Generic;
using DK.Observing.Generic;
using DK.Property.Generic;
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
public class EhSliderBuilder
{
    private readonly EhContainerBuilder      m_ContainerBuilder;
    private readonly EhSliderOption          m_Options;
    private readonly EhInternalSliderBuilder m_SliderBuilder;
    private readonly EhTextBuilder           m_TextBuilder;
    private readonly EhTextureBuilder        m_TextureBuilder;
    public EhSliderBuilder(IEhVisualOption context)
    {
        m_Options          = new();
        m_TextureBuilder   = new();
        m_TextBuilder      = new(context);
        m_ContainerBuilder = new(null);
        m_SliderBuilder    = new();
    }
    private void FillThumbContext(OgTextureBuildContext context, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> valueObserver,
        OgAnimationGetterObserver<OgTransformerRectGetter, Rect, bool> interactObserver)
    {
        context.RectGetProvider.Speed = m_Options.AnimationSpeed;
        interactObserver.Getter       = context.RectGetProvider;
        valueObserver.Getter          = context.RectGetProvider;
    }
    public IOgElement Build(string name, float initial, float min, float max, string textFormat, bool roundToInt = true) =>
        Build(name, initial, min, max, textFormat, roundToInt,
              m_Options);
    private IOgElement Build(string name, float initial, float min, float max, string textFormat, bool roundToInt,
        EhSliderOption options)
    {
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
                                                                      new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
                                                                      {
                                                                          context.RectGetProvider.Options
                                                                                 .SetOption(new
                                                                                                OgSizeTransformerOption(options.SubTabOption.TabWidth,
                                                                                                    options.SubTabOption.TabHeight));
                                                                      }));
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbInteractObserver = new((getter, value) =>
        {
            float offset = m_Options.SliderThumbSize / 6;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> thumbOutlineInteractObserver = new((getter, value) =>
        {
            float offset = m_Options.SliderThumbOutlineSize / 8;
            getter.TargetModifier = getter.AdjustRect(value, getter.TargetModifier, offset, offset);
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * options.SliderWidth) - (options.SliderThumbSize / 2);
            return rect;
        });
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbOutlineObserver = new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value / max * options.SliderWidth) - (options.SliderThumbOutlineSize / 2);
            return rect;
        });
        #region build value text
        DkProperty<string> textProperty = new(string.Format(textFormat, initial));
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", options.TextColorProperty, options.ValueFontSize, options.ValueAlignment,
                                                 textProperty,
                                                 new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                                 {
                                                     context.RectGetProvider.OriginalGetter.Options
                                                            .SetOption(new
                                                                           OgSizeTransformerOption(options.SliderWidth,
                                                                                                   options.SliderHeight *
                                                                                                   2))
                                                            .SetOption(new
                                                                           OgMarginTransformerOption(0, -14));
                                                 }), out DkBinding<string> textValueBinding,
                                                 out DkBinding<Color> colorBinding);
        options.m_TextColorBindings.Add(colorBinding);
        DkScriptableObserver<float> textObserver = new();
        textObserver.OnUpdate += value =>
        {
            textProperty.Set(string.Format(textFormat, roundToInt ? Mathf.RoundToInt(value) : value));
            textValueBinding.Sync();
        };
        #endregion
        #region build name text
        container.Add(m_TextBuilder.Build($"{name}Text", options.TextColorProperty, options.NameFontSize, options.NameAlignment, name,
                                          new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                          {
                                              context.RectGetProvider.OriginalGetter.Options
                                                     .SetOption(new
                                                                    OgSizeTransformerOption(options.SubTabOption.TabWidth - options.SliderWidth,
                                                                                            options.SubTabOption
                                                                                                   .TabHeight));
                                          }), out DkBinding<Color> textNameBinding));
        options.m_TextColorBindings.Add(textNameBinding);
        #endregion
        #region build outline
        OgTextureElement thumbOutline = m_TextureBuilder.Build($"{name}ThumbOutline", options.ThumbOutlineColorProperty, new(),
                                                               new(options.ThumbBorder, options.ThumbBorder, options.ThumbBorder, options.ThumbBorder),
                                                               new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                               {
                                                                   FillThumbContext(context, thumbOutlineObserver, thumbOutlineInteractObserver);
                                                                   context.RectGetProvider.OriginalGetter.Options
                                                                          .SetOption(new OgSizeTransformerOption(options.SliderThumbOutlineSize,
                                                                                         options.SliderThumbOutlineSize))
                                                                          .SetOption(new OgMarginTransformerOption(0,
                                                                                         ((options.SliderHeight * 2) -
                                                                                          options.SliderThumbOutlineSize) / 2));
                                                               }), out DkBinding<Color> outlineBinding);
        options.m_ThumbOutlineColorBindings.Add(outlineBinding);
        #endregion
        #region build background
        OgTextureElement background = m_TextureBuilder.Build($"{name}Background", options.BackgroundColorProperty, new(),
                                                             new(options.BackgroundBorder, options.BackgroundBorder, options.BackgroundBorder,
                                                                 options.BackgroundBorder),
                                                             new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                             {
                                                                 context.RectGetProvider.OriginalGetter.Options
                                                                        .SetOption(new OgSizeTransformerOption(options.SliderWidth,
                                                                                       options.SliderHeight))
                                                                        .SetOption(new OgMarginTransformerOption(0,
                                                                                       ((options.SliderHeight * 2) - options.SliderHeight) /
                                                                                       2));
                                                             }), out DkBinding<Color> backgroundBinding);
        options.m_BackgroundColorBindings.Add(backgroundBinding);
        #endregion
        #region build thumb
        OgTextureElement thumb = m_TextureBuilder.Build($"{name}Thumb", options.ThumbColorProperty, new(),
                                                        new(options.ThumbBorder, options.ThumbBorder, options.ThumbBorder, options.ThumbBorder),
                                                        new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                        {
                                                            FillThumbContext(context, thumbObserver, thumbInteractObserver);
                                                            context.RectGetProvider.OriginalGetter.Options
                                                                   .SetOption(new OgSizeTransformerOption(options.SliderThumbSize,
                                                                                  options.SliderThumbSize))
                                                                   .SetOption(new OgMarginTransformerOption(0,
                                                                                  ((options.SliderHeight * 2) - options.SliderThumbSize) / 2));
                                                        }), out DkBinding<Color> thumbBinding);
        options.m_ThumbColorBindings.Add(thumbBinding);
        #endregion
        #region build fill
        OgTextureElement backgroundFill = m_TextureBuilder.Build($"{name}BackgroundFill", options.BackgroundFillColorProperty, new(),
                                                                 new(options.BackgroundBorder, options.BackgroundBorder, options.BackgroundBorder,
                                                                     options.BackgroundBorder),
                                                                 new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                                 {
                                                                     context.RectGetProvider.Speed = options.AnimationSpeed;
                                                                     context.RectGetProvider.OriginalGetter.Options
                                                                            .SetOption(new OgSizeTransformerOption(0, options.SliderHeight))
                                                                            .SetOption(new OgMarginTransformerOption(0,
                                                                                           ((options.SliderHeight * 2) - options.SliderHeight) / 2))
                                                                     .SetOption(new OgScriptableTransformerOption((rect, parentRect, lastRect,
                                                                         remaining) =>
                                                                                         {
                                                                                             Rect thumbRect = thumb.ElementRect.Get();
                                                                                             rect.width = thumbRect.x;
                                                                                             return rect;
                                                                                         }));
                                                                 }), out DkBinding<Color> backgroundFillBinding);
        options.m_BackgroundFillColorBindings.Add(backgroundFillBinding);
        #endregion
        #region build slider
        IOgSlider<IOgVisualElement> slider = m_SliderBuilder.Build(name, new([thumbObserver, thumbOutlineObserver, textObserver]), initial, min, max,
                                                                   new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
                                                                   {
                                                                       context.RectGetProvider.Options
                                                                              .SetOption(new OgSizeTransformerOption(options.SliderWidth,
                                                                                             options.SliderHeight * 2))
                                                                              .SetOption(new
                                                                                             OgMarginTransformerOption(options.SubTabOption.TabWidth - options.SliderWidth,
                                                                                                 (options.SubTabOption.TabHeight -
                                                                                                  (options.SliderHeight * 2)) / 2));
                                                                       context.Element.IsInteractingObserver?.AddObserver(thumbInteractObserver);
                                                                       context.Element.IsInteractingObserver?.AddObserver(thumbOutlineInteractObserver);
                                                                       context.Observable.Notify(initial);
                                                                   }));
        slider.Add(background);
        slider.Add(backgroundFill);
        slider.Add(thumbOutline);
        slider.Add(thumb);
        slider.Add(text);
        #endregion
        container.Add(slider);
        return container;
    }
}