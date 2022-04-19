using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDAudioEffectDraggingManager
    {
        public HDAudioMixerSO CurrentMixer { get; set; }
        public int SourceIndex { get; set; }
        public int DestinationIndex { get; set; }

        public HDAudioEffectDraggingManager()
        {
            ResetIndices();
        }

        public void SwapEffects()
        {
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