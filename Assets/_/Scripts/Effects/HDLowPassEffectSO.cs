using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDLowPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.LowPass;
    }
}