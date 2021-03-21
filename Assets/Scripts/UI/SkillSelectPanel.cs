using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct SkillDescription
{
    [SerializeField]
    public int id;
    [SerializeField]
    public Sprite iconSprite;
    [SerializeField]
    public string description;
    [SerializeField]
    public int maxLevel;
    
}
public enum SkillType
{
    FireBall,
    MaxHPUp,
    PowerUp,
    CycleMagicUp,
    MoveSpeedUp,
    MagicBall,
}
public class SkillSelectPanel : SelectPanel
{
    [SerializeField]
    public SkillDescription[] skillDescs;

    public Image[] ItemIcons;

    public Text[] ItemDescriptions;
    public Button[] SelectButtons;

    public Text levelTxt;

    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();

    }
    protected override void OnEnable()
    {
        if(player == null)
            player = FindObjectOfType<Player>();
        levelTxt.text = "현재 레벨 " + player.level;
        int index = GetRandomIndex();
        GetItemIcon(0).sprite = GetItemSprite(index);


     
        GetText(0).text = GetDescription(index);
        GetTouchBtn(0).onClick.RemoveAllListeners();
        GetTouchBtn(0).onClick.AddListener(() =>
        {
            StageUIManager.Instance.HideSkillChoicePanel();
        });
       GetTouchBtn(0).onClick.AddListener(() =>
        {
            TouchedItem(index);
        });

        int secindex = GetRandomIndex();
        while (secindex == index)
        {
            secindex = GetRandomIndex();
        }
        GetItemIcon(1).sprite = GetItemSprite(secindex);
        GetText(1).text = GetDescription(secindex);
        GetTouchBtn(1).onClick.RemoveAllListeners();
        GetTouchBtn(1).onClick.AddListener(() =>
        {
            StageUIManager.Instance.HideSkillChoicePanel();
        });
        GetTouchBtn(1).onClick.AddListener(() =>
        {
            TouchedItem(secindex);
        });

        int thirdindex = GetRandomIndex();
        while (secindex == thirdindex || index == thirdindex)
        {
            thirdindex = GetRandomIndex();
        }
        GetItemIcon(2).sprite = GetItemSprite(thirdindex);
        GetText(2).text = GetDescription(thirdindex);
        GetTouchBtn(2).onClick.RemoveAllListeners();
        GetTouchBtn(2).onClick.AddListener(() =>
        {
            StageUIManager.Instance.HideSkillChoicePanel();
        });
        GetTouchBtn(2).onClick.AddListener(() =>
        {
            TouchedItem(thirdindex);
        });
    }
    public override string GetDescription(int index)
    {
        if (player == null) player = FindObjectOfType<Player>();
        string desc2 = null;
        switch ((SkillType)index)
        {
            case SkillType.FireBall:
                desc2 = player.attackSkills[(int)AttackType.FireBall].name + " 다음레벨" +player.attackSkills[(int)AttackType.FireBall].skill.GetLevel() + "\n";
                break;
            case SkillType.MagicBall:
                desc2 = player.attackSkills[(int)AttackType.MagicBall].name + " 다음레벨" + player.attackSkills[(int)AttackType.MagicBall].skill.GetLevel() + "\n";
                break;
            case SkillType.CycleMagicUp:
                desc2 = player.attackSkills[(int)AttackType.Cycle].name + " 다음레벨" + player.attackSkills[(int)AttackType.Cycle].skill.GetLevel() + "\n";
                break;
        }
        //  int level = player.attackSkills[index].skill.GetLevel();
        //string desc2 = player.attackSkills[index].name + " 다음레벨" +player.attackSkills[index].skill.GetLevel();
        desc2 = desc2 + skillDescs[index].description;
        return desc2;
    }

    public override Image GetItemIcon(int index)
    {
        return ItemIcons[index];
    }

    public override int GetItemIndexMax()
    {
        return skillDescs.Length;
    }

    public override Sprite GetItemSprite(int index)
    {
        return skillDescs[index].iconSprite;
    }


    public override Text GetText(int index)
    {
        return ItemDescriptions[index];
    }

    public override Button GetTouchBtn(int index)
    {
        return SelectButtons[index];
    }

    public override void TouchedItem(int itemIndex)
    {
        switch((SkillType)itemIndex)
        {
            case SkillType.FireBall:
                player.FireBallLevelUp();
                break;
            case SkillType.MaxHPUp:
                player.MaxHpLevelUp();
                break;
            case SkillType.MoveSpeedUp:
                player.MoveSpeedLevelUp();
                break;
            case SkillType.PowerUp:
                player.PowerLevelUp();
                break;
            case SkillType.MagicBall:
                player.MagicBallLevelUp();
                break;
            case SkillType.CycleMagicUp:
                player.CycleMagicLevelUp();
                break;
        }
    }
}
