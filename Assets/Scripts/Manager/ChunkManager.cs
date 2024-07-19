using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : SingletonMonoBehaviour<ChunkManager>
{
    //[SerializeField]  private Chuck[] chunkPrefabs;
    //[SerializeField]  private Chuck[] levelChunks;
    [SerializeField]
    private LevelData[] levels;
    [SerializeField]  private GameObject finishLine;
    protected override void Awake()
    {
        base.Awake();

    }
    void Start()
    {
        GenerateLevel();
        
    }
    private void GenerateLevel()
    {
        int currentLevel = GetLevel();
        currentLevel = currentLevel % levels.Length;
        LevelData level = levels[currentLevel];
        CreateOrderedLevel(level.chunks);

    }
    private void CreateOrderedLevel(Chuck[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chuck chunkCreate = levelChunks[i];

            if (i > 0)
            {
                chunkPosition.z += chunkCreate.GetLength() / 2;
            }

            Chuck chunkInstantiate = Instantiate(chunkCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstantiate.GetLength() / 2;
            if (i == levelChunks.Length - 1)
                finishLine = chunkInstantiate.gameObject;
        }
       
    }
    //private void CreateRandomLevel()
    //{
    //    Vector3 chunkPosition = Vector3.zero;
      
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Chuck chunkCreate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

    //        if (i > 0)
    //        {
    //            chunkPosition.z += chunkCreate.GetLength() / 2;
    //        }

    //        Chuck chunkInstantiate = Instantiate(chunkCreate, chunkPosition, Quaternion.identity, transform);
    //        chunkPosition.z += chunkInstantiate.GetLength() / 2;
    //    }
    //}
    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level",0);
    }

}
