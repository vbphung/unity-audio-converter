using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HerbiDino.Audio;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private List<HDAudioMixerSO> mixers = new List<HDAudioMixerSO>();
    [SerializeField] private Transform buttonList;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private HDAudioSource audioSource;

    private void Start()
    {
        foreach (Transform child in buttonList)
            Destroy(child.gameObject);

        foreach (var mixer in mixers)
        {
            var btn = Instantiate(buttonPrefab, buttonList);
            btn.onClick.AddListener(() => audioSource.SetMixer(mixer));
            btn.GetComponentInChildren<Text>().text = mixer.name;
        }
    }
}
