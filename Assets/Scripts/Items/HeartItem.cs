using UnityEngine;

class HeartItem : MonoBehaviour
{
    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();   
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 0.5f)
        {
            player.IncreaseHP(30);
            PoolManager.ReleaseObject(this.gameObject);
        }
    }
}

