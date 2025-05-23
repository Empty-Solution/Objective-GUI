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
public class EhInternalTextureBuilder
{
    private readonly OgTextureBuilder                   m_OgTextureBuilder;
    private readonly DkProcessor<OgTextureBuildContext> m_Processor;
    public EhInternalTextureBuilder()
    {
        m_Processor        = new();
        m_OgTextureBuilder = new(new OgTextureFactory(), m_Processor);
    }
    public OgTextureElement Build(string name, IDkGetProvider<Color> colorGetter, Vector4 borderWidths, Vector4 borderRadiuses,
        IDkProcess<OgTextureBuildContext> process, out DkBinding<Color> binding)
    {
        m_Processor.AddProcess(process);
        OgTextureElement element = m_OgTextureBuilder.Build(new(name, colorGetter.Get(), Texture2D.whiteTexture, borderWidths, borderRadiuses, 1, false));
        binding = new(colorGetter, new DkScriptableSetter<Color>(color =>
        {
            element.Color = color;
            return true;
        }));
        m_Processor.RemoveProcess(process);
        return element;
    }
}