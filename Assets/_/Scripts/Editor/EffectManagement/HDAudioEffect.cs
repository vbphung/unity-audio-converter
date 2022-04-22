using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HerbiDino.Audio
{
    public class HDAudioEffect : Box, IHDAudioEffectItem
    {
        private Action onPointerDown;
        private Action onClick;

        public HDAudioEffect(Action onPointerDown, Action onClick) : base()
        {
            AddToClassList(HDEffectView.View);

            this.onPointerDown = onPointerDown;
            this.onClick = onClick;
            RegisterPointerEvents();
        }

        public void RegisterPointerEvents()
        {
            RegisterCallback<PointerDownEvent>(ptr => onPointerDown());
            RegisterCallback<ClickEvent>(evt => onClick());
        }

        public void Select()
        {
            AddToClassList(HDEffectView.SelectedView);
        }

        public void Deselect()
        {
            RemoveFromClassList(HDEffectView.SelectedView);
        }
    }
}