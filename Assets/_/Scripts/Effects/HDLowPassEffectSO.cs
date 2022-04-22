using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDLowPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.LowPass;

        [SerializeField][Range(10, 22000)] private float cutoffFrequency;
        [SerializeField][Range(1, 10)] private float lowpassResonanceQ;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioLowPassFilter)) as AudioLowPassFilter;

            filter.cutoffFrequency = cutoffFrequency;
            filter.lowpassResonanceQ = lowpassResonanceQ;

            return filter;
        }
    }
}