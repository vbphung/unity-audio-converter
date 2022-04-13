using System.Collections.Generic;
using UnityEngine;

namespace HerbiDino.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class HDAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private HDAudioMixerSO mixer;

        private List<Behaviour> filterList;
    }
}