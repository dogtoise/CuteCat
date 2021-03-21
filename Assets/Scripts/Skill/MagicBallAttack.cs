using System;
using UnityEngine;

class MagicBallAttack : IAttackSkill
{
    public GameObject projectilePrefab;

    private int level = 0;
    public Actor owner;

    public float damage;
    public float range;
    public float speed;

    private float attackDelay = 10;
    public float AttackDelay
    {
        get
        {
            return attackDelay - (attackDelay * 0.1f) * (level - 1);
        }
    }
    public float attackDistance = 10;
    public float elapsedTime = 0;

    private int maxLevel = 5;

    public int attackCount = 3;
    public void Initialize(Actor owner)
    {
        this.owner = owner;
        this.damage = 10;
        this.speed = 2;

        projectilePrefab = Resources.Load("Prefabs/AttackSkills/MagicBall") as GameObject;
        Debug.Log("Projectile prefab : " + projectilePrefab);
    }

    public void Attack(Actor target, Vector3 attackPos)
    {
            GameObject proj = PoolManager.SpawnObject(projectilePrefab, false);
        float tempDamage = damage + (damage * (0.3f)) * (level - 1);
        proj.GetComponent<MultiWideProjectile>().Initialize(owner, target, attackPos, tempDamage, speed);
        
    }

    public bool IsMaxLevel()
    {
        return level == maxLevel;
    }

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;

            attackCount++;
        }
    }
    public int GetLevel()
    {
        return level;
    }
}
