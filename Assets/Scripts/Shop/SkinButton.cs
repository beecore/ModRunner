using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button thisButton;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;
    private bool unlocked;

    public void Configure(Sprite skinSpite, bool unlocked)
    {
        this.skinImage.sprite = skinSpite;
        this.unlocked = unlocked;
        if (unlocked)
            Unlock();
        else
            Lock();
    }
    public void Unlock()
    {
        //TH mở khóa nhân vật
        this.thisButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);
        unlocked = true;
    }
    public void Lock()
    {
        //TH  khóa nhân vật
        this.thisButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
        unlocked = false;
    }
    public void Select()
    {
        selector.SetActive(true);
    }
    public void Deselect()
    {
        selector.SetActive(false);
    }
    public bool IsUnlocked()
    {
        return unlocked;
    }
    public Button GetButton()
    {
        return thisButton;
    }
}
