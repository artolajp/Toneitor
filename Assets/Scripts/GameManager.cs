using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Toneitor
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SoundController soundController;

        [SerializeField] private UIToneKeyboard toneKeyboard = null;
        [SerializeField] private TextMeshProUGUI feedbackText = null;

        private int streak;

        [SerializeField] private string startTone = "C";
        [SerializeField] private int startOctave = 3;
        [SerializeField] private int longScale = 8;

        [SerializeField] private float[] rithm = { 1, 0.5f, 0.5f, 1, 0.5f, 0.5f };

        [SerializeField] [Range(10, 240)] private float tempo = 60f;

        private List<OctaveTone> currentScale;

        private ToneController toneController ;
        private OctaveTone currentTone;
        [SerializeField] bool isPlaying;


        private void Start() {

            toneController = new ToneController();
            List<OctaveTone> keyboardTones = new List<OctaveTone>(12);
            OctaveTone currentTone = toneController.GetOctaveTone("C", startOctave);
            for (int i = 0; i < 12; i++) {
                keyboardTones.Add(currentTone);
                currentTone = toneController.GetNextSemiTone(currentTone);
            }
            toneKeyboard.SetKeyboard(keyboardTones, CheckTone);
            PlayRandomTone();
            feedbackText.text = "Choose your destiny!";
            streak = 0;
            //StartCoroutine( PlayToneRoutine());
        }

        private void CheckTone(OctaveTone octaveTone) {
            
            if (currentTone.Tone == octaveTone.Tone) {
                PlayRandomTone();
                streak++;
                feedbackText.text = "Yeah " + streak + "!";

            } else {
                Debug.Log("NO! Expected " + currentTone.ToneName);
                feedbackText.text = "<color=red>Oh no!</color>";
                streak = 0;
                soundController.PlayTone(octaveTone, 1);
            }
        }

        private void PlayRandomTone() {
            soundController.MuteTone(currentTone);
            OctaveTone newTone;
            do  {
                if (streak > 5) {
                    newTone = toneController.GetRandomOctaveTone(3);
                } else {
                    newTone = toneController.GetScale(toneController.GetOctaveTone("C", 3), 7, ToneController.MajorScale)[Random.Range(0, 7)];
                }
            } while (newTone.Equals(currentTone));

            currentTone = newTone;
            soundController.PlayTone(currentTone, 0);
        }


        private IEnumerator PlayToneRoutine() {
            int index = 0;
            currentScale = toneController.GetScale(toneController.GetOctaveTone(startTone, startOctave), longScale, ToneController.MajorScale);
            OctaveTone octaveTone;
            int rithmLenght = rithm.Length;
            float time = 0;
            isPlaying = true;
            yield return new WaitForSecondsRealtime(1);
            while (isPlaying) {
                octaveTone = currentScale[UnityEngine.Random.Range(0, longScale)];

                time = rithm[index] * 60 / tempo;
                soundController.PlayTone(octaveTone, time);
                Debug.Log(octaveTone.ToneName);
                yield return new WaitForSecondsRealtime(time);
                index++;
                if (index >= rithmLenght) {
                    index = 0;
                }
            }
        }
    }
}