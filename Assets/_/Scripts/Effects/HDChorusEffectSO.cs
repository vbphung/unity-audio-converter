using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDChorusEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Chorus;
        [field: SerializeField] public float DryMix { get; private set; }
        [field: SerializeField] public float WetMix1 { get; private set; }
        [field: SerializeField] public float WetMix2 { get; private set; }
        [field: SerializeField] public float WetMix3 { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float Rate { get; private set; }
        [field: SerializeField] public float Depth { get; private set; }
    }
}