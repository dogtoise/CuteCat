using UnityEngine;
class CycleMagic : IAttackSkill
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

    public void Initialize(Actor owner)
    {
        level = 0;
        this.owner = owner;
        this.damage = 2.5f;
        this.speed = 4;

        projectilePrefab = Resources.Load("Prefabs/AttackSkills/CycleMagic") as GameObject;
    }

    bool isStart = false;
    public void Attack()
    {
        if (isStart == true) return;
        isStart = true;
        GameObject proj = PoolManager.SpawnObject(projectilePrefab, false);

        float tempDamage = damage + (damage * (0.3f)) * (level - 1) ;

        proj.GetComponent<CycleElement>().Initialize(owner, damage);
       // proj.GetComponent<FallProjectile>
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

        }
    }

    public int GetLevel()
    {
        return level;
    }
}
