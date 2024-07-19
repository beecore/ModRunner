using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum BonusType
{
    Addition, Difference, Product, Division
}
public class Door : MonoBehaviour
{
    

    [Header("Elements")]
    [SerializeField]  private SpriteRenderer rightDoorRenderer;
    [SerializeField]  private SpriteRenderer leftDoorRenderer;
    [SerializeField]  private TextMeshPro rightDoorText;
    [SerializeField]  private TextMeshPro leftDoorText;
    [SerializeField]  private Collider colliderDoor;

    [Header("Settings")]
    [SerializeField]  private BonusType rightDoorBonusType;
    [SerializeField]  private int rightDoorBonusAmout;

    [SerializeField]  private BonusType leftDoorBonusType;
    [SerializeField]  private int leftDoorBonusAmout;

    [SerializeField]  private Color bonusColor;
    [SerializeField]  private Color penaltyColor;

    // Start is called before the first frame update
    void Start()
    {
        ConfigureDoors();
    }
    public int GetBonusAmount(float xPostion)
    {
        if (xPostion > 0)
            return rightDoorBonusAmout;
        else
            return leftDoorBonusAmout;

    }
    public BonusType GetBonusType(float xPostion)
    {
        if (xPostion > 0)
            return rightDoorBonusType;
        else
            return leftDoorBonusType ;
    }

    public void Disable()
    {
        colliderDoor.enabled = false;
    }
    private void ConfigureDoors()
    {
        //Cấu hình màu cửa và số điểm bên phải
        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmout;
                break;
            case BonusType.Difference:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmout;
                break;
            case BonusType.Product:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "x" + rightDoorBonusAmout;
                break;
            case BonusType.Division:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmout;
                break;

        }


        //Cấu hình màu cửa và số điểm bên trái
        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmout;
                break;
            case BonusType.Difference:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmout;
                break;
            case BonusType.Product:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "x" + leftDoorBonusAmout;
                break;
            case BonusType.Division:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmout;
                break;

        }

    }
}
