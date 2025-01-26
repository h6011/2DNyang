using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEffectCtrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Target;


    private void Start()
    {
        SetVisibleTarget(false);
    }

    private void SetVisibleTarget(bool boolean)
    {
        Target.SetActive(boolean);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        SetVisibleTarget(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetVisibleTarget(false);
    }



}
