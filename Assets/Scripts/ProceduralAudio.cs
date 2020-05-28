using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioLowPassFilter))]
public class ProceduralAudio : MonoBehaviour {
    private static float sampling_frequency = 48000;
    private const float doublePi = 2 * Mathf.PI;

    //[Range(0f, 1f)]
    //public float noiseRatio = 0.5f;

    //for noise part
    //[Range(-1f, 1f)]
    //public float offset;

    //public float cutoffOn = 800;
    //public float cutoffOff = 100;

    //public bool cutOff;

    //for tonal part
    [InspectorName("Frequency")]
    private float frequency = 440f;
    public float Frequency { get => frequency; set => frequency = value; }


    public float gain = 0.05f;

    private float increment;
    private float phase;
    private float tonalPart;



    //System.Random rand = new System.Random();

    void Awake() {
        sampling_frequency = AudioSettings.outputSampleRate;
    }

    void OnAudioFilterRead(float[] data, int channels) {
        //float noisePart = 0;

        // update increment in case frequency has changed
        increment = Frequency * doublePi / sampling_frequency;

        for (int i = 0; i < data.Length; i++) {

            //noise
            //noisePart = noiseRatio * (float)(rand.NextDouble() * 2.0 - 1.0 + offset);

            phase += increment;
            if (phase > doublePi) phase -= doublePi;

            //tone
            //tonalPart = (1f - noiseRatio) * (float)(gain * Mathf.Sin(phase));
            tonalPart = gain * Mathf.Sin(phase);

            //together
            data[i] = tonalPart;// + noisePart ;

            // if we have stereo, we copy the mono data to each channel
            if (channels == 2) {
                //data[i + 1] = data[i];
                data[i + 1] = tonalPart;
                i++;
            }
        }        
    }

}
