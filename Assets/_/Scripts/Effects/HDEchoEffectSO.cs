using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDEchoEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.Echo;
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float DecayRatio { get; private set; }
        [field: SerializeField] public float DryMix { get; private set; }
        [field: SerializeField] public float WetMix { get; private set; }
    }
}