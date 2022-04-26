using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HerbiDino.Audio
{
    public class HDAudioMixerManager : HDSingleton<HDAudioMixerManager>
    {
        [SerializeField] private string storagePath = null;

        public HDAudioMixerSO GetMixer(string name)
        {
            return MixerDict[name];
        }

        private Dictionary<string, HDAudioMixerSO> mixerDict = null;
        private Dictionary<string, HDAudioMixerSO> MixerDict
        {
            get
            {
                if (mixerDict == null)
                {
                    mixerDict = new Dictionary<string, HDAudioMixerSO>();

                    var mixerLs = LoadAllMixers();
                    foreach (var mixer in mixerLs)
                    {
                        if (mixer == null) continue;
                        mixerDict[mixer.name] = mixer;
                    }
                }

                return mixerDict;
            }
        }

        private List<HDAudioMixerSO> LoadAllMixers()
        {
            var objs = Resources.LoadAll(storagePath, typeof(HDAudioMixerSO));
            var mixers = objs.Select(obj => obj as HDAudioMixerSO);
            return mixers.ToList();
        }
    }
}
