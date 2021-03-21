using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
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
            PoolManager.ReleaseObject(this.gameObject);
            StageUIManager.Instance.ShowShopPanel();
        }
    }


}
