using DK.Processing.Abstraction.Generic;
using OG.Builder.Contexts.Interactive;
using OG.Factory.Interactive;
namespace OG.Builder.Interactive;
public class OgVerticalSliderBuilder(OgVerticalSliderFactory factory, IDkProcessor<OgSliderBuildContext>? processor)
    : OgSliderBuilder<OgVerticalSliderFactory>(factory, processor);