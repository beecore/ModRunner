using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyParents;
    [SerializeField] private int amout;
    [SerializeField] private float radius;
    [SerializeField] private float angle;

    private void Start()
    {
        GenerateEnemies();
    }
    void GenerateEnemies()
    {
        for(int i = 0; i < amout; i++)
        {
            Vector3 enemyLocalPosition = GetEnemieLocalPosition(i);
            Vector3 enemyWorldPosition = transform.TransformPoint(enemyLocalPosition);
            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemyParents);
        }
    }
    //đặt vị trí nhân vật
    private Vector3 GetEnemieLocalPosition(int index)
    {
        //https://en.wikipedia.org/wiki/Fermat%27s_spiral
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);
        return new Vector3(x, 0, z);

    }
}
