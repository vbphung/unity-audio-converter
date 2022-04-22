using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDHighPassEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.HighPass;

        [SerializeField][Range(10, 22000)] private float cutoffFrequency;
        [SerializeField][Range(1, 10)] private float highpassResonanceQ;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioHighPassFilter)) as AudioHighPassFilter;

            filter.cutoffFrequency = cutoffFrequency;
            filter.highpassResonanceQ = highpassResonanceQ;

            return filter;
        }
    }
}