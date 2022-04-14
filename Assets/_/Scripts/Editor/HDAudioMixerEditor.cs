using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace HerbiDino.Audio
{
    public class HDAudioMixerEditor : EditorWindow
    {
        private HDAudioMixerEditorManager manager;
        private ListView mixerListView, effectListView;

        private void OnEnable()
        {
            SetupManager();
            SetupVisual();
        }

        private void SetupManager()
        {
            manager = new HDAudioMixerEditorManager();
            manager.onMixerChange.AddListener(ShowMixer);
        }

        private void SetupVisual()
        {
            LoadVisualTreeAsset();
        }

        private void ShowMixer(HDAudioMixerSO mixer)
        {

        }

        [MenuItem("HerbiDino/Audio/Audio Mixer")]
        private static void ShowWindow()
        {
            var window = GetWindow<HDAudioMixerEditor>();
            window.titleContent = new GUIContent("Audio Mixer");
        }

        private void LoadVisualTreeAsset()
        {
            var tree = Resources.Load<VisualTreeAsset>("HDAudioMixerEditor").CloneTree();
            rootVisualElement.Add(tree);

            var style = Resources.Load<StyleSheet>("HDAudioMixerEditor_Style");
            rootVisualElement.styleSheets.Add(style);
        }
    }
}