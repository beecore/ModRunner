using TMPro;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    [SerializeField]
    private TMP_Text[] coinTexts;

    private int coin;

    protected override void Awake()
    {
        base.Awake();
        coin = PlayerPrefs.GetInt("coins", 0);
    }

    // Start is called before the first frame update
    private void Start()
    {
        UpdateCoinTexts();
    }

    public void UpdateCoinTexts()
    {
        foreach (TMP_Text textContain in coinTexts)
        {
            textContain.text = coin.ToString();
        }
    }

    public void AddCoins(int amout)
    {
        coin += amout;
        UpdateCoinTexts();
        PlayerPrefs.SetInt("coins", coin);
    }
    public void UseCoins(int amout)
    {
        coin -= amout;
        UpdateCoinTexts();
        PlayerPrefs.SetInt("coins", coin);
    }
    public int GetCoins()
    {
        return coin;
    }
}