using DK.Scoping;
using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Graphics;
using OG.Graphics.Abstraction.Contexts;
using OG.Unity.Extensions;
using UnityEngine;
namespace OG.Unity.Graphics;
public class OgClipHandler : OgBaseRepaintHandler<OgClipRepaintContext>
{
    private OgClipScope? m_Scope;
    protected override bool Handle(OgClipRepaintContext reason)
    {
        m_Scope          ??= new();
        m_Scope.ClipRect =   reason.RepaintRect;
        reason.Scope     =   m_Scope.OpenContext();
        return true;
    }
    private class OgClipScope : DkScope
    {
        public OgRectangle ClipRect { get; set; }
        protected override void OnOpened() => GUI.BeginClip(ClipRect.ToUnity());
        protected override void OnClosed() => GUI.EndClip();
    }
}