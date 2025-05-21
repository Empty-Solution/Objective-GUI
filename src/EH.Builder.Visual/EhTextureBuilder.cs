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
public class EhTextureBuilder
{
    private readonly IEhVisualOption                    m_Context;
    private readonly OgTextureBuilder                   m_OgTextureBuilder;
    private readonly DkProcessor<OgTextureBuildContext> m_Processor;
    public EhTextureBuilder(IEhVisualOption context)
    {
        m_Processor        = new();
        m_OgTextureBuilder = new(new OgTextureFactory(), m_Processor);
        m_Context          = context;
    }
    public OgTextureElement Build(string name, IDkGetProvider<Color> colorGetter, Vector4 borders, IDkProcess<OgTextureBuildContext> process,
        out DkBinding<Color> binding)
    {
        m_Processor.AddProcess(process);
        OgTextureElement element = m_OgTextureBuilder.Build(new(name, colorGetter.Get(), m_Context.Material, borders));
        binding = new(colorGetter, new DkScriptableSetter<Color>(color =>
        {
            element.Color = color;
            return true;
        }));
        m_Processor.RemoveProcess(process);
        return element;
    }
}