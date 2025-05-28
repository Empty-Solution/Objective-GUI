using DK.Getting.Abstraction.Generic;
using DK.Getting.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using EH.Builder.Options.Abstraction;
using OG.Builder.Contexts.Visual;
using OG.Builder.Visual;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Factory.Visual;
using UnityEngine;
namespace EH.Builder.Visual;
public class EhInternalTextBuilder
{
    private readonly IEhVisualProvider                 m_VisualProvider;
    private readonly OgTextBuilder                   m_OgTextBuilder;
    private readonly DkProcessor<OgTextBuildContext> m_Processor;
    public EhInternalTextBuilder(IEhVisualProvider visualProvider)
    {
        m_Processor     = new();
        m_OgTextBuilder = new(new OgTextFactory(), m_Processor);
        m_VisualProvider       = visualProvider;
    }
    public OgTextElement Build(string name, IDkGetProvider<Color> colorGetter, IOgEventHandlerProvider? provider, int fontSize, TextAnchor alignment,
        IDkGetProvider<string> textGetter, IDkProcess<OgTextBuildContext> process)
    {
        m_Processor.AddProcess(process);
        OgTextElement element = m_OgTextBuilder.Build(new(name, colorGetter, provider, m_VisualProvider.Font, fontSize, alignment, FontStyle.Normal,
            TextClipping.Clip, false, textGetter));
        m_Processor.RemoveProcess(process);
        return element;
    }
    public OgTextElement BuildStatic(string name, IDkGetProvider<Color> colorGetter, IOgEventHandlerProvider? provider, int fontSize, TextAnchor alignment,
        string text, IDkProcess<OgTextBuildContext> process)
    {
        m_Processor.AddProcess(process);
        OgTextElement element = m_OgTextBuilder.Build(new(name, colorGetter, provider, m_VisualProvider.Font, fontSize, alignment, FontStyle.Normal,
            TextClipping.Clip, false, new DkReadOnlyGetter<string>(text)));
        m_Processor.RemoveProcess(process);
        return element;
    }
}