using System;
using UnityEngine;
class NormalEnemy : Actor
{
    Player player;
    int exp = 10;
    UnitGenerator unitGen;

    public int damage = 10;
    public int genID;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindObjectOfType<Player>();
        unitGen = FindObjectOfType<UnitGenerator>();
    }
    protected override void OnDie()
    {
        base.OnDie();
        player.TakeExp(exp);
        GlobalVars.TotalKill++;
        unitGen.DecreaseGenCount(genID);
        PoolManager.ReleaseObject(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().OnHit(damage);
        }
    }

}
