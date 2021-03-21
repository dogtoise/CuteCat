using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public struct HeartGenDesc
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    [Header("생성 시작시간")]
    public int generateStartTime;

    [SerializeField]
    [Header("생성 반복주기")]
    public int generateDelay;

    [SerializeField]
    [Range(1, 100)]
    [Header("한번에 생성할 개수")]
    public int generateCount;

    [SerializeField]
    [Range(0, 100)]
    [Header("생성 확률")]
    public int per;

    [SerializeField]
    public float elapsedTime;

    [HideInInspector]
    public bool isStarted;

}
public class HeartGenerator: MonoBehaviour
{
    // 1분마다 20퍼 
    // 첫 1분에는 무조건 

    [SerializeField]
    public HeartGenDesc[] itemGenDescs;

    private bool isStarted = false;

    public float CreateOffset = 10;

    Player player;
    private void Start()
    {
        for (int i = 0; i < itemGenDescs.Length; i++)
        {
            itemGenDescs[i].elapsedTime = 0;
            itemGenDescs[i].isStarted = false;
        }
        player = FindObjectOfType<Player>();
        isStarted = true;
    }

    public void Update()
    {
        if (!isStarted) return;

        for (int i = 0; i < itemGenDescs.Length; i++)
        {
            itemGenDescs[i].elapsedTime += Time.deltaTime;
            if (itemGenDescs[i].isStarted == false)
            {
                if (itemGenDescs[i].generateStartTime < itemGenDescs[i].elapsedTime)
                {
                    // 첫 생성
                    itemGenDescs[i].isStarted = true;
                    itemGenDescs[i].elapsedTime = 0;

                    GameObject obj = PoolManager.SpawnObject(itemGenDescs[i].prefab, false);

                    Vector3 dist;
                    float randX = UnityEngine.Random.Range(-2f, 2f);
                    float randY = UnityEngine.Random.Range(-2f, 2f);
                    dist = new Vector3(player.transform.position.x + (randX * CreateOffset), player.transform.position.y + randY * CreateOffset, player.transform.position.z);
                    obj.transform.position = dist;

                    itemGenDescs[i].elapsedTime = 0;
                }
            }
            else
            {
                if (itemGenDescs[i].generateDelay < itemGenDescs[i].elapsedTime)
                {
                    float rand = UnityEngine.Random.Range(0, 100);
                    if (rand < itemGenDescs[i].per)
                    {
                        GameObject obj = PoolManager.SpawnObject(itemGenDescs[i].prefab, false);
                        Vector3 dist;
                        float randX = UnityEngine.Random.Range(-2f, 2f);
                        float randY = UnityEngine.Random.Range(-2f, 2f);
                        dist = new Vector3(player.transform.position.x + (randX * CreateOffset), player.transform.position.y + randY * CreateOffset, player.transform.position.z);
                        obj.transform.position = dist;
                    }

                    itemGenDescs[i].elapsedTime = 0;
                }
            }
        }
    }

}
