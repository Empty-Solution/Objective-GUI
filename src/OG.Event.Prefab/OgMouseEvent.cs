using OG.Event.Prefab.Abstraction;
using UnityEngine;
namespace OG.Event.Prefab;
public abstract class OgMouseEvent(UnityEngine.Event source) : OgInputEvent(source), IOgMouseEvent;