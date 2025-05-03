using DK.Scoping;
using DK.Scoping.Extensions;
using OG.DataTypes.Vector;
using OG.Graphics;
using OG.Graphics.Abstraction.Contexts;
using OG.Unity.Extensions;
using UnityEngine;
namespace OG.Unity.Graphics;
public class OgInlineHandler : OgBaseRepaintHandler<OgClipRepaintContext>
{
    private OgInlineScope? m_Scope;
    protected override bool Handle(OgClipRepaintContext reason)
    {
        m_Scope            ??= new();
        m_Scope.InlineRect =   new(reason.RepaintRect.X, reason.RepaintRect.Y);
        reason.Scope       =   m_Scope.OpenContext();
        return true;
    }
    private class OgInlineScope : DkScope
    {
        public OgVector2 InlineRect { get; set; }
        protected override void OnOpened()
        {
            GL.PushMatrix();
            GL.MultMatrix(Matrix4x4.Translate(InlineRect.ToUnity()));
        }
        protected override void OnClosed() => GL.PopMatrix();
    }
}