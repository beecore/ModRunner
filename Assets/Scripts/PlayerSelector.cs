using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField]
    private Transform runnerParents;
    [SerializeField]
    private RunnerSelect runnerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        ShopManager.onSkinSelected += SelectSkin;
    }
    private void OnDestroy()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("chon");
            SelectSkin(Random.Range(0, 9));
        }
    }
    public void SelectSkin(int skinIndex)
    {
        for(int i = 0; i < runnerParents.childCount; i++)
        {
            runnerParents.GetChild(i).GetComponent<RunnerSelect>().SelectRunner(skinIndex);
        }
        runnerPrefabs.SelectRunner(skinIndex);
    }
}
