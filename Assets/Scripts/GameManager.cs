using UnityEngine;

class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool isPause = false; 
    public void Start()
    {
        GlobalVars.TotalKill = 0;
        GlobalVars.TotalPlayTime_Sec = 0;
        GlobalVars.TotalPlayTime_Min = 0;
        PoolManager.Initialize();
        Actor.allActors.Clear();
    }

    private void Update()
    {
        GlobalVars.TotalPlayTime_Sec += GetDeltaTime();
    }


    public float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    public void OnGameOver()
    {
        StageUIManager.Instance.ShowGameOverPane();
    }

}

