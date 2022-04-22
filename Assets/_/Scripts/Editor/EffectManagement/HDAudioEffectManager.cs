using UnityEngine.Events;
using UnityEditor;
using System.Collections.Generic;

namespace HerbiDino.Audio
{
    public class HDAudioEffectManager
    {
        public UnityEvent onSwapEffects;

        public HDAudioMixerSO CurrentMixer { get; set; }
        public int SourceIndex { get; set; }
        public int DestinationIndex { get; set; }
        public List<HDAudioEffect> EffectList { get; set; } = new List<HDAudioEffect>();
        public int CurrentEffectIndex
        {
            get => currentEffectIndex; set
            {
                currentEffectIndex = value;

                EffectList.ForEach(sfx => sfx.Deselect());
                EffectList[currentEffectIndex].Select();
            }
        }

        private bool CanSwap => DestinationIndex < SourceIndex || DestinationIndex > SourceIndex + 1;
        private int currentEffectIndex = 0;

        public HDAudioEffectManager()
        {
            onSwapEffects = new UnityEvent();
            ResetIndices();
        }

        public void SwapTwoEffects()
        {
            if (CanSwap)
            {
                int src = SourceIndex;
                int des = DestinationIndex - (DestinationIndex > SourceIndex ? 1 : 0);

                var srcSfx = CurrentMixer.Effects[src];
                CurrentMixer.Effects[src] = CurrentMixer.Effects[des];
                CurrentMixer.Effects[des] = srcSfx;
                EditorUtility.SetDirty(CurrentMixer);

                onSwapEffects?.Invoke();
            }

            ResetIndices();
        }

        public void ResetDestination()
        {
            DestinationIndex = -1;
        }

        private void ResetIndices()
        {
            SourceIndex = DestinationIndex = -1;
        }
    }
}