using DK.Binding.Generic;
using DK.Observing.Generic;
using DK.Property.Generic;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.DataTypes.Alignment;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Options;
using System;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhSliderBuilder // фулл говнокодище, еще будет переписано
{
    private readonly EhSliderOption                                                              m_Colors = new();
    private readonly EhContainerBuilder                                                          m_ContainerBuilder;
    private readonly EhInternalSliderBuilder                                                     m_SliderBuilder;
    private readonly EhTextBuilder                                                               m_TextBuilder;
    private readonly EhTextureBuilder                                                            m_TextureBuilder;
    private readonly OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> m_ThumbInteractObserver;
    private readonly OgAnimationArbitraryScriptableObserver<OgTransformerRectGetter, Rect, bool> m_ThumbOutlineInteractObserver;
    public EhSliderBuilder(IEhVisualOption context)
    {
        m_ThumbInteractObserver = new((getter, value) =>
        {
            getter.TargetModifier = SqueezeRect(getter, value, getter.TargetModifier, (SliderThumbSize / 8) + 0.5f,
                                                SliderThumbSize / 8);
        });
        m_ThumbOutlineInteractObserver = new((getter, value) =>
        {
            float offset = SliderThumbOutlineSize / 8;
            getter.TargetModifier = SqueezeRect(getter, value, getter.TargetModifier, offset, offset);
        });
        m_TextureBuilder = new(context);
        m_TextBuilder    = new(context);
        m_ContainerBuilder = new(new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(SliderWidth * 2,
                                                          SliderWidth));
        }));
        m_SliderBuilder = new();
    }
    public DkProperty<float> AnimationSpeed         { get; set; } = new(0.8f);
    public float             BackgroundBorder       { get; set; } = 0.5f;
    public int               FontSize               { get; set; } = 10;
    public float             PixelsPerUnit          { get; set; } = 200;
    public float             SliderHeight           { get; set; } = 4;
    public float             SliderThumbOutlineSize { get; set; } = 16;
    public float             SliderThumbSize        { get; set; } = 12;
    public float             SliderWidth            { get; set; } = 200;
    public float             ThumbBorder            { get; set; } = 0.5f;
    private OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> BuildThumbOutlineObserver(float max) =>
        new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value * (SliderWidth / max)) - (SliderThumbOutlineSize / 2);
            return rect;
        });
    private OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> BuildThumbObserver(float max) =>
        new((getter, value) =>
        {
            Rect rect = getter.TargetModifier;
            rect.x = (value * (SliderWidth / max)) - (SliderThumbSize / 2) - ((SliderThumbOutlineSize - SliderThumbSize) / 2);
            return rect;
        });
    private static Rect SqueezeRect(OgAnimationGetter<OgTransformerRectGetter, Rect> getter, bool value, Rect rect, float xOffset, float yOffset)
    {
        if(value)
        {
            getter.SetTime();
            rect.position += new Vector2(xOffset, yOffset);
            rect.size     -= new Vector2(xOffset * 2, yOffset * 2);
        }
        else
        {
            rect.position -= new Vector2(xOffset, yOffset);
            rect.size     += new Vector2(xOffset * 2, yOffset * 2);
        }
        return rect;
    }
    private void FillThumbContext(OgTextureBuildContext context, OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> valueObserver,
        OgAnimationGetterObserver<OgTransformerRectGetter, Rect, bool> interactObserver)
    {
        context.RectGetProvider.Speed = AnimationSpeed.Get();
        interactObserver.Getter       = context.RectGetProvider;
        valueObserver.Getter          = context.RectGetProvider;
    }
    public IOgElement Build(string name, float initial, float min, float max, string textFormat = "{0}", bool roundingToInt = false)
    {
        #region build value text
        DkProperty<string> textProperty = new(string.Format(textFormat, initial));
        OgTextElement text = m_TextBuilder.Build($"{name}TextValue", m_Colors.TextColorProperty, FontSize, PixelsPerUnit, textProperty,
                                                 new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                                 {
                                                     context.RectGetProvider.OriginalGetter.Options
                                                            .SetOption(new
                                                                           OgMinSizeTransformerOption(SliderWidth,
                                                                                                      SliderWidth))
                                                            .SetOption(new
                                                                           OgMarginTransformerOption(SliderWidth - 20,
                                                                                                     -SliderHeight * 2));
                                                 }), out DkBinding<string> textValueBinding,
                                                 out DkBinding<Color> colorBinding);
        ;
        m_Colors.m_TextColorBindings.Add(colorBinding);
        DkScriptableObserver<float> textObserver = new();
        textObserver.OnUpdate += value =>
        {
            textProperty.Set(string.Format(textFormat, roundingToInt ? Math.Round(value) : value));
            textValueBinding.Sync();
        };
        #endregion
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbObserver        = BuildThumbObserver(max);
        OgAnimationScriptableObserver<OgTransformerRectGetter, Rect, float> thumbOutlineObserver = BuildThumbOutlineObserver(max);
        #region build background
        OgTextureElement background = m_TextureBuilder.Build($"{name}Background", m_Colors.BackgroundColorProperty,
                                                             new(BackgroundBorder, BackgroundBorder, BackgroundBorder, BackgroundBorder),
                                                             new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                             {
                                                                 context.RectGetProvider.OriginalGetter.Options
                                                                        .SetOption(new OgSizeTransformerOption(SliderWidth, SliderHeight))
                                                                        .SetOption(new OgAlignmentTransformerOption(EOgAlignment.MIDDLE));
                                                             }), out DkBinding<Color> backgroundBinding);
        m_Colors.m_BackgroundColorBindings.Add(backgroundBinding);
        #endregion
        #region build outline
        OgTextureElement thumbOutline = m_TextureBuilder.Build($"{name}ThumbOutline", m_Colors.ThumbOutlineColorProperty,
                                                               new(ThumbBorder, ThumbBorder, ThumbBorder, ThumbBorder),
                                                               new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                               {
                                                                   FillThumbContext(context, thumbOutlineObserver, m_ThumbOutlineInteractObserver);
                                                                   context.RectGetProvider.OriginalGetter.Options
                                                                          .SetOption(new OgSizeTransformerOption(SliderThumbOutlineSize,
                                                                                         SliderThumbOutlineSize))
                                                                          .SetOption(new OgAlignmentTransformerOption(EOgAlignment.MIDDLE));
                                                               }), out DkBinding<Color> outlineBinding);
        m_Colors.m_ThumbOutlineColorBindings.Add(outlineBinding);
        #endregion
        #region build thumb
        OgTextureElement thumb = m_TextureBuilder.Build($"{name}Thumb", m_Colors.ThumbColorProperty,
                                                        new(ThumbBorder, ThumbBorder, ThumbBorder, ThumbBorder),
                                                        new OgScriptableBuilderProcess<OgTextureBuildContext>(context =>
                                                        {
                                                            FillThumbContext(context, thumbObserver, m_ThumbInteractObserver);
                                                            context.RectGetProvider.OriginalGetter.Options
                                                                   .SetOption(new OgSizeTransformerOption(SliderThumbSize, SliderThumbSize))
                                                                   .SetOption(new OgAlignmentTransformerOption(EOgAlignment.MIDDLE))
                                                                   .SetOption(new OgMarginTransformerOption((SliderThumbOutlineSize - SliderThumbSize) /
                                                                                  2));
                                                        }), out DkBinding<Color> thumbBinding);
        m_Colors.m_ThumbColorBindings.Add(thumbBinding);
        #endregion
        #region build slider
        IOgSlider<IOgVisualElement> slider = m_SliderBuilder.Build(name, new([
            thumbObserver,
            thumbOutlineObserver,
            textObserver
        ]), initial, min, max, new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgMinSizeTransformerOption(SliderWidth, SliderHeight * 2));
            context.Element.IsInteractingObserver?.AddObserver(m_ThumbInteractObserver);
            context.Element.IsInteractingObserver?.AddObserver(m_ThumbOutlineInteractObserver);
            context.Observable.Notify(initial);
        }));
        slider.Add(background);
        slider.Add(thumbOutline);
        slider.Add(thumb);
        #endregion
        #region build container
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container", null);
        container.Add(text);
        container.Add(slider);
        container.Add(m_TextBuilder.Build($"{name}Text", m_Colors.TextColorProperty, FontSize, PixelsPerUnit, name,
                                          new OgScriptableBuilderProcess<OgTextBuildContext>(context =>
                                          {
                                              context.RectGetProvider.OriginalGetter.Options
                                                     .SetOption(new OgMinSizeTransformerOption(SliderWidth,
                                                                                               SliderWidth))
                                                     .SetOption(new
                                                                    OgMarginTransformerOption(0,
                                                                                              -SliderHeight * 2));
                                          }), out DkBinding<Color> textNameBinding));
        m_Colors.m_TextColorBindings.Add(textNameBinding);
        #endregion
        return container;
    }
}