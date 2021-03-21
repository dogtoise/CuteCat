using UnityEngine;

public enum NormalAttackProperty
{
    UpSpd = 0x00000001, // 0001
    LowDelay = 0x00000002, // 0010
    // 0x00000004, 0x00000008
}

class NormalAttack : IAttackSkill
{
    public GameObject projectilPrefab;

    public NormalProjectile projectile;
    public int level;

    public Actor owner;

    public float damage = 10;
    public float speed = 8;

    public float attackDelay = 0.37f;
    public float attackRange = 5;
    public float elapsedTime = 0;

    public int maxLevel;
    public void Initialize(Actor owner)
    {
        this.owner = owner;
        projectilPrefab = Resources.Load("Prefabs/AttackSkills/DefaultAttack") as GameObject;
    }

    public void Attack(Actor target, Vector3 attackPos)
    {
        GameObject proj =  PoolManager.SpawnObject(projectilPrefab, false);
        proj.GetComponent<NormalProjectile>().Initialize(owner, target, attackPos, damage, speed);
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
