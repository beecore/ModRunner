using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowCounter : MonoBehaviour
{
    [SerializeField] private Transform runnersParent;
    [SerializeField] private TextMeshPro crowCounterText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crowCounterText.text = runnersParent.childCount.ToString();
        if (runnersParent.childCount <= 0)
            gameObject.SetActive(false);
    }
}
