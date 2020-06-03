using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toneitor
{
    [RequireComponent(typeof(ProceduralAudio))]
    public class ToneControllerBehaviour : MonoBehaviour, IToneControllerBehaviour
    {

        private ToneController tc = new ToneController();

        private ProceduralAudio proceduralAudio;

        [SerializeField] private string startTone = "C";
        [SerializeField] private int startOctave = 2;
        [SerializeField] private int longScale = 8;

        [SerializeField] private float[] rithm ={ 1, 0.5f, 0.5f, 1, 0.5f, 0.5f };

        [SerializeField] [Range(10, 240)] private float tempo = 60f;

        private List<OctaveTone> currentScale;

        private bool isPlaying;
        public bool IsPlaying { get => isPlaying;}

        private OctaveTone currentTone;

        OctaveTone IToneControllerBehaviour.CurrentTone => currentTone;

        private void Start() {
            proceduralAudio = GetComponent<ProceduralAudio>();
            StartCoroutine(PlayToneRoutine());
        }

        private IEnumerator PlayToneRoutine() {
            int index = 0;
            currentScale = tc.GetScale(tc.GetOctaveTone(startTone, startOctave), longScale, ToneController.MajorScale);
            OctaveTone octaveTone;
            int rithmLenght = rithm.Length;
            float time = 0;
            isPlaying = true;
            while (isPlaying) {
                octaveTone = currentScale[UnityEngine.Random.Range(0, longScale)];
                PlayTone(octaveTone);
                //Debug.Log(octaveTone.Name);
                time = rithm[index] * 60 / tempo;
                yield return new WaitForSecondsRealtime(time);
                MuteTone();
                yield return new WaitForEndOfFrame();
                index++;
                if (index >= rithmLenght) {
                    index = 0;
                }
            }
        }

        public void PlayTone(OctaveTone octaveTone) {
            currentTone = octaveTone;
            proceduralAudio.Frequency = octaveTone.Frequency;
        }

        public void MuteTone() {
            currentTone = null;
            proceduralAudio.Frequency = 0;
        }


    }
}