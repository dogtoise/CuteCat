using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct UnitGenDesc
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    [Header("Gen ID")]
    public int genID;
    [SerializeField]
    [Header("생성 시작 시간")]
    public int generateStartTime;
    [SerializeField]
    [Header("생성 반복 주기")]
    public int generateDelay;
    [SerializeField]
    [Range(1, 100)]
    [Header("한번에 생성할 개수")]
    public int generateCount;

    [SerializeField]
    [Header("생성 최대값")]
    public int generateMaxCount;

    [SerializeField]
    public float elapsedTime;

    [HideInInspector]
    public bool isStarted;

    [HideInInspector]
    public int generatedCount;
}

class UnitGenerator : SingletonMonoBehaviour<UnitGenerator>
{
    [SerializeField]
    public UnitGenDesc[] unitGenDescs;
    private AllUnits unitManager;
    private bool isStarted = false;

    private void Start()
    {

        unitManager = FindObjectOfType<AllUnits>();
        if (unitManager == null)
            Debug.LogWarning("AllUnits 컴포넌트를 가진 오브젝트가 없습니다.");
        else
            isStarted = true;

        if (isStarted)
        {
            for (int i = 0; i < unitGenDescs.Length; i++)
            {
                unitGenDescs[i].elapsedTime = 0;
                unitGenDescs[i].isStarted = false;
            }
        }
    }

    public void DecreaseGenCount(int id)
    {
        for (int i = 0; i < unitGenDescs.Length; i++)
        {
            if (unitGenDescs[i].genID == id)
                unitGenDescs[i].generatedCount--;
        }
    }
    private float elapsedTime = 0;
    private void Update()
    {
        if (isStarted == false) return;
        for (int i = 0; i < unitGenDescs.Length; i++)
        {
            if (unitGenDescs[i].generateMaxCount <= unitGenDescs[i].generatedCount)
                continue;
            unitGenDescs[i].elapsedTime += Time.deltaTime;
            if (unitGenDescs[i].isStarted == false)
            {
                if (unitGenDescs[i].generateStartTime < unitGenDescs[i].elapsedTime)
                {
                    // 첫 생성
                    unitGenDescs[i].isStarted = true;
                    unitGenDescs[i].elapsedTime = 0;
                    unitGenDescs[i].generatedCount += unitGenDescs[i].generateCount;
                    unitManager.GenerateUnits(unitGenDescs[i].prefab, unitGenDescs[i].genID, unitGenDescs[i].generateCount);
                }
            }
            else
            {
                if (unitGenDescs[i].generateDelay < unitGenDescs[i].elapsedTime)
                {
                    unitGenDescs[i].elapsedTime = 0;
                    unitGenDescs[i].generatedCount += unitGenDescs[i].generateCount;
                    unitManager.GenerateUnits(unitGenDescs[i].prefab, unitGenDescs[i].genID, unitGenDescs[i].generateCount);
                }
            }
        }
    }
}
