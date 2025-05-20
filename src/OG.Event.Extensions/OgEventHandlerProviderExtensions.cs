using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Event.Extensions;
public static class OgEventHandlerProviderExtensions
{
    public static void Register<TEvent>(this IOgEventHandlerProvider provider, IOgEventCallback<TEvent> handler) where TEvent : IOgEvent =>
        provider.Register(new OgEventCallbackHandler<TEvent>(handler));
    public static void RegisterToEnd<TEvent>(this IOgEventHandlerProvider provider, IOgEventCallback<TEvent> handler) where TEvent : IOgEvent =>
        provider.RegisterToEnd(new OgEventCallbackHandler<TEvent>(handler));
}