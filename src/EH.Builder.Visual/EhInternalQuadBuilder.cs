using DK.Getting.Abstraction.Generic;
using DK.Processing.Abstraction.Generic;
using DK.Processing.Generic;
using OG.Builder.Contexts.Visual;
using OG.Builder.Visual;
using OG.Element.Visual;
using OG.Factory.Visual;
using UnityEngine;
namespace EH.Builder.Visual;
public class EhInternalQuadBuilder
{
    private readonly Material                        m_Material;
    private readonly OgQuadBuilder                   m_OgTextureBuilder;
    private readonly DkProcessor<OgQuadBuildContext> m_Processor;
    public EhInternalQuadBuilder(Material material)
    {
        m_Processor        = new();
        m_OgTextureBuilder = new(new OgQuadFactory(), m_Processor);
        m_Material         = material;
    }
    public OgQuadElement Build(string name, IDkGetProvider<Color> topLeft, IDkGetProvider<Color> topRight, IDkGetProvider<Color> bottomLeft,
        IDkGetProvider<Color> bottomRight, Vector4 borderRadiuses, IDkProcess<OgQuadBuildContext> process)
    {
        m_Processor.AddProcess(process);
        OgQuadElement element = m_OgTextureBuilder.Build(new(name, topLeft, topRight, bottomLeft, bottomRight));
        element.Radius   = borderRadiuses;
        element.Material = m_Material;
        m_Processor.RemoveProcess(process);
        return element;
    }
}