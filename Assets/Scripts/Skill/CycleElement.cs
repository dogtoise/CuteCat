using System.Collections.Generic;
using UnityEngine;

class CycleElement : MonoBehaviour
{
    private Actor owner;
    private float damage;
    private float speed;
    bool isInit = false;


    Vector3 direction;
    Vector3 startPos;
    Vector3 dirPos;
    public float elapsed; // 2초
    public float maxElapsed = 2; // 2초

    public void Initialize(Actor owner, float damage)
    {
        transform.position = owner.transform.position;
        this.damage = damage;
        elapsed = 0;
        maxElapsed = 1;
        this.owner = owner;
        this.damage = damage;
        isInit = true;
    }
    Stack<Actor> hitedList = new Stack<Actor>();
    private void Update()
    {
        transform.position = owner.transform.position;
        elapsed += Time.deltaTime;
        if (elapsed > maxElapsed)
        {
            foreach (var act in Actor.allActors)
            {
                if (act.Value.gameObject.CompareTag("Player")) continue;
                bool bHitted = false;
                foreach (var hit in hitedList)
                {
                    if (act.Value == hit)
                    {
                        bHitted = true;
                        break;
                    }
                }
                if (bHitted == true) continue;
                if (Vector2.Distance(act.Value.transform.position, transform.position) < 1)
                {
                    act.Value.OnHit(damage);
                    hitedList.Push(act.Value);
                    return;
                }
            }
            elapsed = 0;
            hitedList.Clear();
        }
    }
}

