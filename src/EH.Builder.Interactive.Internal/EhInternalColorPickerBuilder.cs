using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Providing.Abstraction;
using EH.Builder.Visual;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.Builder.Contexts.Visual;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
using UnityEngine;
namespace EH.Builder.Interactive.Internal;
public class EhInternalColorPickerBuilder(IEhConfigProvider provider, EhBaseBackgroundBuilder backgroundBuilder, EhContainerBuilder containerBuilder,
    EhBaseModalInteractableBuilder modalInteractableBuilder, EhQuadBuilder quadBuilder, EhBaseVectorBuilder vectorBuilder,
    EhBaseHorizontalSliderBuilder horizontalSliderBuilder, EhBaseVerticalSliderBuilder verticalSliderBuilder)
{
    public IEhColorPicker Build(string name, IDkProperty<Color> value, float x, float y)
    {
        EhPickerConfig      pickerConfig     = provider.PickerConfig;
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> sourceContainer = containerBuilder.Build($"{name}SourceContainer",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(pickerConfig.Width, pickerConfig.Height))
                       .SetOption(new OgMarginTransformerOption(x, y));
                optionsContainer = context.RectGetProvider.Options;
            }));
        IOgModalInteractable<IOgElement> modalInteractable = modalInteractableBuilder.Build($"{name}", false,
            new OgScriptableBuilderProcess<OgModalButtonBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(pickerConfig.Width, pickerConfig.Height));
            }));
        OgTextureElement background = backgroundBuilder.Build($"{name}Background", value, pickerConfig.Width, pickerConfig.Height, 0, 0,
            new(pickerConfig.Border, pickerConfig.Border, pickerConfig.Border, pickerConfig.Border));
        sourceContainer.Add(background);
        sourceContainer.Add(modalInteractable);
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(pickerConfig.ModalWindowWidth, pickerConfig.ModalWindowHeight))
                   .SetOption(new OgMarginTransformerOption(-pickerConfig.ModalWindowWidth, pickerConfig.Height));
        }));
        OgTextureElement modalBackground = backgroundBuilder.Build($"{name}ModalBackground", pickerConfig.BackgroundColor, pickerConfig.ModalWindowWidth,
            pickerConfig.ModalWindowHeight, 0, 0,
            new(pickerConfig.ModalWindowBorder, pickerConfig.ModalWindowBorder, pickerConfig.ModalWindowBorder, pickerConfig.ModalWindowBorder));
        container.Add(new OgInteractableElement<IOgElement>($"{name}ModalInteractable", new OgEventHandlerProvider(),
            new DkReadOnlyGetter<Rect>(new(0, 0, pickerConfig.ModalWindowWidth, pickerConfig.ModalWindowHeight))));
        modalBackground.ZOrder = 9999;
        container.Add(modalBackground);
        HSVAColor                   hsvaColor         = (HSVAColor)value.Get();
        DkObservableProperty<float> hue               = new(new DkObservable<float>([]), hsvaColor.H);
        float                       huePickerWidth    = pickerConfig.ModalWindowWidth - (pickerConfig.PickerOffset * 2);
        float                       huePickerHeight   = pickerConfig.ModalWindowHeight * 0.1f;
        float                       alphaPickerWidth  = pickerConfig.ModalWindowWidth * 0.1f;
        float                       alphaPickerHeight = (pickerConfig.ModalWindowHeight * 0.8f) - pickerConfig.PickerOffset;
        float                       alphaPickerX      = pickerConfig.ModalWindowWidth - alphaPickerWidth - pickerConfig.PickerOffset;
        float                       alphaPickerY      = huePickerHeight + (pickerConfig.PickerOffset * 2);
        container.Add(BuildAlphaPicker($"{name}AlphaPicker", hsvaColor, value, alphaPickerWidth, alphaPickerHeight, alphaPickerX, alphaPickerY,
            pickerConfig));
        container.Add(BuildHuePicker($"{name}HuePicker", hue, value, huePickerWidth, huePickerHeight, pickerConfig));
        container.Add(BuildSvPicker(name, hsvaColor, hue, value, alphaPickerWidth, alphaPickerHeight, alphaPickerY, pickerConfig));
        modalInteractable.Add(container);
        return new EhColorPicker(sourceContainer, optionsContainer);
    }
    private IOgVectorValueElement<IOgVisualElement> BuildSvPicker(string name, HSVAColor hsvaColor, DkObservableProperty<float> hue,
        IDkProperty<Color> value, float alphaWidth, float height, float y, EhPickerConfig pickerConfig)
    {
        DkObservableProperty<Vector2> sV         = new(new DkObservable<Vector2>([]), new(hsvaColor.S, hsvaColor.V));
        DkScriptableObserver<Vector2> sVObserver = new();
        sVObserver.OnUpdate += state =>
        {
            HSVAColor hsvaColor = (HSVAColor)value.Get();
            hsvaColor.S = state.x;
            hsvaColor.V = state.y;
            hsvaColor.H = hue.Get();
            value.Set((Color)hsvaColor);
        };
        sV.AddObserver(sVObserver);
        float sVPickerWidth = pickerConfig.ModalWindowWidth - alphaWidth - (pickerConfig.PickerOffset * 3);
        IOgVectorValueElement<IOgVisualElement> sVPicker = vectorBuilder.Build($"{name}SVPicker", sV, new(0, 1), new(1, 0),
            new OgScriptableBuilderProcess<OgVectorBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(sVPickerWidth, height))
                       .SetOption(new OgMarginTransformerOption(pickerConfig.PickerOffset, y));
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
        OgQuadElement sVBackground = quadBuilder.Build($"{name}SVBackground", topLeftColor, topRightColor, bottomLeftColor, bottomRightColor,
            new(pickerConfig.MainPickerBorder, pickerConfig.MainPickerBorder, pickerConfig.MainPickerBorder, pickerConfig.MainPickerBorder),
            new OgScriptableBuilderProcess<OgQuadBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(sVPickerWidth, height));
            }));
        sVBackground.ZOrder = 9999;
        sVPicker.Add(sVBackground);
        return sVPicker;
    }
    private IOgSlider<IOgVisualElement> BuildAlphaPicker(string name, HSVAColor hsvaColor, IDkProperty<Color> value, float width, float height, float x,
        float y, EhPickerConfig pickerConfig)
    {
        DkScriptableObserver<float> alphaObserver = new();
        alphaObserver.OnUpdate += state =>
        {
            Color got = value.Get();
            value.Set(new(got.r, got.g, got.b, state));
        };
        DkObservableProperty<float> alpha = new(new DkObservable<float>([]), hsvaColor.A);
        alpha.AddObserver(alphaObserver);
        IOgSlider<IOgVisualElement> alphaPicker = verticalSliderBuilder.Build($"{name}AlphaPicker", alpha, 1, 0,
            new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height)).SetOption(new OgMarginTransformerOption(x, y));
            }));
        DkScriptableGetter<Color> topColor = new(() =>
        {
            HSVAColor hsvaColor = (HSVAColor)value.Get();
            hsvaColor.A = 1;
            return (Color)hsvaColor;
        });
        DkReadOnlyGetter<Color> nullColor = new(new(0, 0, 0, 0));
        OgQuadElement alphaBackground = quadBuilder.Build($"{name}AlphaBackground", topColor, topColor, nullColor, nullColor,
            new(pickerConfig.AlphaPickerBorder, pickerConfig.AlphaPickerBorder, pickerConfig.AlphaPickerBorder, pickerConfig.AlphaPickerBorder),
            new OgScriptableBuilderProcess<OgQuadBuildContext>(context =>
            {
                context.RectGetProvider.OriginalGetter.Options.SetOption(new OgSizeTransformerOption(width, height));
            }));
        alphaBackground.ZOrder = 9999;
        alphaPicker.Add(alphaBackground);
        return alphaPicker;
    }
    private IOgSlider<IOgVisualElement> BuildHuePicker(string name, DkObservableProperty<float> hue, IDkProperty<Color> value, float width, float height,
        EhPickerConfig pickerConfig)
    {
        DkScriptableObserver<float> hueObserver = new();
        hueObserver.OnUpdate += state =>
        {
            HSVAColor hsvaColor = (HSVAColor)value.Get();
            hsvaColor.H = state;
            value.Set((Color)hsvaColor);
        };
        hue.AddObserver(hueObserver);
        IOgSlider<IOgVisualElement> huePicker = horizontalSliderBuilder.Build($"{name}HuePicker", hue, 1, 0,
            new OgScriptableBuilderProcess<OgSliderBuildContext>(context =>
            {
                context.RectGetProvider.Options.SetOption(new OgSizeTransformerOption(width, height))
                       .SetOption(new OgMarginTransformerOption(pickerConfig.PickerOffset, pickerConfig.PickerOffset));
            }));
        OgTextureElement hueBackground = backgroundBuilder.Build($"{name}HueBackground", new DkReadOnlyGetter<Color>(Color.white), width, height, 0, 0,
            new(pickerConfig.HuePickerBorder, pickerConfig.HuePickerBorder, pickerConfig.HuePickerBorder, pickerConfig.HuePickerBorder), null, null, new(),
            GenerateHueTexture(width, height));
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