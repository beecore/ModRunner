using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    enum State {  Idle,Running}
    [Header(" Setting")]
    [SerializeField]
    private float searchRadius;
    [SerializeField]
    private float moveSpeed;
    private State state;
    private Transform targetRunner;
    public static Action onRunnerDie;
    [SerializeField]
    private GameObject effectDie;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }
    public void ManageState()
    {
       
        switch (state)
        {
           case State.Idle:
                //Tìm kiếm mục tiêu trong bán kính
                SearchForTarget();
            break;
            case State.Running:
                //Chạy đến mục tiêu
                RunTowardsTarget();
            break;

        }
    }


    private void SearchForTarget()
    {
        
        Collider[] detectedoColliders = Physics.OverlapSphere(transform.position, searchRadius);
        for(int i=0;i< detectedoColliders.Length; i++)
        {
            if (detectedoColliders[i].TryGetComponent(out Runner runner))
            {
               
                //Nếu nó là target của enemy khác thì bỏ qua tìm tiếp
                if (runner.IsTarget())
                    continue;

                //Nêu chưa là target
                runner.SetTarget();
                targetRunner = runner.transform;
                StartRunningTowardsTarget();


            }
        }
    }
    /// <summary>
    /// Bắt đầu chạy về mục tiêu
    /// </summary>
    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }
    /// <summary>
    /// Hàm enemy chạy về mục tiêu
    /// </summary>
    private void RunTowardsTarget()
    {
        //Nếu không có mục tiêu thì ko làm gì cả
        if (targetRunner == null)
            return;
        //Di chuyển đến mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);
        //Nếu enemy gần hoặc chạm nhân vật thì hủy cả hai
        if (Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            onRunnerDie?.Invoke();
            Instantiate(effectDie, gameObject.transform.position, Quaternion.identity);
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
            

        }
    }

}
