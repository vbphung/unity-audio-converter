using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDAudioPitchShifterEffectSO : HDAudioEffectSO
    {
        public override HDEffectType Type => HDEffectType.PitchShifter;

        [SerializeField][Range(-3, 3)] private float pitch;

        public override Component CreateFilter(GameObject audioSource)
        {
            audioSource.GetComponent<AudioSource>().pitch = pitch;
            return null;
        }
    }
}