using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDHighPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.HighPass;
        [SerializeField] private float cutoffFrequency;
        [SerializeField] private float highpassResonanceQ;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioHighPassFilter)) as AudioHighPassFilter;

            filter.cutoffFrequency = cutoffFrequency;
            filter.highpassResonanceQ = highpassResonanceQ;

            return filter;
        }
    }
}