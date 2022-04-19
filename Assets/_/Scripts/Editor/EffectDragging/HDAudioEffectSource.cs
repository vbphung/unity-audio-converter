using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HerbiDino.Audio
{
    public class HDAudioEffectSource : Box, IHDAudioEffectItem
    {
        private Action onPointerDown;

        public HDAudioEffectSource(Action onPointerDow) : base()
        {
            AddToClassList(HDEffectView.View);

            this.onPointerDown = onPointerDow;
            RegisterPointerEvents();
        }

        public void RegisterPointerEvents()
        {
            RegisterCallback<PointerDownEvent>(ptr =>
            {
                onPointerDown();
            });
        }
    }
}