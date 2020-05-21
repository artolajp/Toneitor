using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toneitor
{
    public class ToneControllerBehavior : MonoBehaviour
    {

        private ToneController tc = new ToneController();

        [SerializeField] private ProceduralAudio proceduralAudio;

        [SerializeField] private float[] rithm = new float[] { 1, 0.5f, 0.5f, 1, 0.5f, 0.5f };

        [SerializeField] [Range(10, 240)] private float tempo = 60f;

        private IEnumerator Start() {
            int index = 0;
            List<OctaveTone> scale = tc.GetScale(tc.GetOctaveTone("G", 3), 12, ToneController.MajorScale);
            while (true) {
                OctaveTone octaveTone = scale[UnityEngine.Random.Range(0, 9)];
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
    }
}