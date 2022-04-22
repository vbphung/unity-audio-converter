using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HerbiDino.Audio
{
    public class HDAudioEffectDestination : VisualElement, IHDAudioEffectItem
    {
        private Action onPointerEnter;
        private Action onPointerLeave;
        private Action onPointerUp;

        public HDAudioEffectDestination(Action onPointerEnter, Action onPointerLeave, Action onPointerUp) : base()
        {
            AddToClassList(HDEffectView.DragDestination);

            this.onPointerEnter = onPointerEnter;
            this.onPointerLeave = onPointerLeave;
            this.onPointerUp = onPointerUp;

            RegisterPointerEvents();
        }

        public void RegisterPointerEvents()
        {
            RegisterCallback<PointerEnterEvent>(ptr =>
            {
                if (Event.current.type == EventType.MouseDrag)
                {
                    AddToClassList(HDEffectView.DragDestinationHover);
                    onPointerEnter();
                }
            });

            RegisterCallback<PointerLeaveEvent>(ptr =>
            {
                if (Event.current.type == EventType.MouseDrag)
                {
                    RemoveFromClassList(HDEffectView.DragDestinationHover);
                    onPointerLeave();
                }
            });

            RegisterCallback<PointerUpEvent>(ptr =>
            {
                RemoveFromClassList(HDEffectView.DragDestinationHover);
                onPointerUp();
            });
        }
    }
}