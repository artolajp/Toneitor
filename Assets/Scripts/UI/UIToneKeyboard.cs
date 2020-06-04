using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Toneitor {
    public class UIToneKeyboard : MonoBehaviour {
        [SerializeField] private Transform whiteNotesContanier = null;
        [SerializeField] private Transform blackNotesContanier = null;

        [SerializeField] private UIToneButton whiteKeyPrefab = null;
        [SerializeField] private UIToneButton blackKeyPrefab = null;
        [SerializeField] private GameObject spaceBetweenBlackKeysPrefab = null;
        [SerializeField] private GameObject halfSpaceBetweenBlackKeysPrefab = null;

        public void SetKeyboard(List<OctaveTone> octaveTones, Action<OctaveTone> onPressedKeys) {

            ClearChilddren(whiteNotesContanier);
            ClearChilddren(blackNotesContanier);
            Instantiate(halfSpaceBetweenBlackKeysPrefab, blackNotesContanier);
            UIToneButton keyButton;
            for (int i = 0; i < octaveTones.Count; i++) {
                if (octaveTones[i].Tone.ToneNatural) {
                    keyButton = Instantiate(whiteKeyPrefab, whiteNotesContanier);

                    if (i>1&& octaveTones[i-1].Tone.ToneNatural) Instantiate(spaceBetweenBlackKeysPrefab, blackNotesContanier);

                } else {
                    keyButton = Instantiate(blackKeyPrefab, blackNotesContanier);
                }
                keyButton.Tone = octaveTones[i];
                keyButton.OnClickButtonListener = onPressedKeys;
            }
            Instantiate(halfSpaceBetweenBlackKeysPrefab, blackNotesContanier);

        }

        private void ClearChilddren(Transform target) {
            for (int i = target.childCount-1; i >=0 ; i--) {
                Destroy(target.GetChild(i).gameObject);
            }
        }

    }
}
