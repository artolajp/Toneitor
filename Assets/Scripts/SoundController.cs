using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
namespace Toneitor
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private ToneControllerBehaviour toneControllerBehaviourPrefab = null;

        [SerializeField] private Transform toneControllersContainers = null;
        [SerializeField] private int startControllerCount = 3;

        private List<ToneControllerBehaviour> controllers;

        private void Awake() {
            Config();
        }

        private void Config() {
            controllers = new List<ToneControllerBehaviour>(startControllerCount);
            for (int i = 0; i < startControllerCount; i++) {
                NewToneController();
            }
        }

        private ToneControllerBehaviour NewToneController() {
            ToneControllerBehaviour newController = Instantiate(toneControllerBehaviourPrefab, toneControllersContainers);
            controllers.Add(newController);
            return newController;
        }


        public int PlayTone(OctaveTone tone, float seg) {

            ToneControllerBehaviour controller = null;
            for (int i = 0; i < controllers.Count; i++) {
                if (!controllers[i].IsInUse) {
                    controller = controllers[i];
                }
            }
            if (controller == null) {
                controller = NewToneController();
            }
            StartCoroutine(PlayToneRoutine(controller, tone, seg));
            return 0;
        }

        public void MuteTone(OctaveTone tone) {
            if (tone == null) return;
            controllers.ForEach(controller => {
                if (controller.IsInUse && controller.GetCurrentTone == tone) controller.MuteTone() ; 
            }
            );
        }

        private IEnumerator PlayToneRoutine(ToneControllerBehaviour toneController, OctaveTone tone, float seg) {

            toneController.IsInUse = true;
            toneController.PlayTone(tone);
            if (seg > 0) {
                yield return new WaitForSeconds(seg);
                toneController.IsInUse = false;
            } else {
                do {
                    yield return new WaitForEndOfFrame();                    
                } while (toneController.GetCurrentTone != null);
                toneController.IsInUse = false;
            }
        }
    }
}