using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CrowdSystem crowdSystem;
    public static Action onDoorHit;
    public static Action onCoinHit;
    public static Action onWinHit;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameState())
        {
            DetectDoors();
        }
       
    }
    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);
        for(int i = 0; i < detectedColliders.Length; i++)
        {
            if(detectedColliders[i].TryGetComponent(out Door doors))
            {
                doors.Disable();
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);
                crowdSystem.ApplyBonus(bonusType, bonusAmount);
                //bat am thanh
                onDoorHit?.Invoke();
            }
            else if (detectedColliders[i].tag == "Finish")
            {
                onWinHit?.Invoke();
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
            }
            else if (detectedColliders[i].tag == "Coin")
            {
                onCoinHit?.Invoke();
                Destroy(detectedColliders[i].gameObject);
                DataManager.Instance.AddCoins(1);
            }
        }

    }
}
