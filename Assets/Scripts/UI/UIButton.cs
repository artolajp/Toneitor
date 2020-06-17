using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button),typeof(EventTrigger))]
public class UIButton : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private Button button;

    [SerializeField] private float timeToHold=1;
    [SerializeField] private float timeToRepeat = 0.5f;
    [SerializeField] private float aceleration=1.5f;

    private Action onClickListener;
    public Action OnClickListener { set => onClickListener = value; }

    private Action onHoldListener;
    public Action OnHoldListener { set => onHoldListener = value; }

    private bool isClickAndHold;
    public bool IsClickAndHold { get => isClickAndHold; set => isClickAndHold = value; }

    private float currentHoldingTime;
    private float currentHoldingSpeed;

    private void Awake() {
        button = GetComponent<Button>();
        eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener(OnPointerDown);
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener(OnPointerEnter);
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener(OnPointerExit);
        eventTrigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener(OnPointerUp);
        eventTrigger.triggers.Add(entry);
    }

    private void FixedUpdate() {
        if (IsClickAndHold) {
            currentHoldingTime += Time.fixedDeltaTime; 
        } else {
            currentHoldingTime = 0;
        }
        if (currentHoldingTime>=timeToHold) {
            currentHoldingTime = timeToHold - currentHoldingSpeed;
            currentHoldingSpeed /= aceleration;
            onHoldListener?.Invoke();
        }
    }

    private void OnPointerDown(BaseEventData data) {
        onClickListener?.Invoke();
        IsClickAndHold = true;
        currentHoldingSpeed = timeToRepeat;
    }

    private void OnPointerEnter(BaseEventData data) {
    }

    private void OnPointerExit(BaseEventData data) {
        IsClickAndHold = false;
    }

    private void OnPointerUp(BaseEventData data) {
        IsClickAndHold = false;
    }
}
