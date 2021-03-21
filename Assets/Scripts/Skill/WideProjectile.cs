using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class WideProjectile : MonoBehaviour
{

    private Actor owner;
   // private Actor target;
    private float damage;
    private float speed;
    bool isInit = false;

    Vector3 direction;
    Vector3 startPos;
    public void Initialize(Actor owner,Vector3 firePos, float damage, float speed)
    {
        hitedList.Clear();
        transform.position = firePos;

        this.owner = owner;
        //this.target = target;
        this.damage = damage;
        this.speed = speed;
        startPos = transform.position;
        direction = (owner as Player).preDir.normalized;
        //direction = target.transform.position - transform.position;
        //direction = direction.normalized;
        isInit = true;
    }

    Stack<Actor> hitedList = new Stack<Actor>();
    private void Update()
    {
        if (isInit)
        {
           transform.Translate((-direction) * speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startPos) > 10)
            {
                PoolManager.ReleaseObject(this.gameObject);

                return;
            }
            foreach (var act in Actor.allActors)
            {
                if (act.Value.gameObject.CompareTag("Player")) continue;
                bool bHitted = false ;
                foreach (var hit in hitedList)
                {
                    if (act.Value == hit)
                    {
                        bHitted = true;
                        break;
                    }
                }
                if (bHitted == true) continue;
                if (Vector2.Distance(act.Value.transform.position, transform.position) < 0.7f)
                {
                    act.Value.OnHit(damage);
                    hitedList.Push(act.Value);
                    return;
                }
            }
        }

    }
}
