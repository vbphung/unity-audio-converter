using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDReverbEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Reverb;
        [SerializeField] private float hfReference;
        [SerializeField] private float density;
        [SerializeField] private float diffusion;
        [SerializeField] private float reverbDelay;
        [SerializeField] private float reverbLevel;
        [SerializeField] private float reflectionsDelay;
        [SerializeField] private float reflectionsLevel;
        [SerializeField] private float decayHFRatio;
        [SerializeField] private float decayTime;
        [SerializeField] private float roomHF;
        [SerializeField] private float room;
        [SerializeField] private float dryLevel;
        [SerializeField] private AudioReverbPreset reverbPreset;
        [SerializeField] private float roomLF;
        [SerializeField] private float lfReference;

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