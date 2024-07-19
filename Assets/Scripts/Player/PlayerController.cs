using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [Header("Elements")]
    [SerializeField]
    private CrowdSystem crowdSystem;

    [SerializeField]
    private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float roadWidth;

    [Header("Control")]
    [SerializeField]
    private float slideSpeed;

    private Vector3 clickedScreenPostion;
    private Vector3 clickedPlayerPostion;
    private bool canMove;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangeCallBack;
    }

    private void GameStateChangeCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
        {
            StartMoving();
        }
        else if (gameState == GameManager.GameState.GameOver)
        {
            StopMoving();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            StopMoving();
        }
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallBack;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canMove)
        {
            MoveToWard();
            ManageControl();
        }
    }

    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
    }

    /// <summary>
    /// Hàm di chuyển về phía trước
    /// </summary>
    private void MoveToWard()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }

    private void ManageControl()
    {
#if UNITY_EDITOR
        //Debug.Log("Unity Editor");
        if (Input.GetMouseButtonDown(0))
        {
            //lưu vi trí  chuột
            clickedScreenPostion = Input.mousePosition;
            //lưu vị trí player
            clickedPlayerPostion = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            //tính vị trí chuột hiện tại so với vị trí click ban đầu
            float xScreenDifference = Input.mousePosition.x - clickedScreenPostion.x;
            xScreenDifference = xScreenDifference * slideSpeed;
            Vector3 position = transform.position;
            position.x = clickedPlayerPostion.x + xScreenDifference;
            //Giới hạn trục x
            position.x = Mathf.Clamp(position.x, (-roadWidth / 2) + crowdSystem.GetGrowRadius(), (roadWidth / 2) - crowdSystem.GetGrowRadius());
            transform.position = position;
        }
#endif

#if UNITY_ANDROID
        //  Debug.Log("ANDROID");
#endif
    }
}