using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowCounter : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;
    [SerializeField] private TextMeshPro crowCounterText;

    // Update is called once per frame
    void Update()
    {
        crowCounterText.text = runnersParent.childCount.ToString();
        if (runnersParent.childCount <= 0)
            gameObject.SetActive(false);
    }
}
