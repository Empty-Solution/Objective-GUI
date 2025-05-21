using DK.Observing.Generic;
namespace OG.Builder.Arguments.Interactive;
public class OgToggleBuildArguments(string name, bool value, DkObservable<bool> observable) : OgValueElementBuildArguments<bool>(name, value, observable);