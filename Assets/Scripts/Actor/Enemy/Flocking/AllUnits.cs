using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnits : MonoBehaviour
{
    public float createPosOffset;

    public static int unitsCount;

    public GameObject[] units;
    public GameObject[] unitPrefab;
    public int numUnits = 10;
    public Vector3 range = new Vector3(5, 5, 5);

    public bool seekGoal = true;
    public bool obedient = true;
    public bool willful = false;

    [Range(0, 200)]
    public int neighbourDistance = 50;

    [Range(0, 2)]
    public float maxforce = 0.5f;

    [Range(0, 5)]
    public float maxvelocity = 2.0f;

    public Transform goal;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(this.transform.position, range * 2);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, 0.2f);
    }

    private void Start()
    {
        if (goal == null)
            goal = this.transform;
        //units = new GameObject[numUnits];
        //for (int i = 0; i < numUnits; i++)
        //{
        //    Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x),
        //        Random.Range(-range.y, range.y),
        //        Random.Range(0, 0));
        //    units[i] = Instantiate(unitPrefab[0], this.transform.position + unitPos, Quaternion.identity) as GameObject;
        //    units[i].GetComponent<Unit>().manager = this.gameObject;
        //}
    }
    public void GenerateUnit(int unitPrefabIndex)
    {
        GameObject unit = new GameObject();
        Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(0, 0));
        unit = Instantiate(unitPrefab[unitPrefabIndex], this.transform.position + unitPos, Quaternion.identity) as GameObject;
        unit.GetComponent<Unit>().manager = this.gameObject;
    }
    public void GenerateUnit(int unitPrefabIndex, Vector3 randomRange)
    {
        GameObject unit = new GameObject();
        Vector3 unitPos = new Vector3(Random.Range(-randomRange.x, randomRange.x),
                Random.Range(-randomRange.y, randomRange.y),
                Random.Range(0, 0));
        unit = Instantiate(unitPrefab[unitPrefabIndex], this.transform.position + unitPos, Quaternion.identity) as GameObject;
        unit.GetComponent<Unit>().manager = this.gameObject;
    }
    public void GenerateUnits(int unitPrefabIndex, int count)
    {
        GameObject[] units = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x),
               Random.Range(-range.y, range.y),
               Random.Range(0, 0));
            units[i] = Instantiate(unitPrefab[unitPrefabIndex], this.transform.position + unitPos, Quaternion.identity) as GameObject;
            units[i].GetComponent<Unit>().manager = this.gameObject;
        }
       
    }
    public void GenerateUnits(GameObject prefab, int genID, int count)
    {
        GameObject[] units = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            float randX;
            float randY;
            randX = Random.Range(-range.x, range.x);
            randY = Random.Range(-range.y, range.y);
            if (randX > 0) randX += createPosOffset;
            else if (randX <= 0) randX -= createPosOffset;
            if (randY > 0) randY += createPosOffset;
            else if (randY <= 0) randY -= createPosOffset;
            Vector3 unitPos = new Vector3(randX,
              randY,
               Random.Range(0, 0));

            //units[i] = Instantiate(prefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
            units[i] = PoolManager.SpawnObject(prefab, goal.position + unitPos, Quaternion.identity, false) as GameObject;
            units[i].GetComponent<NormalEnemy>().genID = genID;
            units[i].GetComponent<Unit>().manager = this.gameObject;
        }
    }
    public void GenerateUnits(GameObject prefab, Vector3 randomRange, int count)
    {
        GameObject[] units = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-randomRange.x, randomRange.x),
               Random.Range(-randomRange.y, randomRange.y),
               Random.Range(0, 0));
            units[i] = Instantiate(prefab, this.transform.position + unitPos, Quaternion.identity) as GameObject;
            units[i].GetComponent<Unit>().manager = this.gameObject;
        }
    }
    public void GenerateUnits(int unitPrefabIndex, Vector3 randomRange , int count)
    {
        GameObject[] units = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-randomRange.x, randomRange.x),
               Random.Range(-randomRange.y, randomRange.y),
               Random.Range(0, 0));
            units[i] = Instantiate(unitPrefab[unitPrefabIndex], this.transform.position + unitPos, Quaternion.identity) as GameObject;
            units[i].GetComponent<Unit>().manager = this.gameObject;
        }
    }
}
