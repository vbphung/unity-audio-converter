using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDHighPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.HighPass;
        [field: SerializeField] public float HighpassResonaceQ { get; private set; }
        [field: SerializeField] public float CutoffFrequency { get; private set; }
        [field: SerializeField] public float HighpassResonanceQ { get; private set; }
    }
}