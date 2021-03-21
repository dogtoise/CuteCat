using System.Collections.Generic;
using UnityEngine;

class MultiWideProjectile : MonoBehaviour
{
    private Actor owner;
    private Actor target;
    private float damage;
    private float speed;
    bool isInit = false;


    Vector3 direction;
    Vector3 startPos;
    Vector3 dirPos;
    public float elapsed; // 2초
    public float maxElapsed = 2; // 2초
    public float curveElapse = 0;
    public float curveElapsed = 1;
    public void Initialize(Actor owner, Actor target, Vector3 firePos, float damage, float speed)
    {
        transform.position = firePos;
        elapsed = 0;
        maxElapsed = 2;
        curveElapse = 0;
        curveElapsed = 1;
        this.owner = owner;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        startPos = transform.position;
        float randX = UnityEngine.Random.Range(-2f, 2f);
        float randY = UnityEngine.Random.Range(-2f, 2f);
        dirPos = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);

        direction = dirPos - transform.position;
        direction = direction.normalized;


        isInit = true;
    }
    Stack<Actor> hitedList = new Stack<Actor>();
    private void Update()
    {
        if (isInit)
        {
            elapsed += Time.deltaTime;
            curveElapse += Time.deltaTime;
            if (elapsed > maxElapsed)
            {
                PoolManager.ReleaseObject(this.gameObject);
                return;
            }
            if (curveElapse > curveElapsed)
            {
                float randX = UnityEngine.Random.Range(-2f, 2f);
                float randY = UnityEngine.Random.Range(-2f, 2f);
                dirPos = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);

                direction = dirPos - transform.position;
                direction = direction.normalized;
                curveElapse = 0;
            }
            transform.Translate(direction * speed * Time.deltaTime);
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
                if (Vector2.Distance(act.Value.transform.position, transform.position) < 0.5f)
                {
                    act.Value.OnHit(damage);
                    hitedList.Push(act.Value);
                    return;
                }
            }


            //if (target.gameObject.activeSelf == false)
            //{
            //    transform.Translate(direction * speed * Time.deltaTime);
            //    if (Vector2.Distance(transform.position, startPos) > 10)
            //    {
            //        PoolManager.ReleaseObject(this.gameObject);
            //        return;
            //    }
            //    foreach (var act in Actor.allActors)
            //    {
            //        if (act.Value.gameObject.CompareTag("Player")) continue;
            //        if (Vector2.Distance(act.Value.transform.position, transform.position) < 0.5f)
            //        {
            //            act.Value.OnHit(damage);
            //            PoolManager.ReleaseObject(this.gameObject);
            //            return;
            //        }
            //    }
            //}
            //else
            //{
            //    direction = target.transform.position - transform.position;
            //    direction = direction.normalized;
            //    transform.Translate(direction * speed * Time.deltaTime);

            //    foreach (var act in Actor.allActors)
            //    {
            //        if (act.Value.gameObject.CompareTag("Player")) continue;
            //        if (Vector2.Distance(act.Value.transform.position, transform.position) < 0.5f)
            //        {
            //            act.Value.OnHit(damage);
            //            PoolManager.ReleaseObject(this.gameObject);
            //            return;
            //        }
            //    }
            //}
        }

    }
}
