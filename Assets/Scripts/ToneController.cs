using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toneitor {
    public class ToneController : MonoBehaviour {

        [SerializeField] [Range(10, 240)] private float tempo = 60f;

        [SerializeField] private ProceduralAudio proceduralAudio;

        public float[] rithm = new float[] { 1, 0.5f, 0.5f, 1, 0.5f, 0.5f };

        public static readonly List<bool> MajorScale = new List<bool>() { true,true,false,true,true,true,false};

        private Tone [] tones;

        public Tone GetTone(string toneName) {
            if (tones == null) LoadTones();
            int length = tones.Length;
            for(int i  = 0; i< length;i++) {
                if (tones[i].ToneName == toneName) return tones[i];
            }
            return null;
        }


        //private float[] tones = new float[]{
        //220,246.94f,261.63f,293.66f,329.63f, 349.23f,392,
        //440,493.88f,523.25f,587.33f,659.26f,698.46f,789.99f};

        private IEnumerator Start() {
            int index = 0;
            List<OctaveTone> scale = GetScale(GetOctaveTone("G",4),10);
            while (true) {
                OctaveTone octaveTone = scale[UnityEngine.Random.Range(0,9)];
                proceduralAudio.frequency = octaveTone.Frequency;
                Debug.Log(octaveTone.Name);
                yield return new WaitForSeconds(rithm[index] * 60 / tempo);
                proceduralAudio.frequency = 0;
                yield return new WaitForEndOfFrame();
                index++;
                if (index >= rithm.Length) {
                    index = 0;
                }
            }
        }

        public OctaveTone GetOctaveTone(string noteName, int octave) {
            Tone tone = GetTone(noteName);
            if (tone!=null)
                return new OctaveTone(tone, octave);

            return null;
        }

        public Tone [] LoadTones() {
            tones = Resources.LoadAll<Tone>("");
            return tones;
        }

        public Tone GetRandomTone() {
            if(tones==null)LoadTones();
            return tones[UnityEngine.Random.Range(0, tones.Length)];
        }

        public OctaveTone GetRandomOctaveTone(int octave = 0) {
            Tone tone = GetRandomTone();
            if (octave > 0)
                return new OctaveTone(tone, octave);
            else
                return  new OctaveTone(tone, UnityEngine.Random.Range(1,6));
        }

        public OctaveTone GetNextSemiTone(OctaveTone tone) {
            if (tones == null) LoadTones();
            int indexTone = -1;
            int length = tones.Length;
            for (int i = 0; i < length; i++) {
                if (tones[i] == tone.Tone) indexTone = i;
            }
            if (indexTone < 0)
                return null;
            else if (indexTone < length - 1)
                return new OctaveTone(tones[indexTone + 1], tone.Octave);
            else
                return new OctaveTone(tones[0], tone.Octave + 1);
        }

        public List<OctaveTone> GetScale(OctaveTone tone, int count) {

            List<OctaveTone> scale = new List<OctaveTone>(count);
            scale.Add(tone);
            OctaveTone semiTone = tone;
            int scaleLength = MajorScale.Count;
            for (int i = 0; i < count-1; i++) {
                semiTone = GetNextSemiTone(semiTone);
                if(MajorScale[i% scaleLength]) {
                    semiTone = GetNextSemiTone(semiTone);
                }
                scale.Add(semiTone);
            }

            return scale;
        }
    }
}