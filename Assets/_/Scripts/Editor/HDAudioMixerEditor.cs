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

        private HDAudioMixerSO EditingMixer => Manager.EditingMixer;

        private ListView mixerListView;
        private ScrollView effectScrollView;
        private HDAudioMixerEditorManager manager = null;

        private const string MIXER_LIST = "mixerLs";
        private const string EFFECT_LIST = "effectLs";
        private const string EFFECT_VIEW = "effect";
        private const string TITLE = "title";

        private void OnEnable()
        {
            SetupManager();
            SetupVisual();
        }

        private void SetupManager()
        {
            Manager.onChangeMixer.AddListener(ShowMixer);
        }

        private void SetupVisual()
        {
            LoadVisualTreeAsset();

            SetupMixerListView();
            SetupEffectScrollView();
        }

        private void SetupMixerListView()
        {
            mixerListView = rootVisualElement.Query<ListView>(MIXER_LIST);
            mixerListView.itemsSource = Manager.MixerList;
            mixerListView.makeItem = () => new Label();
            mixerListView.bindItem = (element, index) => (element as Label).text = Manager.MixerList[index].name;
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

            Manager.onChangeMixerList.AddListener(() =>
            {
                mixerListView.itemsSource = Manager.MixerList;
                mixerListView.Refresh();
            });
        }

        private void ShowMixer(HDAudioMixerSO mixer)
        {
            effectScrollView.Clear();
            foreach (var sfx in EditingMixer.Effects)
                ShowEffect(sfx);
        }

        private void ShowEffect(HDAudioEffectSO sfx)
        {
            var sfxView = CreateEffectView(sfx);
            effectScrollView.Add(sfxView);
        }

        private Box CreateEffectView(HDAudioEffectSO sfx)
        {
            var sfxView = new Box();
            sfxView.AddToClassList(EFFECT_VIEW);
            sfxView.Add(CreateTextElement(TITLE, sfx.Type.ToString()));

            var sfxObj = new SerializedObject(sfx);

            var sfxProp = sfxObj.GetIterator();
            sfxProp.Next(true);

            while (sfxProp.NextVisible(false))
            {
                var propField = new PropertyField(sfxProp);

                propField.SetEnabled(sfxProp.name != "m_Script");
                propField.Bind(sfxObj);
                sfxView.Add(propField);
            }

            return sfxView;
        }

        private void SetupEffectScrollView()
        {
            effectScrollView = rootVisualElement.Query<ScrollView>(EFFECT_LIST);
        }

        private TextElement CreateTextElement(string className, string text)
        {
            var textElement = new TextElement();
            textElement.text = text;
            textElement.AddToClassList(className);

            return textElement;
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