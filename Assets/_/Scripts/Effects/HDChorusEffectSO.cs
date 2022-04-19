using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDChorusEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Chorus;
        [SerializeField] private float dryMix;
        [SerializeField] private float wetMix1;
        [SerializeField] private float wetMix2;
        [SerializeField] private float wetMix3;
        [SerializeField] private float delay;
        [SerializeField] private float rate;
        [SerializeField] private float depth;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioChorusFilter)) as AudioChorusFilter;

            filter.dryMix = dryMix;
            filter.wetMix1 = wetMix1;
            filter.wetMix2 = wetMix2;
            filter.wetMix3 = wetMix3;
            filter.delay = delay;
            filter.rate = rate;
            filter.depth = depth;

            return filter;
        }
    }
}