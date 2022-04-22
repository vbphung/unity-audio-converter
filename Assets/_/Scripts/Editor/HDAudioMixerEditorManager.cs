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
                EffectManager.CurrentMixer = value;
                onChangeMixer?.Invoke(value);
            }
        }

        public string StoragePath
        {
            get
            {
                if (storagePath != null) return storagePath;

                var guids = AssetDatabase.FindAssets("t:HDAudioMixerSO");
                if (guids.Length == 0) return null;

                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                var mixer = AssetDatabase.LoadAssetAtPath<HDAudioMixerSO>(path);

                var storage = path.Remove(path.IndexOf(mixer.name) - 1);
                if (IsValidStorageDir(storage))
                    storagePath = storage;

                return storagePath;
            }
            set
            {
                var isValidDir = IsValidStorageDir(value);
                if (isValidDir) storagePath = value;

                onChangeStoragePath?.Invoke(isValidDir);
            }
        }

        public List<HDAudioMixerSO> MixerList { get; set; }
        public HDAudioEffectManager EffectManager { get; private set; }

        public UnityEvent<bool> onChangeStoragePath;
        public UnityEvent onChangeMixerList;
        public UnityEvent<HDAudioMixerSO> onChangeMixer;
        public UnityEvent onChangeEditingMixer;

        private string storagePath = null;
        private HDAudioMixerSO editingMixer;

        public HDAudioMixerEditorManager()
        {
            onChangeStoragePath = new UnityEvent<bool>();
            onChangeMixerList = new UnityEvent();
            onChangeMixer = new UnityEvent<HDAudioMixerSO>();
            onChangeEditingMixer = new UnityEvent();

            EffectManager = new HDAudioEffectManager();
            EffectManager.onSwapEffects.AddListener(() => onChangeEditingMixer?.Invoke());

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
            if (storagePath == null && EditingMixer == null)
            {
                return;
            }

            AssetDatabase.DeleteAsset(GetMixerDir(EditingMixer.name));
            AssetDatabase.Refresh();

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
            AssetDatabase.Refresh();

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
                case HDEffectType.Reverb:
                    return ScriptableObject.CreateInstance<HDReverbEffectSO>();
                default:
                    return ScriptableObject.CreateInstance<HDAudioPitchShifterEffectSO>();
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

        private bool IsValidStorageDir(string storageDir)
        {
            return AssetDatabase.IsValidFolder(storageDir);
        }
    }
}