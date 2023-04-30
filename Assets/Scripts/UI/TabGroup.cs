using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabGroup : MonoBehaviour
{
    [SerializeField] List<TabBtn> btns;
    [SerializeField] List<GameObject> objs;
    public Sprite btnBgIdle;
    public Sprite btnBgHover;
    public Sprite btnBgActive;

    Dictionary<TabBtn, GameObject> tabs;
    TabBtn selectedBtn;

    void Awake()
    {
        tabs = new Dictionary<TabBtn, GameObject>();
        for (int i = 0; i < btns.Count; i++)
        {
            if (btns[i] == null) continue;
            if (i >= objs.Count) AddTab(btns[i], null);
            else AddTab(btns[i], objs[i]);
        }
    }
    public void AddTab(TabBtn btn, GameObject obj)
    {
        btn.tabGroup = this;
        tabs.Add(btn, obj);
    }

    public void OnTabEnter(TabBtn btn)
    {
        ResetTabs();
        if (selectedBtn != null && btn == selectedBtn) return;
        btn.background.sprite = btnBgHover;
    }

    public void OnTabExit(TabBtn btn)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabBtn btn)
    {
        if (selectedBtn != null) selectedBtn.Deselect();
        selectedBtn = btn;
        selectedBtn.Select();
        GameObject selectedObj = tabs[btn];
        ResetTabs();
        btn.background.sprite = btnBgActive;
        foreach (GameObject obj in tabs.Values)
        {
            if (obj == null) continue;
            if (obj == selectedObj) obj.SetActive(true);
            else obj.SetActive(false);
        }

    }

    public void ResetTabs()
    {
        foreach (TabBtn btn in tabs.Keys)
        {
            if (selectedBtn != null && btn == selectedBtn) continue;
            btn.background.sprite = btnBgIdle;
        }
    }
}
