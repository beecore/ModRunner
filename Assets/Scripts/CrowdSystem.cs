using UnityEngine;
using DG.Tweening;
public class CrowdSystem : MonoBehaviour
{
    [SerializeField] private Transform runnerParents;
    [SerializeField] private GameObject runnerPrefabs;
    [SerializeField] private PlayerAnimator playerAnimator;

    public float radius;
    public float angle;
    private float countNumbers;

    // Start is called before the first frame update
    private void Start()
    {
        countNumbers = runnerParents.childCount;
        PlaceRunners();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        if (runnerParents.childCount <= 0)
        {
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnerParents.childCount; i++)
        {
            Vector3 positionChild = GetRunnerLocalPosition(i);
            runnerParents.GetChild(i).localPosition = positionChild;
        }
    }

    //đặt vị trí nhân vật
    private Vector3 GetRunnerLocalPosition(int index)
    {
        //https://en.wikipedia.org/wiki/Fermat%27s_spiral
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);
        return new Vector3(x, 0, z);
    }

    public float GetGrowRadius()
    {
        return radius * Mathf.Sqrt(runnerParents.childCount);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;

            case BonusType.Difference:
                RemoveRunners(bonusAmount);
                break;

            case BonusType.Product:
                int runnerNumber = runnerParents.childCount * bonusAmount - runnerParents.childCount;
                AddRunners(runnerNumber);
                break;

            case BonusType.Division:
                int runnerToRemove = runnerParents.childCount - runnerParents.childCount / bonusAmount;
                RemoveRunners(runnerToRemove);
                break;
        }
    }

    private void AddRunners(int bonusAmount)
    {
        for (int i = 0; i < bonusAmount; i++)
        {
            GameObject targetObject = Instantiate(runnerPrefabs, runnerParents);
            targetObject.transform.localScale = Vector3.zero;
            // Tạo hiệu ứng scale từ 0 đến 1 trong 2 giây
            targetObject.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 2f).SetEase(Ease.OutBounce);

        }


        playerAnimator.Run();
        PlaceRunners();
    }

    private void RemoveRunners(int bonusAmount)
    {
        if (bonusAmount > runnerParents.childCount)
        {
            bonusAmount = runnerParents.childCount;
        }
        int runnerAmount = runnerParents.childCount;

        for (int i = runnerAmount - 1; i >= runnerAmount - bonusAmount; i--)
        {
            Transform runnerDestroy = runnerParents.GetChild(i);
            runnerDestroy.gameObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => OnScaleComplete(runnerDestroy));

        }
        PlaceRunners();
    }
    void OnScaleComplete(Transform runnerDestroy)
    {
        runnerDestroy.SetParent(null);
        Destroy(runnerDestroy.gameObject);
    }
}