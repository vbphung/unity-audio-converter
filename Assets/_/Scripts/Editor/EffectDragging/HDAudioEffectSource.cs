using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HerbiDino.Audio
{
    public class HDAudioEffectSource : Box, IHDAudioEffectItem
    {
        public int Index { get; set; }

        public HDAudioEffectSource(int index) : base()
        {
            Index = index;
            AddToClassList(HDEffectView.View);
            RegisterPointerEvents();
        }

        public void RegisterPointerEvents()
        {
            RegisterCallback<PointerDownEvent>(ptr =>
            {

            });
        }
    }
}