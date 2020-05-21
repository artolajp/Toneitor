using UnityEngine;

namespace Toneitor {
    public class OctaveTone {
        private Tone tone; 
        public Tone Tone { get { return tone; } }
        private float frequency;
        public float Frequency { get { return frequency; } }
        public string Name { get { return tone.name + Octave; } }
        public string ToneName { get { return tone.name; } }
        private int octave;
        public int Octave { get { return octave; } }

        public OctaveTone(Tone tone, int octave) {
            this.tone = tone;
            this.octave = octave;
            frequency = tone.BaseFrequency * Mathf.Pow(2, octave); 
        }
    }
}