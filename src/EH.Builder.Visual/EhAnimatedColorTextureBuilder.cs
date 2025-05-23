using DK.Binding.Generic;
using DK.Getting.Abstraction.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using DK.Setting.Generic;
using OG.Builder.Contexts.Visual;
using OG.Builder.Visual;
using OG.Element.Visual;
using OG.Factory.Visual;
using UnityEngine;
namespace EH.Builder.Visual;
public class EhAnimatedColorTextureBuilder
{
    private readonly OgAnimatedColorTextureBuilder                   m_OgTextureBuilder;
    private readonly DkProcessor<OgAnimatedColorTextureBuildContext> m_Processor;
    public EhAnimatedColorTextureBuilder()
    {
        m_Processor        = new();
        m_OgTextureBuilder = new(new OgTextureFactory(), m_Processor);
    }
    public OgTextureElement Build(string name, IDkGetProvider<Color> colorGetter, Texture2D texture, Vector4 borderWidths, Vector4 borderRadiuses,
        IDkProcess<OgAnimatedColorTextureBuildContext> process, out DkBinding<Color> binding)
    {
        m_Processor.AddProcess(process);
        OgTextureElement element = m_OgTextureBuilder.Build(new(name, colorGetter.Get(), texture, borderWidths, borderRadiuses, 1, false));
        binding = new(colorGetter, new DkScriptableSetter<Color>(color =>
        {
            element.Color = color;
            return true;
        }));
        m_Processor.RemoveProcess(process);
        return element;
    }
}