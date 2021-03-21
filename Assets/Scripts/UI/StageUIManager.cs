using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageUIManager : SingletonMonoBehaviour<StageUIManager>
{
    public GameObject SkillChoicePanel;
    public GameObject PropertyChoicePanel;
    public GameObject GameOverPanel;
    public GameObject OptionPanel;
    public GameObject ShopPanel;
    public Text totalPlayTime;
    public Text totalKill;

    public LevelChanger levelChanger;

    public Image[] skillDefault;
    public Sprite[] skillSprite;

    public Text option_level;
    public Text option_lifeTime;

    public Text gameOver_lifeTime;
    public Text gameOver_kill;
    public Text gameOver_level;

    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();

    }
    // Skill Choice Panel
    public void ShowSkillChoicePanel()
    {
        Time.timeScale = 0;
        SkillChoicePanel.SetActive(true);
    }
    public void HideSkillChoicePanel()
    {
        Time.timeScale = 1;
        SkillChoicePanel.SetActive(false);
    }
    public void ShowOptionPanel()
    {
        Time.timeScale = 0;
        option_level.text = string.Format("현재 레벨 {0}", player.level);
        option_lifeTime.text = string.Format("생존시간 {0}분 {1}초", (int)GlobalVars.TotalPlayTime_Min, (int)GlobalVars.TotalPlayTime_Sec);
        OptionPanel.SetActive(true);
    }
    public void HideOptionPanel()
    {
        Time.timeScale = 1;
        OptionPanel.SetActive(false);
    }
    public void ShowGameOverPane()
    {
        Time.timeScale = 0;
        gameOver_level.text = string.Format("달성 레벨 {0}", player.level);
       gameOver_lifeTime.text = string.Format("생존시간 {0}분 {1}초", (int)GlobalVars.TotalPlayTime_Min, (int)GlobalVars.TotalPlayTime_Sec);
        gameOver_kill.text = string.Format("킬스코어 {0} Kill", (int)GlobalVars.TotalKill);
        GameOverPanel.SetActive(true);
    }
    public void HideGameOverPanel()
    {
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
    }
    public void ShowShopPanel()
    {
        Time.timeScale = 0;
        ShopPanel.SetActive(true);
    }
    public void HideShopPanel()
    {
        Time.timeScale = 1;
        ShopPanel.SetActive(false);
    }
    public List<int> skill = new List<int>();
    public void ChoiceSkill(int index) // 1 ~ 3
    {
        if (skill == null) skill = new List<int>();
        
        
        skill.Add(index);
        for (int i = 0; i < skill.Count; i++)
        {
            skillDefault[i].sprite = skillSprite[skill[i]];
        }

    }
    public void GoToLobby()
    {
        Time.timeScale = 1;
        levelChanger.FadeToLevel(0);
    }
    public void GoToStage()
    {
        Time.timeScale = 1;
        levelChanger.FadeToLevel(1);
    }
    private void Update()
    {
        //totalKill.text = ((int)GlobalVars.TotalKill).ToString();
        totalPlayTime.text =
            string.Format("생존시간 {0}분 {1}초", GlobalVars.TotalPlayTime_Min, (int)GlobalVars.TotalPlayTime_Sec);
    }
    public void ReturnExp()
    {
        Time.timeScale = 1;

        Player player = FindObjectOfType<Player>();
        player.TakeExp(player.expRequired[player.level] / 2);
        SkillChoicePanel.SetActive(false);
        Time.timeScale = 1;
    }
    int getSkillIndex = 0;
    public void GetSkill(int skill_index)
    {
        if (skill == null) skill = new List<int>();

        skillDefault[getSkillIndex++].sprite = skillSprite[skill_index- 1];
    }

    public void OnKill_Increased()
    {
        StartCoroutine(Kill_Increase());
    }

    int funtSize_default = 150;
    IEnumerator Kill_Increase()
    {
        totalKill.text = GlobalVars.TotalKill.ToString();
        totalKill.fontSize = funtSize_default * 2;
        float curSize = totalKill.fontSize;
        //300 ~ 150
        while (true)
        {
            curSize = Mathf.Lerp(curSize, funtSize_default, Time.deltaTime * 6);
            totalKill.fontSize = (int)curSize;
            if (Mathf.Abs(curSize - funtSize_default) < 10)
            {
                totalKill.fontSize = funtSize_default;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
