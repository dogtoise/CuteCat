using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Actor : MonoBehaviour
{
    [SerializeField]
    public static Dictionary<Id64, Actor> allActors = new Dictionary<Id64, Actor>();

    public static Actor FindActor(Func<Actor, bool> condition)
    {
        foreach(var actor in allActors)
        {
            if(condition(actor.Value))
            {
                return actor.Value;
            }
        }
        return null;
    }
    public static Actor Find<T>()
    {
        foreach (var actor in allActors)
        {
            if (actor.Value.GetComponent<T>() != null)
            {
                return actor.Value;
            }
        }
        return null;
    }

    private Id64 uniqueID;
    protected virtual void Start()
    {
        uniqueID = new Id64(Uid.GetNextId());
        allActors.Add(uniqueID, this);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    private SpriteRenderer spriteRenderer;
    private bool isHit; 
    public float hp;
    public float maxHP;
    public void IncreaseHP(float hp)
    {
        if (this.hp + hp > maxHP)
        {
            this.hp = maxHP;
        }
        else
            this.hp += hp;
    }
    public virtual void OnHit(float damage)
    {
        Debug.Log("Hit. Damage : " + damage);
       
        if (hp <= damage) hp = 0;
        else
            hp -= damage;
        Debug.Log("after hp : " + hp); 
        if (hp <= 0)
        {
            // 임시
            OnDie();
        }
        else
            OnHitReaction();
    }
    protected virtual void OnDie() { }

    public virtual void OnHitReaction()
    {
        StartCoroutine(SimpleHitReaction());
    }


    IEnumerator SimpleHitReaction()
    {
        spriteRenderer.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = new Color32(255, 255, 255, 255);
    }

    private void OnDestroy()
    {
      
    }
}
