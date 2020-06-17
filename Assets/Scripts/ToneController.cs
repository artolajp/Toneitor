using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toneitor {
    public class ToneController {

        private static ToneController instance;
        public static ToneController Instance {
            get {
                return instance ?? new ToneController();
            }
        }

        public static readonly List<bool> MajorScale = new List<bool>() { true,true,false,true,true,true,false};
        public static readonly List<bool> NaturalMinorScale = new List<bool>() { true,false,true,true,false,true,true};

        private static Tone[] tones;

        public ToneController() {
            LoadTones();
        }

        public Tone GetTone(string toneName) {
            int length = tones.Length;
            for(int i  = 0; i< length;i++) {
                if (tones[i].ToneName == toneName) return tones[i];
            }
            return null;
        }

        public OctaveTone GetOctaveTone(string noteName, int octave) {
            Tone tone = GetTone(noteName);
            if (tone!=null)
                return new OctaveTone(tone, octave);

            return null;
        }

        public Tone [] LoadTones() {
            tones = Resources.LoadAll<Tone>("");
            Array.Sort(tones);
            return tones;
        }

        public Tone GetRandomTone() {
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

        public List<OctaveTone> GetScale(OctaveTone tone, int count,List<bool> pattern) {
            List<OctaveTone> scale = new List<OctaveTone>(count);
            scale.Add(tone);
            OctaveTone semiTone = tone;
            int scaleLength = pattern.Count;
            for (int i = 0; i < count-1; i++) {
                semiTone = GetNextSemiTone(semiTone);
                if(pattern[i% scaleLength]) {
                    semiTone = GetNextSemiTone(semiTone);
                }
                scale.Add(semiTone);
            }

            return scale;
        }
    }
}