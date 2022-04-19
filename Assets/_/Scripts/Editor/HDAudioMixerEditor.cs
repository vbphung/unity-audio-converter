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

        private HDAudioMixerSO EditingMixer { get => Manager.EditingMixer; set => Manager.EditingMixer = value; }

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
            Manager.onChangeMixer.AddListener(ShowMixer);
            Manager.onChangeEditingMixer.AddListener(() => ShowMixer(Manager.EditingMixer));
        }

        private void SetupVisual()
        {
            LoadVisualTreeAsset();

            SetupStoragePath();
            SetupMixerView();
            SetupEffectView();
        }

        private void SetupStoragePath()
        {
            var pathText = rootVisualElement.Query<TextField>(StorageUI.InputField).First();
            if (pathText == null) return;

            var checkPathBtn = rootVisualElement.Query<Button>(StorageUI.CheckButton).First();
            if (checkPathBtn == null) return;

            var checkResult = rootVisualElement.Query<TextElement>(StorageUI.ValidityText).First();
            if (checkResult == null) return;

            if (Manager.StoragePath != null)
            {
                pathText.value = Manager.StoragePath;
                SetStoragePathState(checkResult, true);
            }

            Manager.onChangeStoragePath.AddListener(isValidDir => SetStoragePathState(checkResult, isValidDir));

            checkPathBtn.clicked += () =>
            {
                Manager.StoragePath = pathText.value;
            };
        }

        private void SetupMixerView()
        {
            SetupMixerListView();
            SetupMixerManager();
        }

        private void SetupEffectView()
        {
            effectScrollView = rootVisualElement.Query<ScrollView>(EffectUI.ListView);
            SetupEffectManager();
        }

        private void ShowMixer(HDAudioMixerSO mixer)
        {
            effectScrollView.Clear();

            if (mixer == null) return;

            foreach (var sfx in mixer.Effects)
                ShowEffect(sfx);
        }

        private void ShowEffect(HDAudioEffectSO sfx)
        {
            var sfxView = new Box();
            sfxView.AddToClassList(EffectUI.View);
            sfxView.Add(CreateTextElement(UI.TitleText, sfx.Type.ToString()));

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

            effectScrollView.Add(sfxView);
        }

        private TextElement CreateTextElement(string type, string text)
        {
            var textElement = new TextElement();
            textElement.text = text;
            textElement.AddToClassList(type);

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

        private void SetupMixerListView()
        {
            mixerListView = rootVisualElement.Query<ListView>(MixerUI.ListView);
            mixerListView.itemsSource = Manager.MixerList;
            mixerListView.bindItem = (element, index) => (element as Label).text = Manager.MixerList[index].name;
            mixerListView.itemHeight = 20;
            mixerListView.selectionType = SelectionType.Single;

            mixerListView.makeItem = () =>
            {
                var item = new Label();
                item.AddToClassList(MixerUI.View);
                return item;
            };

            mixerListView.onSelectionChange += mixers =>
            {
                foreach (var mixer in mixers)
                {
                    EditingMixer = mixer as HDAudioMixerSO;
                    return;
                }
            };

            Manager.onChangeMixerList.AddListener(() =>
            {
                mixerListView.itemsSource = Manager.MixerList;
                mixerListView.Refresh();
            });
        }

        private void SetupMixerManager()
        {
            SetupMixerCreate();
            SetupMixerRemove();
        }

        private void SetupEffectManager()
        {
            SetupEffectCreate();
            SetupEffectRemove();
        }

        private void SetupMixerCreate()
        {
            var mixerName = rootVisualElement.Query<TextField>(MixerUI.NameText).First();
            if (mixerName == null) return;

            var createMixerBtn = rootVisualElement.Query<Button>(MixerUI.CreateButton).First();
            if (createMixerBtn == null) return;

            createMixerBtn.clicked += () => Manager.CreateMixer(mixerName.value);
        }

        private void SetupMixerRemove()
        {
            var removeMixerBtn = rootVisualElement.Query<Button>(MixerUI.RemoveButton).First();
            if (removeMixerBtn == null) return;

            removeMixerBtn.clicked += Manager.RemoveMixer;
        }

        private void SetupEffectCreate()
        {
            var sfxType = rootVisualElement.Query<EnumField>(EffectUI.TypeDropdown).First();
            if (sfxType == null) return;

            sfxType.Init(HDEffectType.Chorus);

            var createEffectBtn = rootVisualElement.Query<Button>(EffectUI.CreateButton).First();
            if (createEffectBtn == null) return;

            createEffectBtn.clicked += () => Manager.CreateEffect((HDEffectType)sfxType.value);
        }

        private void SetupEffectRemove()
        {
            var removeEffectBtn = rootVisualElement.Query<Button>(EffectUI.RemoveButton).First();
            if (removeEffectBtn == null) return;

            removeEffectBtn.clicked += () => Manager.RemoveEffect(0);
        }

        private void SetStoragePathState(TextElement stateText, bool isValid)
        {
            stateText.ClearClassList();
            if (isValid)
            {
                stateText.AddToClassList("safe");
                stateText.text = "Valid Path";
            }
            else
            {
                stateText.AddToClassList("error");
                stateText.text = "Invalid Path";
            }
        }
    }
}