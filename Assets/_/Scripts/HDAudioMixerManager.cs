using System.Collections.Generic;
using UnityEditor;

namespace HerbiDino.Audio
{
    public class HDAudioMixerManager
    {
        private static Dictionary<string, HDAudioMixerSO> mixerDict = null;
        private static Dictionary<string, HDAudioMixerSO> MixerDict
        {
            get
            {
                if (mixerDict == null)
                {
                    mixerDict = new Dictionary<string, HDAudioMixerSO>();

                    var mixerLs = MixerList;
                    foreach (var mixer in mixerLs)
                        mixerDict[mixer.name] = mixer;
                }

                return mixerDict;
            }
        }

        private static List<HDAudioMixerSO> mixerLs = null;
        private static List<HDAudioMixerSO> MixerList
        {
            get
            {
                if (mixerLs == null)
                    mixerLs = LoadAllMixers();

                return mixerLs;
            }
        }

        public static HDAudioMixerSO GetMixerByName(string name)
        {
            return MixerDict[name];
        }

        public static List<HDAudioMixerSO> LoadAllMixers()
        {
            var mixers = new List<HDAudioMixerSO>();

            var guids = AssetDatabase.FindAssets("t:HDAudioMixerSO");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var mixer = AssetDatabase.LoadAssetAtPath<HDAudioMixerSO>(path);
                mixers.Add(mixer);
            }

            return mixers;
        }
    }
}
