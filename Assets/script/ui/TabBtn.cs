using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class TabBtn : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [HideInInspector] public TabGroup tabGroup;
    [HideInInspector] public Image background;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    void Awake()
    {
        background = GetComponent<Image>();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void Select()
    {
        if (onTabSelected != null) onTabSelected.Invoke();
    }

    public void Deselect()
    {
        if (onTabDeselected != null) onTabDeselected.Invoke();
    }
}
