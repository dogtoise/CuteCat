using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public GameObject mapSelectDialog;


    public int mapCount = 1;
    private int mapSelectIndex = 0;
    public void ShowMapSelectDialog()
    {
        mapSelectDialog.SetActive(true);
    }
    public void HideMapSelectDialog()
    {
        mapSelectDialog.SetActive(false);
    }

    public void MapChangeLeft()
    {
        if (mapSelectIndex == 0) return;
        mapSelectIndex--;
    }
    public void MapChangeRight()
    {
        mapSelectIndex++;
    }

    public void GoToSelectedMap()
    {
        // TODO : 페이드 인아웃
    }


}
