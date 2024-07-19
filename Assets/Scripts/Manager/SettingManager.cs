using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingManager : MonoBehaviour
{

    [SerializeField]
    private SoundManager soundManager;
    [SerializeField]
    private Sprite optionOnSprite;
    [SerializeField]
    private Sprite optionOffSprite;
    [SerializeField]
    private Image soundButtonImage;
    private bool soundState=true;
    private void Awake()
    {
        soundState = PlayerPrefs.GetInt("sounds", 1) == 1;
    }
    // Start is called before the first frame update
    private void Start()
    {
        SetUp();
    }
    public void SetUp()
    {
        if (soundState)
        {
            EnableSounds();
        }
        else
        {
            DisableSounds();
        }
        
    }
    public void ChangeSoundState()
    {
        if (soundState)
        {
            DisableSounds();
        }else
        {
            EnableSounds();
        }
        soundState = !soundState;
        PlayerPrefs.SetInt("sounds", soundState ? 1 : 0);
    }
    void EnableSounds()
    {
        soundButtonImage.sprite = optionOnSprite;
        soundManager.EnableSound();
    }
    void DisableSounds()
    {
        soundButtonImage.sprite = optionOffSprite;
        soundManager.DisableSound();
    }
}
