using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Abstraction;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using EH.Builder.Options.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive;
public class EhPickerBuilder(EhConfigProvider provider, IEhVisualProvider visualProvider)
{
    private readonly EhBackgroundBuilder                m_BackgroundBuilder        = new();
    private readonly EhContainerBuilder                 m_ContainerBuilder         = new();
    private readonly EhInternalHorizontalSliderBuilder  m_HorizontalSliderBuilder  = new();
    private readonly EhInternalModalInteractableBuilder m_ModalInteractableBuilder = new();
    private readonly EhInternalQuadBuilder              m_QuadBuilder              = new(visualProvider.Material);
    private readonly EhInternalVectorBuilder            m_VectorBuilder            = new();
    private readonly EhInternalVerticalSliderBuilder    m_VerticalSliderBuilder    = new();
    public IOgContainer<IOgElement> Build(string name, IDkProperty<Color> value, float x, float y)
    {
        EhPickerConfig pickerConfig = provider.PickerConfig;
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(pickerConfig.Width, pickerConfig.Height))
                       .SetOption(new OgMarginTransformerOption(x, y)).SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL));
            }));
        IOgModalInteractable<IOgElement> button = m_ModalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(pickerConfig.Width, pickerConfig.Height));
            }));
        OgTextureElement background = m_BackgroundBuilder.Build($"{name}Background", value, pickerConfig.Width, pickerConfig.Height, 0, 0,
            new(pickerConfig.Border, pickerConfig.Border, pickerConfig.Border, pickerConfig.Border));
        container.Add(background);
        container.Add(button);
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(pickerConfig.ModalWindowWidth, pickerConfig.ModalWindowHeight))
                       .SetOption(new OgMarginTransformerOption(pickerConfig.Width, pickerConfig.Height));
            }));
        OgTextureElement modalBackground = m_BackgroundBuilder.Build($"{name}ModalBackground", pickerConfig.BackgroundColorProperty, pickerConfig.ModalWindowWidth,
            pickerConfig.ModalWindowHeight, 0, 0, new(pickerConfig.ModalBorder, pickerConfig.ModalBorder, pickerConfig.ModalBorder, pickerConfig.ModalBorder));
        sourceContainer.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, 0, pickerConfig.ModalWindowWidth, pickerConfig.ModalWindowHeight))));
        modalBackground.ZOrder = 9999;
        sourceContainer.Add(modalBackground);
        HSVAColor                   hsvaColor         = (HSVAColor)value.Get();
        DkObservableProperty<float> hue               = new(new DkObservable<float>([]), hsvaColor.H);
        float                       huePickerWidth    = pickerConfig.ModalWindowWidth - (pickerConfig.PickerOffset * 2);
        float                       huePickerHeight   = pickerConfig.ModalWindowHeight * 0.1f;
        float                       alphaPickerWidth  = pickerConfig.ModalWindowWidth * 0.1f;
        float                       alphaPickerHeight = (pickerConfig.ModalWindowHeight * 0.8f) - pickerConfig.PickerOffset;
        float                       alphaPickerX      = pickerConfig.ModalWindowWidth - alphaPickerWidth - pickerConfig.PickerOffset;
        float                       alphaPickerY      = huePickerHeight + (pickerConfig.PickerOffset * 2);
        DkScriptableObserver<float> alphaObserver     = new();
        alphaObserver.OnUpdate += state =>
        {
            Color got = value.Get();
            value.Set(new(got.r, got.g, got.b, state));
        };
        DkObservableProperty<float> alpha = new(new DkObservable<float>([]), hsvaColor.A);
        alpha.AddObserver(alphaObserver);
        IOgSlider<IOgVisualElement> alphaPicker = m_VerticalSliderBuilder.Build($"{name}AlphaPicker", alpha, 1, 0,
            new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(alphaPickerWidth, alphaPickerHeight))
                       .SetOption(new OgMarginTransformerOption(alphaPickerX, alphaPickerY));
            }));
        DkScriptableGetter<Color> topColor = new(() =>
        {
            HSVAColor hsvaColor = (HSVAColor)value.Get();
            hsvaColor.A = 1;
            return (Color)hsvaColor;
        });
        DkReadOnlyGetter<Color> nullColor = new(new(0, 0, 0, 0));
        OgQuadElement alphaBackground = m_QuadBuilder.Build($"{name}AlphaBackground", topColor, topColor, nullColor, nullColor,
            new(pickerConfig.AlphaPickerBorder, pickerConfig.AlphaPickerBorder, pickerConfig.AlphaPickerBorder, pickerConfig.AlphaPickerBorder),
            new OgScriptableBuilderProcess<OgQuadBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(alphaPickerWidth, alphaPickerHeight));
            }));
        alphaBackground.ZOrder = 9999;
        alphaPicker.Add(alphaBackground);
        sourceContainer.Add(alphaPicker);
        DkScriptableObserver<float> hueObserver = new();
        hueObserver.OnUpdate += state =>
        {
            HSVAColor hsvaColor = (HSVAColor)value.Get();
            hsvaColor.H = state;
            value.Set((Color)hsvaColor);
        };
        hue.AddObserver(hueObserver);
        IOgSlider<IOgVisualElement> huePicker = m_HorizontalSliderBuilder.Build($"{name}HuePicker", hue, 1, 0,
            new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(huePickerWidth, huePickerHeight))
                       .SetOption(new OgMarginTransformerOption(pickerConfig.PickerOffset, pickerConfig.PickerOffset));
            }));
        OgTextureElement hueBackground = m_BackgroundBuilder.Build($"{name}HueBackground", new DkReadOnlyGetter<Color>(Color.white), huePickerWidth,
            huePickerHeight, 0, 0, new(pickerConfig.HuePickerBorder, pickerConfig.HuePickerBorder, pickerConfig.HuePickerBorder, pickerConfig.HuePickerBorder), null, null, new(),
            GenerateHueTexture(huePickerWidth, huePickerHeight));
        hueBackground.ZOrder = 9999;
        huePicker.Value.Set(huePicker.Value.Get());
        huePicker.Add(hueBackground);
        sourceContainer.Add(huePicker);
        DkObservableProperty<Vector2> sV         = new(new DkObservable<Vector2>([]), new(hsvaColor.S, hsvaColor.V));
        DkScriptableObserver<Vector2> sVObserver = new();
        sVObserver.OnUpdate += state =>
        {
            HSVAColor hsvaColor = (HSVAColor)value.Get();
            hsvaColor.S = state.x;
            hsvaColor.V = state.y;
            value.Set((Color)hsvaColor);
        };
        sV.AddObserver(sVObserver);
        float sVPickerWidth = pickerConfig.ModalWindowWidth - alphaPickerWidth - (pickerConfig.PickerOffset * 3);
        IOgVectorValueElement<IOgVisualElement> sVPicker = m_VectorBuilder.Build($"{name}SVPicker", sV, new(0, 1), new(1, 0),
            new OgScriptableBuilderProcess<OgVectorBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(sVPickerWidth, alphaPickerHeight))
                       .SetOption(new OgMarginTransformerOption(pickerConfig.PickerOffset, alphaPickerY));
            }));
        DkScriptableGetter<Color> topLeftColor = new(() =>
        {
            HSVAColor hsvaColor = new(hue.Get(), 0, 1, 1);
            return (Color)hsvaColor;
        });
        DkScriptableGetter<Color> topRightColor = new(() =>
        {
            HSVAColor hsvaColor = new(hue.Get(), 1, 1, 1);
            return (Color)hsvaColor;
        });
        DkScriptableGetter<Color> bottomLeftColor = new(() =>
        {
            HSVAColor hsvaColor = new(hue.Get(), 0, 0, 1);
            return (Color)hsvaColor;
        });
        DkScriptableGetter<Color> bottomRightColor = new(() =>
        {
            HSVAColor hsvaColor = new(hue.Get(), 1, 0, 1);
            return (Color)hsvaColor;
        });

        OgQuadElement sVBackground = m_QuadBuilder.Build($"{name}SVBackground", topLeftColor, topRightColor, bottomLeftColor, bottomRightColor,
            new(pickerConfig.MainPickerBorder, pickerConfig.MainPickerBorder, pickerConfig.MainPickerBorder, pickerConfig.MainPickerBorder),
            new OgScriptableBuilderProcess<OgQuadBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(sVPickerWidth, alphaPickerHeight));
            }));
        sVBackground.ZOrder = 9999;
        sVPicker.Add(sVBackground);
        sourceContainer.Add(sVPicker);
        button.Add(sourceContainer);
        return container;
    }
    private Texture2D GenerateHueTexture(float width, float height) //thx ruler gui
    {
        Texture2D texture = new((int)width, (int)height)
        {
            filterMode = FilterMode.Point
        };
        for(int x = 0; x < width; x++)
        {
            float     h     = 1 - (x / width);
            HSVAColor color = new(h, 1, 1, 1);
            for(int y = 0; y < height; y++) texture.SetPixel(x, y, (Color)color);
        }
        texture.Apply();
        return texture;
    }
}