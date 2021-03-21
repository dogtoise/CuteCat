using UnityEngine;
class FallProjectile : MonoBehaviour
{
    private Actor owner;
    public GameObject explosionPrefab;
    // private Actor target;
    private float damage;
    private float speed;
    public float explosionRange = 1;
    bool isInit = false;

    Vector3 direction;
    Vector3 startPos;
    Vector3 distPos;

    public void Initialize(Actor owner, Vector3 randomPos, float damage, float speed)
    {
        transform.position = randomPos;

        this.owner = owner;
        //this.target = target;
        this.damage = damage;
        this.speed = speed;
        transform.position = randomPos;
        startPos = transform.position;
        distPos = new Vector3(transform.position.x - 2, transform.position.y - 4, 0);

        direction = (owner as Player).preDir.normalized;
        explosionPrefab = Resources.Load("Prefabs/AttackSkills/SimpleExplosion") as GameObject;
        //direction = target.transform.position - transform.position;
        //direction = direction.normalized;
        isInit = true;
    }
    private void Update()
    {
        if (isInit)
        {
            direction = distPos - transform.position;
            direction = direction.normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector2.Distance(distPos, transform.position) < 1)
            {
                GameObject eff =  PoolManager.SpawnObject(explosionPrefab,false) as GameObject;
                eff.GetComponent<SimpleExplosion>().elapsedTime = 0;
                eff.SetActive(true);
                eff.transform.position = transform.position;
                foreach (var act in Actor.allActors)
                {
                    if (act.Value.gameObject.CompareTag("Player")) continue;
                    if (Vector2.Distance(act.Value.transform.position, transform.position) < explosionRange)
                    {
                        act.Value.OnHit(damage);
                    }
                }
                PoolManager.ReleaseObject(this.gameObject);
            }
            //if (Vector2.Distance(transform.position, startPos) > 10)
            //{
            //    PoolManager.ReleaseObject(this.gameObject);
            //    return;
            //}
           

        }

    }
}
