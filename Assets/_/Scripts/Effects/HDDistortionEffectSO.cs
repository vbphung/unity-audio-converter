using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDDistortionEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Distortion;
        [field: SerializeField] public float DistortionLevel { get; private set; }
    }
}