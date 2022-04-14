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
        private static HDAudioMixerSO editedMixer;
        private ListView mixerLs;
        private HDAudioMixerSO[] mixers;

        #region Default
        [MenuItem("HerbiDino/Audio Mixer")]
        private static void ShowWindow()
        {
            var window = GetWindow<HDAudioMixerEditor>();
            window.titleContent = new GUIContent("Audio Mixer");
            window.Show();
        }

        [OnOpenAssetAttribute(1)]
        private static bool OnOpenAsset(int id, int line)
        {
            var instance = EditorUtility.InstanceIDToObject(id) as HDAudioMixerSO;
            if (instance != null)
            {
                editedMixer = instance;
                ShowWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            LoadTreeAsset();
            LoadAllMixers();

            SetupMixerLs();
            SetupCreateMixer();
            SetupRemoveMixer();

            SetupCreateEffect();

            ShowMixerOnEnable();
        }
        #endregion

        #region Setup
        private void SetupMixerLs()
        {
            mixerLs = rootVisualElement.Query<ListView>("mixer-list").First();
            mixerLs.makeItem = () => new Label();
            mixerLs.bindItem = (element, i) => (element as Label).text = mixers[i].name;
            mixerLs.itemsSource = mixers;
            mixerLs.itemHeight = 16;
            mixerLs.selectionType = SelectionType.Single;
            mixerLs.onSelectionChange += ShowMixerInfo;
        }

        private void SetupCreateMixer()
        {
            TextField newMixerName = rootVisualElement.Query<TextField>("new-mixer-name").First();
            newMixerName.value = "New Mixer";

            var createMixerBtn = rootVisualElement.Query<Button>("mixer-create-button").First();
            createMixerBtn.text = "New Mixer";
            createMixerBtn.clicked += () =>
            {
                CreateMixer(newMixerName.value);
                newMixerName.value = "New Mixer";
            };
        }

        private void SetupRemoveMixer()
        {
            var removeMixerBtn = rootVisualElement.Query<Button>("mixer-remove-button").First();
            removeMixerBtn.text = "Remove Mixer";
            removeMixerBtn.clicked += RemoveMixer;
        }
        #endregion

        #region Update
        private void CreateMixer(string newHDAudioMixerName)
        {
            if (String.IsNullOrWhiteSpace(newHDAudioMixerName))
                return;

            var newMixer = ScriptableObject.CreateInstance<HDAudioMixerSO>();
            AssetDatabase.CreateAsset(newMixer, "Assets/Demo/" + newHDAudioMixerName + ".asset");
            AssetDatabase.SaveAssets();
            ReloadAllMixers();

            editedMixer = newMixer;
            mixerLs.selectedIndex = Array.FindIndex(mixers, audioMixer => audioMixer.Equals(editedMixer));
            ShowMixerInfo(new HDAudioMixerSO[] { mixers[mixerLs.selectedIndex] });
        }

        private void RemoveMixer()
        {
            if (editedMixer == null)
                return;

            string audioMixerPath = "Assets/Demo/" + editedMixer.name + ".asset";
            AssetDatabase.DeleteAsset(audioMixerPath);
            ReloadAllMixers();

            if (mixers.Length > 0)
            {
                mixerLs.selectedIndex = 0;
                ShowMixerInfo(new HDAudioMixerSO[] { mixers[mixerLs.selectedIndex] });
            }
            else
                ShowMixerInfo(new HDAudioMixerSO[0]);
        }
        #endregion

        #region Show
        private void ShowMixerInfo(IEnumerable<object> objs)
        {
            foreach (var obj in objs)
            {
                editedMixer = obj as HDAudioMixerSO;

                var propRoot = rootVisualElement.Query<ScrollView>("mixer-properties").First();
                propRoot.Clear();

                foreach (var sfx in editedMixer.Effects)
                    ShowEffectInfo(new SerializedObject(sfx), propRoot);
            }
        }

        private void ShowEffectInfo(SerializedObject sfx, ScrollView propRoot)
        {
            SerializedProperty sfxProp = sfx.GetIterator();
            sfxProp.Next(true);

            while (sfxProp.NextVisible(false))
            {
                var propField = new PropertyField(sfxProp);

                propField.SetEnabled(sfxProp.name != "m_Script");
                propField.Bind(sfx);
                propRoot.Add(propField);
            }
        }

        private void ShowMixerOnEnable()
        {
            if (editedMixer != null)
            {
                mixerLs.selectedIndex = Array.FindIndex(mixers, audioMixer => audioMixer.Equals(editedMixer));
                ShowMixerInfo(new HDAudioMixerSO[] { mixers[mixerLs.selectedIndex] });
                return;
            }

            if (mixers.Length > 0)
            {
                mixerLs.selectedIndex = 0;
                ShowMixerInfo(new HDAudioMixerSO[] { mixers[mixerLs.selectedIndex] });
            }
        }
        #endregion

        #region Load
        private VisualElement LoadTreeAsset()
        {
            rootVisualElement.Add(Resources.Load<VisualTreeAsset>("HDAudioMixerEditor").CloneTree());
            rootVisualElement.styleSheets.Add(Resources.Load<StyleSheet>("HDAudioMixerEditor_Style"));
            return rootVisualElement;
        }

        private void LoadAllMixers()
        {
            var guids = AssetDatabase.FindAssets("t:HDAudioMixerSO");
            mixers = new HDAudioMixerSO[guids.Length];
            for (int i = 0; i < guids.Length; ++i)
                mixers[i] = AssetDatabase.LoadAssetAtPath<HDAudioMixerSO>(AssetDatabase.GUIDToAssetPath(guids[i]));
        }

        private void ReloadAllMixers()
        {
            LoadAllMixers();
            mixerLs.itemsSource = mixers;
            mixerLs.Refresh();
        }
        #endregion

        #region EffectManagement
        private void SetupCreateEffect()
        {
            var sfxType = rootVisualElement.Query<EnumField>("mixer-type").First();
            sfxType.Init(HDEffectType.Chorus);
        }

        private void RemoveEffect()
        {

        }

        private void MoveEffectUp()
        {

        }

        private void MoveEffectDown()
        {

        }
        #endregion
    }
}