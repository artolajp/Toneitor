using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Toneitor
{
    [RequireComponent(typeof(ProceduralAudio))]
    public class ToneControllerBehaviour : MonoBehaviour, IToneControllerBehaviour
    {

        private ProceduralAudio proceduralAudio;

        private bool isPlaying;
        public bool IsPlaying { get => isPlaying; }

        private bool isInUse;
        public bool IsInUse {
            get => isInUse;
            set {
                if (!value) {
                    MuteTone();
                }
                isInUse = value;
            }
        }

        private OctaveTone currentTone;
        public OctaveTone GetCurrentTone => currentTone;

        OctaveTone IToneControllerBehaviour.CurrentTone => currentTone;

        private void Awake() {
            proceduralAudio = GetComponent<ProceduralAudio>();
            proceduralAudio.Frequency = 0;
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