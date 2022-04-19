using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HerbiDino.Audio
{
    public class HDAudioEffectDestination : VisualElement, IHDAudioEffectItem
    {
        public int Index { get; set; }

        public HDAudioEffectDestination(int index) : base()
        {
            Index = index;
            AddToClassList(HDEffectView.DragDestination);
            RegisterPointerEvents();
        }

        public void RegisterPointerEvents()
        {
            RegisterCallback<PointerEnterEvent>(ptr =>
            {
                if (Event.current.type == EventType.MouseDrag)
                    AddToClassList(HDEffectView.DragDestinationHover);
            });

            RegisterCallback<PointerLeaveEvent>(ptr =>
            {
                if (Event.current.type == EventType.MouseDrag)
                    RemoveFromClassList(HDEffectView.DragDestinationHover);
            });

            RegisterCallback<PointerUpEvent>(ptr =>
            {
                RemoveFromClassList(HDEffectView.DragDestinationHover);
            });
        }
    }
}