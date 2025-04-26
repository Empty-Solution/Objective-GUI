using OG.Animation.Abstraction;

namespace OG.Animation;

public class OgKeyframeCurve(float min, float max) : OgCurve(min, max)
{
    public override IOgCurveVertex GetNearestVertex(float time)
    {
        IOgCurveVertex first = this[0];
        if(time <= first.Time)
            return first;
        IOgCurveVertex last = this[Count - 1];
        if(time >= last.Time)
            return last;

        for(int i = 1; i < Count - 1; i++)
        {
            IOgCurveVertex next = this[i + 1];
            if(time >= this[i].Time && time < next.Time)
                return next;
        }

        return last;
    }
}