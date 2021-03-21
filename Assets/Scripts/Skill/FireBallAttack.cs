using UnityEngine;

class FireBallAttack : IAttackSkill
{
    public GameObject projectilPrefab;

    public WideProjectile projectile;

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
        this.owner = owner;
        this.damage = 10;
        this.speed = 4;
        projectilPrefab = Resources.Load("Prefabs/AttackSkills/FireBall") as GameObject;
    }
    public void Attack(Vector3 attackPos)
    {
        GameObject proj = PoolManager.SpawnObject(projectilPrefab, false);
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
        float tempDamage = damage + (damage * (0.3f)) * (level - 1);
        proj.GetComponent<WideProjectile>().Initialize(owner, attackPos, tempDamage, speed);
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
