using System.Collections.Generic;
using UnityEngine;

namespace HerbiDino.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class HDAudioSource : MonoBehaviour
    {
        private AudioSource audioSource;
        private HDAudioMixerSO mixer;
        private List<Component> filterList = new List<Component>();

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void SetMixer(HDAudioMixerSO mixer)
        {
            ClearFilterList();

            var hasPitchShifter = false;
            foreach (var sfx in mixer.Effects)
            {
                var filter = sfx.CreateFilter(gameObject);
                if (filter != null)
                    filterList.Add(filter);
                else hasPitchShifter = true;
            }

            if (!hasPitchShifter)
                audioSource.pitch = 1;
        }

        private void ClearFilterList()
        {
            foreach (var filter in filterList)
                DestroyImmediate(filter);

            filterList = new List<Component>();
        }
    }
}