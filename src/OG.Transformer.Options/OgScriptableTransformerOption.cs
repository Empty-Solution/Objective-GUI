using OG.Transformer.Abstraction;
using System;
using UnityEngine;
namespace OG.Transformer.Options;
public class OgScriptableTransformerOption(Func<Rect, Rect, Rect, int, Rect> func) : IOgTransformerOption
{
    public Func<Rect, Rect, Rect, int, Rect> Function => func;
}