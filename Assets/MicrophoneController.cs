using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private const int sampleSpectrum = 256;
    private float[] samples  = new float[sampleSpectrum];
    public int max;
    public int tone;
    public int channel =0;
    public int device =0;

    void Start()
    {
        string[] mics = Microphone.devices;

        Debug.Log("Mics");
        for (int i = 0; i < mics.Length; i++) {
            Debug.Log(mics[i]);
        }
        audioSource.clip = Microphone.Start(mics[device],true,10,AudioSettings.outputSampleRate);
        audioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(samples, channel, FFTWindow.Triangle);
        max = 0;
        float calc = 0;
        for (int i = 1; i < 511; i++) {
            if (samples[i] + samples[i + 1] - samples[i - 1] > calc) {
                max = i;
                calc = samples[max] + samples[max + 1] - samples[max - 1];
            }
        }
        tone =  max*22000 / sampleSpectrum;



    }


}
