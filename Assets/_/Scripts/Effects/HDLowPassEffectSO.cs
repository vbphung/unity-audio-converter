using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDLowPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.LowPass;
        [field: SerializeField] public float LowpassResonaceQ { get; private set; }
        [field: SerializeField] public AnimationCurve CustomCutoffCurve { get; private set; }
        [field: SerializeField] public float CutoffFrequency { get; private set; }
        [field: SerializeField] public float LowpassResonanceQ { get; private set; }
    }
}