using UnityEngine.Events;
using UnityEditor;

namespace HerbiDino.Audio
{
    public class HDAudioEffectManager
    {
        public UnityEvent onSwapEffects;

        public HDAudioMixerSO CurrentMixer { get; set; }
        public int SourceIndex { get; set; }
        public int DestinationIndex { get; set; }
        public int CurrentEffectIndex { get; set; } = 0;

        private bool CanSwap => DestinationIndex < SourceIndex || DestinationIndex > SourceIndex + 1;

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