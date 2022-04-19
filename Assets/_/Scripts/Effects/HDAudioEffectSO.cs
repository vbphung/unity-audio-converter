using UnityEngine;

namespace HerbiDino.Audio
{
    public abstract class HDAudioEffectSO : ScriptableObject
    {
        public abstract HDEffectType Type { get; }
        public abstract Component CreateFilter(GameObject audioSource);
    }
}