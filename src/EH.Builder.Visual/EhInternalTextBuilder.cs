using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Setting.Generic;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts.Visual;
using OG.Builder.Visual;
using OG.Element.Visual;
using OG.Event.Abstraction;
using OG.Factory.Visual;
using UnityEngine;
namespace EH.Builder.Visual;
public class EhInternalTextBuilder
{
    private readonly IEhVisualOption                 m_Context;
    private readonly OgTextBuilder                   m_OgTextBuilder;
    private readonly DkProcessor<OgTextBuildContext> m_Processor;
    public EhInternalTextBuilder(IEhVisualOption context)
    {
        m_Processor     = new();
        m_OgTextBuilder = new(new OgTextFactory(), m_Processor);
        m_Context       = context;
    }
    public OgTextElement Build(string name, IDkGetProvider<Color> colorGetter, IOgEventHandlerProvider? provider, int fontSize, TextAnchor alignment,
        IDkGetProvider<string> textGetter, IDkProcess<OgTextBuildContext> process, out DkBinding<string> textBinding)
    {
        m_Processor.AddProcess(process);
        OgTextElement element = m_OgTextBuilder.Build(new(name, colorGetter, provider, m_Context.Font, fontSize, alignment, FontStyle.Normal,
            TextClipping.Clip, false, textGetter.Get()));
        textBinding = new(textGetter, new DkScriptableSetter<string>(text =>
        {
            element.Text = text;
            return true;
        }));
        m_Processor.RemoveProcess(process);
        return element;
    }
    public OgTextElement BuildStatic(string name, IDkGetProvider<Color> colorGetter, IOgEventHandlerProvider? provider, int fontSize, TextAnchor alignment,
        string text, IDkProcess<OgTextBuildContext> process)
    {
        m_Processor.AddProcess(process);
        OgTextElement element = m_OgTextBuilder.Build(new(name, colorGetter, provider, m_Context.Font, fontSize, alignment, FontStyle.Normal,
            TextClipping.Clip, false, text));
        m_Processor.RemoveProcess(process);
        return element;
    }
}