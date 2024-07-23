using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField]
    private AudioSource winGameSound;
    [SerializeField]
    private AudioSource coinSound;
    // Start is called before the first frame update

    void Start()
    {
        PlayerDetection.onDoorHit += PlayHitDoorSound;
        PlayerDetection.onCoinHit += PlayCoinSound;
        PlayerDetection.onWinHit += PlayWinSound;
        Enemy.onRunnerDie += PlayRunnerDieSound;
        GameManager.onGameStateChanged += GameStateChangeCallBack;
    }
    private void OnDestroy()
    {
        PlayerDetection.onCoinHit -= PlayCoinSound;
        PlayerDetection.onDoorHit -= PlayHitDoorSound;
        PlayerDetection.onWinHit -= PlayWinSound;
        Enemy.onRunnerDie -= PlayRunnerDieSound;
        GameManager.onGameStateChanged -= GameStateChangeCallBack;
    }
    private void GameStateChangeCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            gameOverSound.Play();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            levelCompleteSound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayHitDoorSound()
    {
        doorHitSound.Play();
    }
    public void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
        
    }
    public void DisableSound()
    {
        doorHitSound.volume=0;
        runnerDieSound.volume = 0; 
        levelCompleteSound.volume = 0; 
        gameOverSound.volume = 0;
        buttonSound.volume = 0;
}
    public void EnableSound()
    {
        doorHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
        buttonSound.volume = 1;
    }
    public void PlayWinSound()
    {
        winGameSound.Play();
    }
    public void PlayCoinSound()
    {
        coinSound.Play();
    }
}
