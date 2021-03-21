using UnityEngine;

class FireFallAttack : IAttackSkill
{
    public GameObject projectilePrefab;

    private int level = 0;
    public Actor owner;

    public float damage;
    public float range;
    public float speed;

    public float attackDelay = 10;
    public float attackDistance = 10;
    public float elapsedTime = 0;

    private int maxLevel = 5;

    public int attackCount = 6;
    public void Initialize(Actor owner)
    {
        this.owner = owner;
        this.damage = 10;
        this.speed = 4;

        projectilePrefab = Resources.Load("Prefabs/AttackSkills/FireFall") as GameObject;
    }

    public void Attack(Vector3 rndPos)
    {
        GameObject proj = PoolManager.SpawnObject(projectilePrefab, false);
        switch (level)
        {
            case 0:
                break;
            case 1:
                proj.transform.localScale = new Vector3(proj.transform.localScale.x + 0.5f, proj.transform.localScale.y + 0.5f, proj.transform.localScale.z);
                break;
            case 2:
                proj.transform.localScale = new Vector3(proj.transform.localScale.x + 0.5f, proj.transform.localScale.y + 0.5f, proj.transform.localScale.z);
                break;
        }

        proj.GetComponent<FallProjectile>().Initialize(owner, rndPos, damage, speed);

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

