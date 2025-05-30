using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.DataTypes.BindType;
using OG.Event.Abstraction;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgBindableFactoryArguments<TValue>(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkFieldProvider<TValue> value, IDkValueOverride<TValue> valueOverride, IDkProperty<KeyCode?> bind,
    IDkGetProvider<EOgBindType> bindTypeGetProvider) : OgElementFactoryArguments(name, rectGetProvider, eventProvider)
{
    public IDkFieldProvider<TValue> Value         { get; } = value;
    public IDkValueOverride<TValue> ValueOverride { get; } = valueOverride;
    public IDkProperty<KeyCode?> Bind                { get; } = bind;
    public IDkGetProvider<EOgBindType>     BindTypeGetProvider { get; } = bindTypeGetProvider;
}