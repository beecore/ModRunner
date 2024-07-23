using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Sprite[] skins;
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private int skinPrice;
    [SerializeField] private TMP_Text priceText;
    public static Action<int> onSkinSelected;
    private void Awake()
    {
        UnLockSkin(0);
        priceText.text = skinPrice.ToString();
    }
    IEnumerator Start()
    {
        ConfigureButtons();
        UpdatePurchaseButton();
        yield return null;
        SelectSkin(GetLastSelectedSkin());
        
    }
    
    private void ConfigureButtons()
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;
            skinButtons[i].Configure(skins[i], unlocked);
            int skinIndex = i;
            skinButtons[i].GetButton().onClick.AddListener(()=>SelectSkin(skinIndex));
        }
    }
    public void UnLockSkin(int skinIndex)
    {

        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();

    }
    public void UnLockSkin(SkinButton skinButton)
    {

        int index = skinButton.transform.GetSiblingIndex();
        UnLockSkin(index);

    }
    private void SelectSkin(int skinIndex)
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Select();
            else
                skinButtons[i].Deselect();
        }
        onSkinSelected?.Invoke(skinIndex);
        SaveLastSelectedSkin(skinIndex);
    }
    public void PurchaseSkin()
    {
        //Danh sách skin còn khóa
        List<SkinButton> skinButtonsList = new List<SkinButton>();
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
            {
                skinButtonsList.Add(skinButtons[i]);
            }
        }
        //Không có thì ko làm gì
        if (skinButtonsList.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonsList[UnityEngine.Random.Range(0, skinButtonsList.Count)];
        UnLockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataManager.Instance.UseCoins(skinPrice);
        UpdatePurchaseButton();
    }
    public void UpdatePurchaseButton()
    {
        if (DataManager.Instance.GetCoins() < skinPrice)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }

    }
    private int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }
    private void SaveLastSelectedSkin(int indexSkin)
    {
       PlayerPrefs.SetInt("lastSelectedSkin", indexSkin);
    }
    public void ShowAds()
    {
        AdsManager.Instance.ShowRewardedAd();

    }
}
