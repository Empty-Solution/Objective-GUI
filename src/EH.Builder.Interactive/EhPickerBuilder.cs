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
public class EhPickerBuilder(IEhVisualOption visual)
{
    private readonly EhBackgroundBuilder                m_BackgroundBuilder        = new();
    private readonly EhContainerBuilder                 m_ContainerBuilder         = new();
    private readonly EhInternalHorizontalSliderBuilder  m_HorizontalSliderBuilder  = new();
    private readonly EhInternalModalInteractableBuilder m_ModalInteractableBuilder = new();
    private readonly EhOptionsProvider                  m_OptionsProvider          = new();
    private readonly EhInternalQuadBuilder              m_QuadBuilder              = new(visual.Material);
    private readonly EhInternalVectorBuilder            m_VectorBuilder            = new();
    private readonly EhInternalVerticalSliderBuilder    m_VerticalSliderBuilder    = new();
    public IOgContainer<IOgElement> Build(string name, IDkProperty<Color> value, float x, float y) => Build(name, value, x, y, m_OptionsProvider);
    private IOgContainer<IOgElement> Build(string name, IDkProperty<Color> value, float x, float y, EhOptionsProvider provider)
    {
        EhPickerOption option = provider.PickerOption;
        IOgContainer<IOgElement> container = m_ContainerBuilder.Build($"{name}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height))
                       .SetOption(new OgMarginTransformerOption(x, y)).SetOption(new OgFlexiblePositionTransformerOption(EOgOrientation.VERTICAL));
            }));
        IOgModalInteractable<IOgElement> button = m_ModalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.Width, option.Height));
            }));
        OgTextureElement background = m_BackgroundBuilder.Build($"{name}Background", value, option.Width, option.Height, 0, 0,
            new(option.Border, option.Border, option.Border, option.Border));
        container.Add(background);
        container.Add(button);
        IOgContainer<IOgElement> sourceContainer = m_ContainerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(option.ModalWindowWidth, option.ModalWindowHeight))
                       .SetOption(new OgMarginTransformerOption(option.Width, option.Height));
            }));
        OgTextureElement modalBackground = m_BackgroundBuilder.Build($"{name}ModalBackground", option.BackgroundColorProperty, option.ModalWindowWidth,
            option.ModalWindowHeight, 0, 0, new(option.ModalBorder, option.ModalBorder, option.ModalBorder, option.ModalBorder));
        sourceContainer.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, 0, option.ModalWindowWidth, option.ModalWindowHeight))));
        modalBackground.ZOrder = 9999;
        sourceContainer.Add(modalBackground);
        HSVAColor                   hsvaColor         = (HSVAColor)value.Get();
        DkObservableProperty<float> hue               = new(new DkObservable<float>([]), hsvaColor.H);
        float                       huePickerWidth    = option.ModalWindowWidth - (option.PickerOffset * 2);
        float                       huePickerHeight   = option.ModalWindowHeight * 0.1f;
        float                       alphaPickerWidth  = option.ModalWindowWidth * 0.1f;
        float                       alphaPickerHeight = (option.ModalWindowHeight * 0.8f) - option.PickerOffset;
        float                       alphaPickerX      = option.ModalWindowWidth - alphaPickerWidth - option.PickerOffset;
        float                       alphaPickerY      = huePickerHeight + (option.PickerOffset * 2);
        sourceContainer.Add(BuildHuePicker(name, huePickerWidth, huePickerHeight, option, value, hue));
        sourceContainer.Add(BuildAlphaPicker(name, alphaPickerWidth, alphaPickerHeight, alphaPickerX, alphaPickerY, hsvaColor, option, value));
        sourceContainer.Add(BuildSvPicker(name, alphaPickerHeight, alphaPickerWidth, alphaPickerY, hsvaColor, option, value, hue));
        button.Add(sourceContainer);
        return container;
    }
    private IOgVectorValueElement<IOgVisualElement> BuildSvPicker(string name, float alphaPickerHeight, float alphaPickerWidth, float alphaPickerY,
        HSVAColor hsvaColor, EhPickerOption option, IDkProperty<Color> value, DkObservableProperty<float> hue)
    {
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
        float sVPickerWidth = option.ModalWindowWidth - alphaPickerWidth - (option.PickerOffset * 3);
        IOgVectorValueElement<IOgVisualElement> sVPicker = m_VectorBuilder.Build($"{name}SVPicker", sV, new(0, 1), new(1, 0),
            new OgScriptableBuilderProcess<OgVectorBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(sVPickerWidth, alphaPickerHeight))
                       .SetOption(new OgMarginTransformerOption(option.PickerOffset, alphaPickerY));
            }));
        DkScriptableGetter<Color> topRightColor = new(() =>
        {
            HSVAColor hsvaColor = new(hue.Get(), 1, 1, 1);
            return (Color)hsvaColor;
        });
        DkReadOnlyGetter<Color> black = new(Color.black);
        OgQuadElement sVBackground = m_QuadBuilder.Build($"{name}SVBackground", new DkReadOnlyGetter<Color>(new(1, 1, 1, 1f)), topRightColor, black, black,
            new(option.MainPickerBorder, option.MainPickerBorder, option.MainPickerBorder, option.MainPickerBorder),
            new OgScriptableBuilderProcess<OgQuadBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(sVPickerWidth, alphaPickerHeight));
            }));
        sVBackground.ZOrder = 9999;
        sVPicker.Add(sVBackground);
        return sVPicker;
    }
    private IOgSlider<IOgVisualElement> BuildAlphaPicker(string name, float alphaPickerWidth, float alphaPickerHeight, float alphaPickerX,
        float alphaPickerY, HSVAColor hsvaColor, EhPickerOption option, IDkProperty<Color> value)
    {
        DkScriptableObserver<float> alphaObserver = new();
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
            new(option.AlphaPickerBorder, option.AlphaPickerBorder, option.AlphaPickerBorder, option.AlphaPickerBorder),
            new OgScriptableBuilderProcess<OgQuadBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(alphaPickerWidth, alphaPickerHeight));
            }));
        alphaBackground.ZOrder = 9999;
        alphaPicker.Add(alphaBackground);
        return alphaPicker;
    }
    private IOgSlider<IOgVisualElement> BuildHuePicker(string name, float huePickerWidth, float huePickerHeight, EhPickerOption option,
        IDkProperty<Color> value, DkObservableProperty<float> hue)
    {
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
                       .SetOption(new OgMarginTransformerOption(option.PickerOffset, option.PickerOffset));
            }));
        OgTextureElement hueBackground = m_BackgroundBuilder.Build($"{name}HueBackground", new DkReadOnlyGetter<Color>(Color.white), huePickerWidth,
            huePickerHeight, 0, 0, new(option.HuePickerBorder, option.HuePickerBorder, option.HuePickerBorder, option.HuePickerBorder), null, null, new(),
            GenerateHueTexture(huePickerWidth, huePickerHeight));
        hueBackground.ZOrder = 9999;
        huePicker.Add(hueBackground);
        return huePicker;
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