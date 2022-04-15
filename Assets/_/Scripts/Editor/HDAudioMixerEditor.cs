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
        private HDAudioMixerEditorManager Manager
        {
            get
            {
                manager ??= new HDAudioMixerEditorManager();
                return manager;
            }
        }

        private ListView mixerListView;
        private ScrollView effectScrollView;
        private HDAudioMixerEditorManager manager = null;

        private void OnEnable()
        {
            SetupManager();
            SetupVisual();
        }

        private void SetupManager()
        {
            Manager.onMixerChange.AddListener(ShowMixer);
        }

        private void SetupVisual()
        {
            LoadVisualTreeAsset();

            SetupMixerListView();
            SetupEffectScrollView();
        }

        private void SetupMixerListView()
        {
            mixerListView = rootVisualElement.Query<ListView>("mixerLs");
            mixerListView.itemsSource = manager.MixerList;
            mixerListView.makeItem = () => new Label();
            mixerListView.bindItem = (element, index) => (element as Label).text = manager.MixerList[index].name;
            mixerListView.itemHeight = 20;
            mixerListView.selectionType = SelectionType.Single;
            mixerListView.onSelectionChange += mixers =>
            {
                foreach (var mixer in mixers)
                {
                    ShowMixer(mixer as HDAudioMixerSO);
                    return;
                }
            };
        }

        private void ShowMixer(HDAudioMixerSO mixer)
        {

        }

        private void SetupEffectScrollView()
        {
            effectScrollView = rootVisualElement.Query<ScrollView>("effectLs");
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