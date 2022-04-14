using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace HerbiDino.Audio
{
    public class HDAudioMixerEditorManager
    {
        public HDAudioMixerSO EditingMixer { get; set; }
        public UnityEvent<HDAudioMixerSO> onCreateMixer;
        public UnityEvent<bool> onRemoveMixer;
        public UnityEvent<HDAudioEffectSO> onCreateEffect;
        public UnityEvent<bool> onRemoveEffect;

        private string storagePath = null;

        private const string MIXER_ASSET = "Mixer.asset";

        public void SetMixerStoragePath(string path)
        {
            storagePath = path;
        }

        public void CreateMixer(string name)
        {
            if (!AssetDatabase.IsValidFolder(storagePath) || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                onCreateMixer?.Invoke(null);
                return;
            }

            var mixer = ScriptableObject.CreateInstance<HDAudioMixerSO>();

            AssetDatabase.CreateFolder(storagePath, name);
            AssetDatabase.CreateAsset(mixer, $"{GetMixerDir(name)}{MIXER_ASSET}");
            AssetDatabase.SaveAssets();

            onCreateMixer?.Invoke(mixer);
        }

        public void RemoveMixer(string name)
        {
            if (EditingMixer == null)
            {
                onRemoveMixer?.Invoke(false);
                return;
            }

            AssetDatabase.DeleteAsset(GetMixerDir(EditingMixer.name));
            onRemoveMixer?.Invoke(true);
        }

        public void CreateEffect(HDEffectType sfxType)
        {
            if (!CanCreateEffect(sfxType))
            {
                onCreateEffect?.Invoke(null);
                return;
            }

            var sfx = CreateEffectInstance(sfxType);

            AssetDatabase.CreateAsset(sfx, GetEffectDir(EditingMixer.name, sfx.Type));
            AssetDatabase.SaveAssets();

            EditingMixer.Effects.Add(sfx);
            EditorUtility.SetDirty(EditingMixer);

            onCreateEffect?.Invoke(sfx);
        }

        public void RemoveEffect(int sfxIndex)
        {
            if (EditingMixer == null || sfxIndex >= EditingMixer.Effects.Count)
            {
                onRemoveEffect?.Invoke(false);
                return;
            }

            var sfx = EditingMixer.Effects[sfxIndex];
            EditingMixer.Effects.RemoveAt(sfxIndex);

            AssetDatabase.DeleteAsset(GetEffectDir(EditingMixer.name, sfx.Type));
            AssetDatabase.SaveAssets();

            onRemoveEffect?.Invoke(true);
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
    }
}