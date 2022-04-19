using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace HerbiDino.Audio
{
    public class HDAudioMixerEditorManager
    {
        public HDAudioMixerSO EditingMixer
        {
            get => editingMixer;
            set
            {
                editingMixer = value;
                onChangeMixer?.Invoke(value);
            }
        }
        public string StoragePath
        {
            get => storagePath; set
            {
                var isValidDir = CheckStorageDir(value);
                if (isValidDir) storagePath = value;

                onChangeStoragePath?.Invoke(isValidDir);
            }
        }
        public List<HDAudioMixerSO> MixerList { get; set; }

        public UnityEvent<bool> onChangeStoragePath;
        public UnityEvent onChangeMixerList;
        public UnityEvent<HDAudioMixerSO> onChangeMixer;
        public UnityEvent onChangeEditingMixer;

        private static string storagePath = null;
        private HDAudioMixerSO editingMixer;

        public HDAudioMixerEditorManager()
        {
            onChangeStoragePath = new UnityEvent<bool>();
            onChangeMixerList = new UnityEvent();
            onChangeMixer = new UnityEvent<HDAudioMixerSO>();
            onChangeEditingMixer = new UnityEvent();

            LoadAllMixers();
        }

        public void CreateMixer(string name)
        {
            if (storagePath == null || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var mixer = ScriptableObject.CreateInstance<HDAudioMixerSO>();

            AssetDatabase.CreateFolder(storagePath, name);
            AssetDatabase.CreateAsset(mixer, $"{GetMixerDir(name)}{name}.asset");
            AssetDatabase.SaveAssets();

            LoadAllMixers();
            EditingMixer = mixer;
        }

        public void RemoveMixer()
        {
            if (EditingMixer == null)
            {
                return;
            }

            AssetDatabase.DeleteAsset(GetMixerDir(EditingMixer.name));

            LoadAllMixers();
            EditingMixer = MixerList.Count > 0 ? MixerList[0] : null;
        }

        public void CreateEffect(HDEffectType sfxType)
        {
            if (!CanCreateEffect(sfxType))
            {
                return;
            }

            var sfx = CreateEffectInstance(sfxType);

            AssetDatabase.CreateAsset(sfx, GetEffectDir(EditingMixer.name, sfx.Type));
            AssetDatabase.SaveAssets();

            EditingMixer.Effects.Add(sfx);
            EditorUtility.SetDirty(EditingMixer);

            onChangeEditingMixer?.Invoke();
        }

        public void RemoveEffect(int sfxIndex)
        {
            if (EditingMixer == null || sfxIndex >= EditingMixer.Effects.Count)
            {
                return;
            }

            var sfx = EditingMixer.Effects[sfxIndex];
            EditingMixer.Effects.RemoveAt(sfxIndex);

            AssetDatabase.DeleteAsset(GetEffectDir(EditingMixer.name, sfx.Type));
            AssetDatabase.SaveAssets();

            onChangeEditingMixer?.Invoke();
        }

        private string GetMixerDir(string name)
        {
            if (storagePath.EndsWith("/"))
                return $"{storagePath}{name}/";

            return $"{storagePath}/{name}/";
        }

        private string GetEffectDir(string name, HDEffectType sfxType)
        {
            return $"{GetMixerDir(name)}{sfxType}.asset";
        }

        private bool CanCreateEffect(HDEffectType sfxType)
        {
            foreach (var sfx in EditingMixer.Effects)
                if (sfx.Type == sfxType)
                    return false;

            return true;
        }

        private HDAudioEffectSO CreateEffectInstance(HDEffectType sfxType)
        {
            switch (sfxType)
            {
                case HDEffectType.Chorus:
                    return ScriptableObject.CreateInstance<HDChorusEffectSO>();
                case HDEffectType.Distortion:
                    return ScriptableObject.CreateInstance<HDDistortionEffectSO>();
                case HDEffectType.Echo:
                    return ScriptableObject.CreateInstance<HDEchoEffectSO>();
                case HDEffectType.HighPass:
                    return ScriptableObject.CreateInstance<HDHighPassEffectSO>();
                case HDEffectType.LowPass:
                    return ScriptableObject.CreateInstance<HDLowPassEffectSO>();
                default:
                    return ScriptableObject.CreateInstance<HDReverbEffectSO>();
            }
        }

        private void LoadAllMixers()
        {
            MixerList = new List<HDAudioMixerSO>();

            var guids = AssetDatabase.FindAssets("t:HDAudioMixerSO");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var mixer = AssetDatabase.LoadAssetAtPath<HDAudioMixerSO>(path);
                MixerList.Add(mixer);
            }

            onChangeMixerList?.Invoke();
        }

        private bool CheckStorageDir(string storageDir)
        {
            return AssetDatabase.IsValidFolder(storageDir);
        }
    }
}