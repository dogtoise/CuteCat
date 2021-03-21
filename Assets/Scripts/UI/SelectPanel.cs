using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class SelectPanel : MonoBehaviour
{
    protected virtual void OnEnable()
    {
       // int index = GetRandomIndex();
       // GetItemIcon(0).sprite = GetItemSprite(index);
       // GetText(0).text = GetDescription(index);
       // GetTouchBtn(0).onClick.AddListener(() =>
       //{
       //    TouchedItem(index);
       //});

       // index = GetRandomIndex();
       // GetItemIcon(1).sprite = GetItemSprite(index);
       // GetText(1).text = GetDescription(index);
       // GetTouchBtn(1).onClick.AddListener(() =>
       // {
       //     TouchedItem(index);
       // });

       // index = GetRandomIndex();
       // GetItemIcon(2).sprite = GetItemSprite(index);
       // GetText(2).text = GetDescription(index);
       // GetTouchBtn(2).onClick.AddListener(() =>
       // {
       //     TouchedItem(index);
       // });
    }

    private void OnDisable()
    {
        
    }

    public int GetRandomIndex()
    {
        return UnityEngine.Random.Range(0, GetItemIndexMax());
    }
    // 1 ~3
    public abstract Image GetItemIcon(int index);

    public abstract Sprite GetItemSprite(int index);


    // 1 ~ 3
    public abstract Text GetText(int index);
    public abstract Button GetTouchBtn(int index);
    public abstract string GetDescription(int index);
    public abstract int GetItemIndexMax();

    public abstract void TouchedItem(int itemIndex);


}
