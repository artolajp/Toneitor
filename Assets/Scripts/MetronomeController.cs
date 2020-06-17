using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Toneitor;
using UnityEngine;
using UnityEngine.UI;

public class MetronomeController : MonoBehaviour
{
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Button okButton;
    [SerializeField] private TextMeshProUGUI numberText;

    [SerializeField] private float metronomeToneLong = 0.25f;
    [SerializeField] private string metronomeTone = "A";

    private Action onOkButtonListener;

    private int currentTempo;

    public int CurrentTempo { get => currentTempo; 
        set { 
            currentTempo = Mathf.Clamp(value,30,240);
            numberText.text = currentTempo.ToString();
        } 
    }

    private void OnUpButtonClick() {
        CurrentTempo++;
    }

    private void OnDownButtonClick() {
        CurrentTempo--;
    }

    private void OnOkButtonClick() {
        onOkButtonListener?.Invoke();
    }

    private void Awake() {
        currentTempo = 60;
        upButton.onClick.AddListener(OnUpButtonClick);
        downButton.onClick.AddListener(OnDownButtonClick);
        okButton.onClick.AddListener(OnOkButtonClick);
    }
    private void Start() {
        StartCoroutine(PlayMetronome());
    }

    private IEnumerator PlayMetronome() {
        while (true) {
            SoundController.Instance.PlayTone(ToneController.Instance.GetOctaveTone(metronomeTone, 4), metronomeToneLong);
            yield return new WaitForSecondsRealtime(60.0f / currentTempo);
        }
    }
}
