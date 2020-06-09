using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toneitor {
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private ToneControllerBehaviour toneControllerBehaviourPrefab = null;

        [SerializeField] private Transform toneControllersContainers = null;

        //return index of controller
        public int PlayTone(OctaveTone tone, float seg ) {

            return 0;
        }

    }
}