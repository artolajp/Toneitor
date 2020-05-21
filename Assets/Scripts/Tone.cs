using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tone", menuName = "Tone")]
public class Tone : ScriptableObject, IComparable<Tone> {
    [SerializeField] private string toneName ;
    public string ToneName { get => toneName; }

    [SerializeField] private float baseFrequency;
    public float BaseFrequency { get => baseFrequency; }

    public int CompareTo(Tone obj) {
        return baseFrequency.CompareTo(obj.baseFrequency);
    }
}
