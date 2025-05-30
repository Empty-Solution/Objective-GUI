using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Generic;
using DK.Observing.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Visual;
using OG.Transformer.Options;
using System.Collections.Generic;
namespace EH.Builder.Interactive;
public class EhSliderBuilder(EhConfigProvider provider,
    EhContainerBuilder containerBuilder, EhBaseTextBuilder textBuilder, EhInternalSliderBuilder sliderBuilder, EhInternalBindModalBuilder<float> bindModalBuilder)
{
    public IOgContainer<IOgElement> Build(string name, IEhProperty<float> value, float min, float max, string textFormat, int round, float y)
    {
        EhSliderConfig sliderConfig = provider.SliderConfig;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
        }));
        OgTextElement nameText = textBuilder.BuildStaticText(name, sliderConfig.TextColor, name, sliderConfig.NameFontSize, sliderConfig.NameAlignment,
            provider.InteractableElementConfig.Width - sliderConfig.Width, provider.InteractableElementConfig.Height);
        container.Add(nameText);
        container.Add(sliderBuilder.Build(name, value, min, max, textFormat, round));
        DkObservableProperty<float> overrideValue = new(new DkObservable<float>([]), 0f);
        container.Add(bindModalBuilder.Build(name, provider.InteractableElementConfig.Width - sliderConfig.Width, 
            (provider.InteractableElementConfig.Height - (sliderConfig.Height * 2)) / 2, provider.InteractableElementConfig.Width,
            provider.InteractableElementConfig.Height, value.ValueOverride, overrideValue, () =>
            {
                return sliderBuilder.Build(name, overrideValue, min, max, textFormat, round);
            }));
        return container;
    }
}