using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Visual;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhToggleBuilder(EhConfigProvider provider, EhContainerBuilder containerBuilder, EhBaseTextBuilder textBuilder,
    EhInternalToggleBuilder toggleBuilder, EhInternalBindModalBuilder<bool> bindModalBuilder)
{
    public IOgContainer<IOgElement> Build(string name, IEhProperty<bool> value, float y)
    {
        EhToggleConfig toggleConfig = provider.ToggleConfig;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
        }));
        OgTextElement nameText = textBuilder.BuildStaticText(name, toggleConfig.TextColor, name, toggleConfig.NameFontSize, toggleConfig.NameAlignment,
            provider.InteractableElementConfig.Width - toggleConfig.Width, provider.InteractableElementConfig.Height);
        container.Add(nameText);
        container.Add(toggleBuilder.Build(name, value, provider.InteractableElementConfig.Width - toggleConfig.Width));
        container.Add(bindModalBuilder.Build(name, provider.InteractableElementConfig.Width - toggleConfig.Width,
            (provider.InteractableElementConfig.Height - (toggleConfig.Height * 2)) / 2, provider.InteractableElementConfig.Width,
            provider.InteractableElementConfig.Height, value.ValueOverride, property =>
            {
                return toggleBuilder.Build(name, property,
                    provider.InteractableElementConfig.BindModalWidth - toggleConfig.Width - (provider.InteractableElementConfig.HorizontalPadding * 2));
            }));
        return container;
    }
}