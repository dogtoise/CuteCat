using UnityEngine;
class NormalProjectile : MonoBehaviour
{
    private Actor owner;
    private Actor target;
    private float damage;
    private float speed;
    bool isInit = false;

    public GameObject hitEff;
    Vector3 direction;
    Vector3 startPos;
    public void Initialize(Actor owner, Actor target, Vector3 firePos, float damage, float speed)
    {
        transform.position = firePos;
        hitEff = Resources.Load("Prefabs/HitEffect") as GameObject;
        this.owner = owner;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        startPos = transform.position;
        direction = target.transform.position - transform.position;
        direction = direction.normalized;
        isInit = true;
    }

    private void Update()
    {
        if (isInit)
        {
            if (target.gameObject.activeSelf == false)
            {
                transform.Translate(direction * speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, startPos) > 10)
                {
                    PoolManager.ReleaseObject(this.gameObject);
                    return;
                }
                foreach (var act in Actor.allActors)
                {
                    if (act.Value.gameObject.CompareTag("Player")) continue;
                    if (Vector2.Distance(act.Value.transform.position, transform.position) < 0.5f)
                    {
                        act.Value.OnHit(damage);
                        PoolManager.ReleaseObject(this.gameObject);
                        return;
                    }
                }
            }
            else
            {
                direction = target.transform.position - transform.position;
                direction = direction.normalized;
                transform.Translate(direction * speed * Time.deltaTime);
                //if (Vector2.Distance(transform.position, startPos) > 10)
                //{
                //    PoolManager.ReleaseObject(this.gameObject);
                //    return;
                //}
                foreach (var act in Actor.allActors)
                {
                    if (act.Value.gameObject.CompareTag("Player")) continue;
                    if (Vector2.Distance(act.Value.transform.position, transform.position) < 0.5f)
                    {
                        act.Value.OnHit(damage);
                        GameObject eff = PoolManager.SpawnObject(hitEff, false);
                        eff.GetComponent<SimpleExplosion>().elapsedTime = 0;
                        eff.transform.position = transform.position;
                        eff.SetActive(true);
                        PoolManager.ReleaseObject(this.gameObject);
                        return;
                    }
                }
            }
        }

    }
}
