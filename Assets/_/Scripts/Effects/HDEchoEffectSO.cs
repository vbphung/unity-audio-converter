using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDEchoEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Echo;
        [SerializeField] private float delay;
        [SerializeField] private float decayRatio;
        [SerializeField] private float dryMix;
        [SerializeField] private float wetMix;

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