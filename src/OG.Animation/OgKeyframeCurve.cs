using OG.Animation.Abstraction;

namespace OG.Animation;

public class OgKeyframeCurve(float min, float max) : OgCurve(min, max)
{
    public override IOgCurveVertex GetNearestVertex(float time)
    {
        if(time <= this[0].Time)
            return this[0];

        if(time >= this[Count - 1].Time)
            return this[Count - 1];

        for(int i = 0; i < Count - 1; i++)
            if(time >= this[i].Time && time < this[i + 1].Time)
                return this[i + 1];

        return this[Count - 1];
    }
}