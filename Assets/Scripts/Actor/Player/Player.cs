using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AttackType
{
    DefaulAttack = 0, // 
    FireBall = 1, // 임시
    MagicBall,
    Cycle,
    MAX,
}
[Serializable]
public struct AttackSkill
{
    [HideInInspector]
    public string name;

    [SerializeField]
    public IAttackSkill skill;

    [SerializeField]
    [Header("활성화 여부")]
    public bool isActive;
}



public class Player : Actor
{
    public const int levelMax = 30;
    public int LevelMax
    {
        get { return levelMax; }
    }
    public float[] expRequired = new float[levelMax];


    [SerializeField]
    public AttackSkill[] attackSkills;

    public Weapon weapon;
    public Transform firePos;

    SpriteRenderer spriteRenderer;

    // level
    public int level;
    public float exp;
    ExpManager expMgr;


    public SpriteRenderer hpFill;


    public Virtualjoystick3 jsMove;
    private Animator animator;

    public List<ItemType> equipedItems = new List<ItemType>();
    // 기본공격
    protected override  void Start()
    {
        equipedItems = new List<ItemType>();
        equipedItems.Clear();
        expRequired = new float[levelMax];
        expRequired[0] = 200;
        expRequired[1] = 200;
        for (int i = 2; i < levelMax; i++)
        {
            expRequired[i] = 500 * (i + 1);
        }

        animator = GetComponent<Animator>();

        level = 0;
        exp = 0;
        expMgr = FindObjectOfType<ExpManager>();
        firePos = weapon.firePos;


        attackSkills = new AttackSkill[(int)AttackType.MAX];
        attackSkills[(int)AttackType.DefaulAttack] = new AttackSkill() { name = "NormalAttack", skill = new NormalAttack(), isActive = true };
        attackSkills[(int)AttackType.FireBall] = new AttackSkill() { name = "FireBall", skill = new FireBallAttack(), isActive = false };
        attackSkills[(int)AttackType.MagicBall] = new AttackSkill() { name = "MagicBall", skill = new MagicBallAttack(), isActive = false };
        attackSkills[(int)AttackType.Cycle] = new AttackSkill() { name = "Cycle", skill = new CycleMagic(), isActive = false };
        attackSkills[(int)AttackType.DefaulAttack].skill.Initialize(this);
        attackSkills[(int)AttackType.FireBall].skill.Initialize(this);
        attackSkills[(int)AttackType.MagicBall].skill.Initialize(this);
        attackSkills[(int)AttackType.Cycle].skill.Initialize(this);

        spriteRenderer = GetComponent<SpriteRenderer>();
        jsMove = FindObjectOfType<Virtualjoystick3>();

        spd = 2.5f;
        //NormalAttack normalAttack = new NormalAttack();
        //normalAttack.Initialize(this, )
        //attackSkills.Add(NormalAttack );
    }

    public Vector3 movementDir;
    public Vector3 preDir;
    public float spd = 0;
    float healElap1 = 0;
    float healElap2 = 0;
    float healElap3 = 0;
    private void Update()
    {
        float empSpd = spd;
        if (hp <= 0)
            hpFill.material.SetFloat("_Progress", 0);
        else
            hpFill.material.SetFloat("_Progress", hp / maxHP);
        for (int i = 0; i < equipedItems.Count; i++)
        {
            switch(equipedItems[i])
            {
                case ItemType.Mask1:
                    empSpd = empSpd + empSpd * 0.2f;
                    break;
                case ItemType.Mask2:
                    empSpd = empSpd + empSpd * 0.5f;
                    break;
                case ItemType.Mask3:
                    empSpd = empSpd + empSpd * 1f;
                    break;
                case ItemType.Heart1:
                    healElap1 += Time.deltaTime;
                    if (healElap1 >= 1 && maxHP > hp * 0.005f)
                    {
                        hp += hp * 0.005f;
                        healElap1 = 0;
                    }
                    break;
                case ItemType.Heart2:
                    healElap2 += Time.deltaTime;
                    if (healElap2 >= 1 && maxHP > hp * 0.01f)
                    {
                        hp += hp * 0.01f;
                        healElap2 = 0;
                    }
                    break;
                case ItemType.Heart3:
                    healElap2 += Time.deltaTime;
                    if (healElap2 >= 1 && maxHP > hp * 0.03f)
                    {
                        hp += hp * 0.03f;
                        healElap3 = 0;
                    }
                    break;
            }
        }


        movementDir = new Vector2(jsMove.Direction.x, jsMove.Direction.z);
        // move anim
        if (Vector2.Distance(movementDir, Vector2.zero) > 0.01f)
        {
            if (Mathf.Abs(movementDir.x) > Mathf.Abs(movementDir.y))
            {
                if (movementDir.x > 0) // 오른쪽
                {
                    preDir = movementDir;
                    animator.SetBool("Idle", false);
                    animator.SetBool("DownMove", false);
                    animator.SetBool("LeftMove", false);
                    animator.SetBool("RightMove", true);
                    animator.SetBool("UpMove", false);
                }
                else if (movementDir.x <= 0) // 왼쪽
                {
                    preDir = movementDir;
                    animator.SetBool("Idle", false);
                    animator.SetBool("DownMove", false);
                    animator.SetBool("LeftMove", true);
                    animator.SetBool("RightMove", false);
                    animator.SetBool("UpMove", false);
                }

            }
            else
            {
                if (movementDir.y > 0) // 위쪽
                {
                    preDir = movementDir;
                    animator.SetBool("Idle", false);
                    animator.SetBool("DownMove", false);
                    animator.SetBool("LeftMove", false);
                    animator.SetBool("RightMove", false);
                    animator.SetBool("UpMove", true);
                }
                else if (movementDir.y <= 0)
                {
                    preDir = movementDir;
                    animator.SetBool("Idle", false);
                    animator.SetBool("DownMove", true);
                    animator.SetBool("LeftMove", false);
                    animator.SetBool("RightMove", false);
                    animator.SetBool("UpMove", false);
                }
            }
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("DownMove", false);
            animator.SetBool("LeftMove", false);
            animator.SetBool("RightMove", false);
            animator.SetBool("UpMove", false);
        }
            
        transform.Translate(movementDir.normalized * empSpd * Time.deltaTime);

        #region normalAttack
        // 기본공격
        if (attackSkills[(int)AttackType.DefaulAttack].isActive)
        {
            NormalAttack normalAttack = attackSkills[(int)AttackType.DefaulAttack].skill as NormalAttack;
            normalAttack.elapsedTime += Time.deltaTime;
            if (normalAttack.elapsedTime > normalAttack.attackDelay)
            {

                //Actor target=    Actor.FindActor((Actor actor) => 
                //{
                //    if (Vector2.Distance(actor.transform.position, transform.position) < normalAttack.attackRange)
                //    {
                //        return true;
                //    }
                //    return false;
                //});

                Actor target = null;
                float bestDist = 100;
                foreach (var act in allActors)
                {
                    if (act.Value == this || act.Value.isActiveAndEnabled ==false) continue;
                    float dist = Vector2.Distance(transform.position, act.Value.transform.position);
                    if (dist < bestDist)
                    {
                        target = act.Value;
                        bestDist = dist;
                    }
                }
                if (target != null && Vector2.Distance(target.transform.position, transform.position) < normalAttack.attackRange)
                {
                    normalAttack.Attack(target, firePos.position);
                    normalAttack.elapsedTime = 0;
                }
            }
        }
        #endregion
        if (attackSkills[(int)AttackType.FireBall].isActive)
        {
            FireBallAttack fireBallAttack = attackSkills[(int)AttackType.FireBall].skill as FireBallAttack;
            fireBallAttack.elapsedTime += Time.deltaTime;
            if (fireBallAttack.elapsedTime > fireBallAttack.AttackDelay)
            {
                fireBallAttack.Attack(firePos.position);
                fireBallAttack.elapsedTime = 0;
            }
        }
        if (attackSkills[(int)AttackType.MagicBall].isActive)
        {
            MagicBallAttack magicBallAttack = attackSkills[(int)AttackType.MagicBall].skill as MagicBallAttack;
            magicBallAttack.elapsedTime += Time.deltaTime;
            if (magicBallAttack.elapsedTime > magicBallAttack.AttackDelay)
            {

                Actor[] target = new Actor[magicBallAttack.attackCount];
                float bestDist = 100;
                int foundCnt = 0;
                bool isAttacked = false;
                foreach (var act in allActors)
                {
                    if (foundCnt >= magicBallAttack.attackCount) break;
                    if (act.Value == this || act.Value.isActiveAndEnabled == false) continue;
                    float dist = Vector2.Distance(transform.position, act.Value.transform.position);
                    if (dist < magicBallAttack.attackDistance)
                    {
                        target[foundCnt++] = act.Value;
                    }
                }
                for (int i = 0; i < target.Length; i++)
                {
                    if (target[i] != null)
                    {
                        magicBallAttack.Attack(target[i], firePos.position);
                        isAttacked = true;
                    }
                }
                if(isAttacked) 
                    magicBallAttack.elapsedTime = 0;
            }
        }
        if (attackSkills[(int)AttackType.Cycle].isActive)
        {
            CycleMagic cycleMagic = attackSkills[(int)AttackType.Cycle].skill as CycleMagic;
            cycleMagic.Attack();
            //fireFallAttack.elapsedTime += Time.deltaTime;
            //if (fireFallAttack.elapsedTime > fireFallAttack.attackDelay)
            //{
            //    for (int i = 0; i < fireFallAttack.attackCount; i++)
            //    {
            //        int randX = UnityEngine.Random.Range(-2, 2);
            //        int randY = UnityEngine.Random.Range(-2, 2);
            //        Vector3 distPos = new Vector3(transform.position.x + randX, transform.position.y + randY);
            //        fireFallAttack.Attack(distPos);
            //    }
            //    fireFallAttack.elapsedTime = 0;
            //}
        }

    }
    public void FireBallLevelUp()
    {
        if (attackSkills[(int)AttackType.FireBall].isActive == false)
        {
            attackSkills[(int)AttackType.FireBall].isActive = true;
            StageUIManager.Instance.GetSkill((int)AttackType.FireBall);
        }
        attackSkills[(int)AttackType.FireBall].isActive = true;
       attackSkills[(int)AttackType.FireBall].skill.LevelUp();
    }
    public void PowerLevelUp()
    {
        (attackSkills[(int)AttackType.DefaulAttack].skill as NormalAttack).damage += 5;
    }
    public void MoveSpeedLevelUp()
    {
        spd += 0.15f;
    }

    public void MaxHpLevelUp()
    {
        maxHP += 50;
    }
    public void AttackSpdLevelUp()
    {
        (attackSkills[(int)AttackType.DefaulAttack].skill as NormalAttack).attackDelay -= 0.05f;
    }
    public void CycleMagicLevelUp()
    {
        if (attackSkills[(int)AttackType.Cycle].isActive == false)
        {
            attackSkills[(int)AttackType.Cycle].isActive = true;
            StageUIManager.Instance.GetSkill((int)AttackType.Cycle);
        }
        attackSkills[(int)AttackType.Cycle].skill.LevelUp();
        //if (attackSkills[(int)AttackType.FireFall].isActive == false)
        //{
        //    attackSkills[(int)AttackType.FireFall].isActive = true;
        //    StageUIManager.Instance.GetSkill((int)AttackType.FireFall);
        //}
        //attackSkills[(int)AttackType.FireFall].skill.LevelUp();
    }
    public void MagicBallLevelUp()
    {
        if (attackSkills[(int)AttackType.MagicBall].isActive == false)
        {
            attackSkills[(int)AttackType.MagicBall].isActive = true;
            StageUIManager.Instance.GetSkill((int)AttackType.MagicBall);
        }
        attackSkills[(int)AttackType.MagicBall].skill.LevelUp();
    }

    public void TakeExp(float exp)
    {
        if (levelMax < level)
        {
            expMgr.IncreaseExp(expRequired[level], expRequired[level]);
            return;
        }
        this.exp += exp;
        expMgr.IncreaseExp(this.exp, expRequired[level]);
    }
    public void LevelUp()
    {
        if (levelMax < level) return;
        else
        {
            if (levelMax - 1 == level)
                expMgr.IncreaseExp(expRequired[level], expRequired[level]);
            else
            {
                this.exp = 0;
                expMgr.IncreaseExp(this.exp, expRequired[++level]);
                StageUIManager.Instance.ShowSkillChoicePanel();
            }
        }
       
    }
    public override void OnHit(float damage)
    {
        if (unHitTime) return;
        base.OnHit(damage);
        //hpFill.ma.fillAmount = hp / maxHP;
        if (hp <= 0)
            hpFill.material.SetFloat("_Progress", 0);
        else
            hpFill.material.SetFloat("_Progress", hp / maxHP);
        if (hp <= 0)
            OnDie();
      
        unHitTime = true;
    }
    public override void OnHitReaction()
    {
      // base.OnHitReaction();
        StartCoroutine(UnHitTime());
    }
    protected override void OnDie()
    {
        base.OnDie();
        Debug.Log("OnDie?");
        GameManager.Instance.OnGameOver();
    }

    bool unHitTime = false;
    IEnumerator UnHitTime()
    {
        int countTime = 0;
        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        unHitTime = false;

        yield return null;
    }
}
