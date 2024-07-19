using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private TextMeshProUGUI LevelText;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        progressBar.value = 0;
        LevelText.text = "Level " + (ChunkManager.Instance.GetLevel() + 1).ToString();
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        HiddenShopPanel();
        GameManager.onGameStateChanged += GameStateChangeCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallBack;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChangeCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            ShowGameOver();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            ShowLevelComplete();
        }
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ContinueButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
       
    }
    public void HiddenSettingPanel()
    {
        settingPanel.SetActive(false);

    }
    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        float progress = PlayerController.Instance.transform.position.z / ChunkManager.Instance.GetFinishZ();

        progressBar.value = progress;
    }
    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();

    }
    public void HiddenShopPanel()
    {
        shopPanel.SetActive(false);

    }
}