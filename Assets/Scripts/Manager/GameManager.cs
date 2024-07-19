using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public enum GameState { Menu,Game,LevelComplete,GameOver}
    private GameState gameState;
    public static Action<GameState> onGameStateChanged;

    protected override void Awake()
    {
        base.Awake();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);

    }
    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
