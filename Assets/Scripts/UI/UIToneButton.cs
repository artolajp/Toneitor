using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Toneitor {
    public class UIToneButton : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI toneNameText;

        public Action<OctaveTone> OnClickButtonListener { get; set; }

        private OctaveTone tone;
        public OctaveTone Tone {
            set {
                tone = value;
                toneNameText.text = tone.ToneName;
            }
        }

        public void OnClickButton() {
            OnClickButtonListener(tone);
        }

    }
}


