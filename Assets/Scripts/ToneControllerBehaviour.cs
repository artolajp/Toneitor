using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Toneitor
{
    [RequireComponent(typeof(ProceduralAudio))]
    public class ToneControllerBehaviour : MonoBehaviour, IToneControllerBehaviour
    {

        private ToneController tc = new ToneController();

        private ProceduralAudio proceduralAudio;

        [SerializeField] private string startTone = "C";
        [SerializeField] private int startOctave = 3;
        [SerializeField] private int longScale = 8;

        [SerializeField] private float[] rithm ={ 1, 0.5f, 0.5f, 1, 0.5f, 0.5f };

        [SerializeField] [Range(10, 240)] private float tempo = 60f;

        private List<OctaveTone> currentScale;

        private bool isPlaying;
        public bool IsPlaying { get => isPlaying;}

        private OctaveTone currentTone;

        OctaveTone IToneControllerBehaviour.CurrentTone => currentTone;

        [SerializeField] private UIToneKeyboard toneKeyboard=null;
        [SerializeField] private TextMeshProUGUI feedbackText=null;

        private int streak;

        private void Start() {
            proceduralAudio = GetComponent<ProceduralAudio>();
            MuteTone();
            //StartCoroutine(PlayToneRoutine());
            List<OctaveTone> keyboardTones = new List<OctaveTone>(12);            
            OctaveTone currentTone = tc.GetOctaveTone("C", startOctave);
            for (int i = 0; i < 12; i++) {
                keyboardTones.Add(currentTone);
                currentTone = tc.GetNextSemiTone(currentTone);
            }
            toneKeyboard.SetKeyboard(keyboardTones, CheckTone);
            PlayRandomTone();
            feedbackText.text = "Choose your destiny!";
            streak = 0;
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

        private void CheckTone(OctaveTone octaveTone) {
            if (currentTone.Tone== octaveTone.Tone) {
                PlayRandomTone();
                Debug.Log("YEA!");
                streak++;
                feedbackText.text = "Yeah " + streak+"!";
                
            } else {
                Debug.Log("NO! Expected "+ currentTone.ToneName);
                feedbackText.text = "<color=red>Oh no!</color>";
                streak = 0; 
            }
        }

        private void PlayRandomTone() {
            if (streak > 5) {
                PlayTone(tc.GetRandomOctaveTone(3));
            } else {
                PlayTone(tc.GetScale(tc.GetOctaveTone("C",3), 7, ToneController.MajorScale)[Random.Range(0,7)]);
            }
        }

    }
}