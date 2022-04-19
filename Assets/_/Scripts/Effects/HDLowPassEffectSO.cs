using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDLowPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.LowPass;
        [SerializeField] private AnimationCurve customCutoffCurve;
        [SerializeField] private float cutoffFrequency;
        [SerializeField] private float lowpassResonanceQ;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioLowPassFilter)) as AudioLowPassFilter;

            filter.customCutoffCurve = customCutoffCurve;
            filter.cutoffFrequency = cutoffFrequency;
            filter.lowpassResonanceQ = lowpassResonanceQ;

            return filter;
        }
    }
}