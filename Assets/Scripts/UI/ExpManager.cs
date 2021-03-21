using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpManager : MonoBehaviour
{
    public Image fillBar;
    float totalExp;
    float totalExp_old;
    float requiredExp;

    Player player;
    private void Start()
    {
        totalExp = 0;
        totalExp_old = 0;
        requiredExp = 0;
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        totalExp_old = Mathf.Lerp(totalExp_old, totalExp, Time.deltaTime);
            fillBar.fillAmount = totalExp_old / requiredExp;
        
    }
    public void IncreaseExp(float totalExp, float requiredExp)
    {
        totalExp_old = this.totalExp;
        this.totalExp = totalExp;
        this.requiredExp = requiredExp;

        if (totalExp >= requiredExp && player.LevelMax- 1 > player.level)
        {
            this.totalExp = 0;
            this.totalExp_old = 0;
            this.requiredExp = 0;
            player.LevelUp();
        }
    }

}
