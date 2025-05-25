using DK.Getting.Generic;
using DK.Observing.Generic;
using DK.Property.Observing.Abstraction.Generic;
using DK.Property.Observing.Generic;
using EH.Builder.Interactive.Base;
using EH.Builder.Option;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts;
using OG.Builder.Contexts.Interactive;
using OG.DataKit.Animation;
using OG.DataKit.Animation.Extensions;
using OG.DataKit.Animation.Observer;
using OG.DataKit.Processing;
using OG.DataKit.Transformer;
using OG.DataTypes.Orientation;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Event;
using OG.Event.Extensions;
using OG.Transformer.Options;
using UnityEngine;

namespace EH.Builder.Interactive;

public class EhDropdownBuilder(IEhVisualOption context)
{
    private readonly EhBackgroundBuilder                m_BackgroundBuilder        = new();
    private readonly EhContainerBuilder                 m_ContainerBuilder         = new();
    private readonly EhTextBuilder                      m_TextBuilder              = new(context);
    private readonly EhOptionsProvider                  m_OptionsProvider          = new();
    private readonly EhInternalModalInteractableBuilder m_ModalInteractableBuilder = new();
    private readonly EhInternalButtonBuilder            m_ButtonBuilder            = new();
    public IOgModalInteractable<IOgElement> Build(string name, IDkObservableProperty<int> selected, string[] values) =>
        Build(name, selected, values, m_OptionsProvider);
    
    private IOgModalInteractable<IOgElement> Build(string name, IDkObservableProperty<int> selected, string[] values, EhOptionsProvider provider)
    {
        EhDropdownOption option = provider.DropdownOption;
        
    }
}
