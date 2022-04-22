using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDChorusEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Chorus;

        [SerializeField][Range(0, 1)] private float dryMix;
        [SerializeField][Range(0, 1)] private float wetMix1;
        [SerializeField][Range(0, 1)] private float wetMix2;
        [SerializeField][Range(0, 1)] private float wetMix3;
        [SerializeField][Range(0.1f, 100)] private float delay;
        [SerializeField][Range(0, 20)] private float rate;
        [SerializeField][Range(0, 1)] private float depth;

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