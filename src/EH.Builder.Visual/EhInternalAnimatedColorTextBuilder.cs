using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Setting.Generic;
using EH.Builder.Option.Abstraction;
using OG.Builder.Contexts.Visual;
using OG.Builder.Visual;
using OG.Element.Visual;
using OG.Factory.Visual;
using UnityEngine;
namespace EH.Builder.Visual;
public class EhInternalAnimatedColorTextBuilder
{
    private readonly IEhVisualOption                              m_Context;
    private readonly OgAnimatedColorTextBuilder                   m_OgTextBuilder;
    private readonly DkProcessor<OgAnimatedColorTextBuildContext> m_Processor;
    public EhInternalAnimatedColorTextBuilder(IEhVisualOption context)
    {
        m_Processor     = new();
        m_OgTextBuilder = new(new OgTextFactory(), m_Processor);
        m_Context       = context;
    }
    public OgTextElement Build(string name, IDkGetProvider<Color> colorGetter, int fontSize, TextAnchor alignment, IDkGetProvider<string> textGetter,
        IDkProcess<OgAnimatedColorTextBuildContext> process, out DkBinding<string> textBinding, out DkBinding<Color> colorBinding)
    {
        m_Processor.AddProcess(process);
        OgTextElement element = m_OgTextBuilder.Build(new(name, colorGetter.Get(), m_Context.Font, fontSize, alignment, FontStyle.Normal,
            TextClipping.Clip, false, textGetter.Get()));
        textBinding = new(textGetter, new DkScriptableSetter<string>(text =>
        {
            element.Text = text;
            return true;
        }));
        colorBinding = new(colorGetter, new DkScriptableSetter<Color>(color =>
        {
            element.Color = color;
            return true;
        }));
        m_Processor.RemoveProcess(process);
        return element;
    }
    public OgTextElement Build(string name, IDkGetProvider<Color> colorGetter, int fontSize, TextAnchor alignment, string text,
        IDkProcess<OgAnimatedColorTextBuildContext> process, out DkBinding<Color> colorBinding)
    {
        m_Processor.AddProcess(process);
        OgTextElement element = m_OgTextBuilder.Build(new(name, colorGetter.Get(), m_Context.Font, fontSize, alignment, FontStyle.Normal,
            TextClipping.Clip, false, text));
        colorBinding = new(colorGetter, new DkScriptableSetter<Color>(color =>
        {
            element.Color = color;
            return true;
        }));
        m_Processor.RemoveProcess(process);
        return element;
    }
}