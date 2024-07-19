using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Transform playerParents;



    public void Run()
    {
        for (int i = 0; i < playerParents.childCount; i++)
        {
            Transform runner = playerParents.GetChild(i);
            Animator anim = runner.GetComponent<Runner>().GetAnimator();
            anim.Play("Run");
        }
    }

    public void Idle()
    {
        for (int i = 0; i < playerParents.childCount; i++)
        {
            Transform runner = playerParents.GetChild(i);
            Animator anim = runner.GetComponent<Runner>().GetAnimator();
            anim.Play("Idle");
        }
    }
}