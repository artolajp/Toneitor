using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tone", menuName = "Tone")]
public class Tone : ScriptableObject {
    [SerializeField] private string toneName ;
    public string ToneName { get => toneName; }

    [SerializeField] private float baseFrequency;
    public float BaseFrequency { get => baseFrequency; }
}
