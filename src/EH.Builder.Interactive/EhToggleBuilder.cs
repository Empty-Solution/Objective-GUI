using DK.Getting.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhToggleBuilder(IEhConfigProvider provider, EhContainerBuilder containerBuilder, EhBaseTextBuilder textBuilder,
    EhInternalToggleBuilder toggleBuilder, EhInternalBindModalBuilder<bool> bindModalBuilder)
{
    public IEhToggle Build(IDkGetProvider<string> name, IEhProperty<bool> value, float y)
    {
        EhToggleConfig      toggleConfig     = provider.ToggleConfig;
        IOgOptionsContainer optionsContainer = null!;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
            optionsContainer = context.RectGetProvider.Options;
        }));
        OgTextElement nameText = textBuilder.Build(name.Get(), toggleConfig.TextColor, name, toggleConfig.NameTextFontSize, toggleConfig.NameTextAlignment,
            provider.InteractableElementConfig.Width - toggleConfig.Width, provider.InteractableElementConfig.Height);
        container.Add(nameText);
        IOgToggle<IOgVisualElement> toggle = toggleBuilder.Build(name.Get(), value, provider.InteractableElementConfig.Width - toggleConfig.Width);
        container.Add(toggle);
        container.Add(bindModalBuilder.Build(name.Get(), provider.InteractableElementConfig.Width - toggleConfig.Width,
            (provider.InteractableElementConfig.Height - toggleConfig.Height) / 2, toggleConfig.Width, toggleConfig.Height, value, property =>
            {
                return toggleBuilder.Build(name.Get(), property,
                    provider.InteractableElementConfig.BindModalWidth - toggleConfig.Width - (provider.InteractableElementConfig.HorizontalPadding * 2));
            }));
        container.Sort = false;
        return new EhToggle(container, optionsContainer);
    }
}