using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tone", menuName = "Tone")]
public class Tone : ScriptableObject, IComparable<Tone> {

    public enum Naming{ Latin,English,German}

    public static Naming CurrentNaming = Naming.English;

    [SerializeField] private string toneNameLatin ="" ;
    [SerializeField] private string toneNameLatinAlt ="";
    [SerializeField] private string toneNameEnglish = "";
    [SerializeField] private string toneNameEnglishAlt = "";
    [SerializeField] private string toneNameGerman = "";
    [SerializeField] private string toneNameGermanAlt = "";
    [SerializeField] private bool toneNatural = true;
    public bool ToneNatural { get => toneNatural;}

    public string ToneNameLatin { get => toneNameLatin; }
    public string ToneNameEnglish { get => toneNameEnglish; }
    public string ToneNameGerman { get => toneNameGerman; }


    [SerializeField] private float baseFrequency;
    public float BaseFrequency { get => baseFrequency; }

    public string ToneName { get {
            switch (CurrentNaming) {
                case Naming.Latin:
                    return toneNameLatin ;
                case Naming.English:
                    return toneNameEnglish ;
                case Naming.German:
                    return toneNameGerman ;
                default:
                    return toneNameEnglish ; 
            }
        }
    }

    public string ToneNameAlt {
        get {
            switch (CurrentNaming) {
                case Naming.Latin:
                    return toneNameLatinAlt;
                case Naming.English:
                    return toneNameEnglishAlt;
                case Naming.German:
                    return toneNameGermanAlt;
                default:
                    return toneNameEnglishAlt;
            }
        }
    }


    public int CompareTo(Tone obj) {
        return baseFrequency.CompareTo(obj.baseFrequency);
    }

}
