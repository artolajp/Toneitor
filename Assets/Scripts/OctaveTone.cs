using System;
using UnityEngine;

namespace Toneitor {
    public class OctaveTone : IComparable<OctaveTone>, IEquatable<OctaveTone>
    {
        private Tone tone; 
        public Tone Tone { get { return tone; } }
        private float frequency;
        public float Frequency { get { return frequency; } }
        public string Name { get { return tone.ToneName + Octave; } }
        public string ToneName { get { return tone.ToneName; } }
        private int octave;
        public int Octave { get { return octave; } }

        public OctaveTone(Tone tone, int octave) {
            this.tone = tone;
            this.octave = octave;
            frequency = tone.BaseFrequency * Mathf.Pow(2, octave); 
        }

        public int CompareTo(OctaveTone obj) {
            return frequency.CompareTo(obj.frequency);
        }

        public bool Equals(OctaveTone other) {
            if (other == null) return false;
            return frequency.Equals(other.frequency);
        }
    }
}