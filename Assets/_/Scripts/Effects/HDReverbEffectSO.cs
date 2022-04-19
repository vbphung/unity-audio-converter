using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDReverbEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Reverb;
        [field: SerializeField] public float HfReference { get; private set; }
        [field: SerializeField] public float Density { get; private set; }
        [field: SerializeField] public float Diffusion { get; private set; }
        [field: SerializeField] public float ReverbDelay { get; private set; }
        [field: SerializeField] public float ReverbLevel { get; private set; }
        [field: SerializeField] public float ReflectionsDelay { get; private set; }
        [field: SerializeField] public float ReflectionsLevel { get; private set; }
        [field: SerializeField] public float DecayHFRatio { get; private set; }
        [field: SerializeField] public float DecayTime { get; private set; }
        [field: SerializeField] public float RoomHF { get; private set; }
        [field: SerializeField] public float Room { get; private set; }
        [field: SerializeField] public float DryLevel { get; private set; }
        [field: SerializeField] public AudioReverbPreset reverbPreset { get; private set; }
        [field: SerializeField] public float RoomLF { get; private set; }
        [field: SerializeField] public float LfReference { get; private set; }
    }
}