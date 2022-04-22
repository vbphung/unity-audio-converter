using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDEchoEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Echo;

        [SerializeField][Range(10, 5000)] private float delay;
        [SerializeField][Range(0, 1)] private float decayRatio;
        [SerializeField][Range(0, 1)] private float dryMix;
        [SerializeField][Range(0, 1)] private float wetMix;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioEchoFilter)) as AudioEchoFilter;

            filter.delay = delay;
            filter.decayRatio = decayRatio;
            filter.dryMix = dryMix;
            filter.wetMix = wetMix;

            return filter;
        }
    }
}