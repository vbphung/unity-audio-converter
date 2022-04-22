using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDReverbEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Reverb;

        [SerializeField] private AudioReverbPreset reverbPreset;
        [SerializeField][Range(-10000, 0)] private float dryLevel;
        [SerializeField][Range(-10000, 0)] private float room;
        [SerializeField][Range(-10000, 0)] private float roomHF;
        [SerializeField][Range(-10000, 0)] private float roomLF;
        [SerializeField][Range(0.1f, 20)] private float decayTime;
        [SerializeField][Range(0.1f, 2)] private float decayHFRatio;
        [SerializeField][Range(-10000, 1000)] private float reflectionsLevel;
        [SerializeField][Range(0, 0.3f)] private float reflectionsDelay;
        [SerializeField][Range(-10000, 2000)] private float reverbLevel;
        [SerializeField][Range(0, 0.1f)] private float reverbDelay;
        [SerializeField][Range(1000, 20000)] private float hfReference;
        [SerializeField][Range(20, 1000)] private float lfReference;
        [SerializeField][Range(0, 100)] private float diffusion;
        [SerializeField][Range(0, 100)] private float density;

        public override Component CreateFilter(GameObject audioSource)
        {
            var filter = audioSource.AddComponent(typeof(AudioReverbFilter)) as AudioReverbFilter;

            filter.hfReference = hfReference;
            filter.density = density;
            filter.diffusion = diffusion;
            filter.reverbDelay = reverbDelay;
            filter.reverbLevel = reverbLevel;
            filter.reflectionsDelay = reflectionsDelay;
            filter.reflectionsLevel = reflectionsLevel;
            filter.decayHFRatio = decayHFRatio;
            filter.decayTime = decayTime;
            filter.roomHF = roomHF;
            filter.room = room;
            filter.dryLevel = dryLevel;
            filter.reverbPreset = reverbPreset;
            filter.roomLF = roomLF;
            filter.lfReference = lfReference;

            return filter;
        }
    }
}