using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct ItemDescription
{
    [SerializeField]
    public int id;
    [SerializeField]
    public Sprite iconSprite;
    [SerializeField]
    public string description;
}
public enum ItemType
{
    Mask1,
    Mask2,
    Mask3,
    Heart1,
    Heart2,
    Heart3,
}
public class ShopSelectPanel : SelectPanel
{
    [SerializeField]
    public ItemDescription[] itemDescs;

    public Image[] ItemIcons;
    public Sprite defaultIconSprite;

    public Text[] ItemDescriptions;
    public Button[] SelectButtons;

    Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    int tempIndex;
     protected override void OnEnable()
    {
        if(player == null)
            player = FindObjectOfType<Player>();
        int index = GetRandomIndex();
        tempIndex = index;
        GetItemIcon(0).sprite = GetItemSprite(index);
        GetText(0).text = GetDescription(index);
        GetTouchBtn(0).onClick.RemoveAllListeners();
        GetTouchBtn(0).onClick.AddListener(() =>
        {
            StageUIManager.Instance.HideShopPanel();
        });
      GetTouchBtn(0).onClick.AddListener(() =>
        {
            TouchedItem(index);
        });
        if (player.equipedItems.Count == 0)
        {
            GetItemIcon(1).sprite = defaultIconSprite;
            GetText(1).text = " ";
            GetTouchBtn(1).onClick.RemoveAllListeners();
            GetItemIcon(2).sprite = defaultIconSprite;
            GetText(2).text = " ";
            GetTouchBtn(2).onClick.RemoveAllListeners();
        }
        else if (player.equipedItems.Count > 0)
        {
            index = GetRandomIndex();
            GetItemIcon(1).sprite = GetItemSprite((int)player.equipedItems[0]);
            GetText(1).text = GetDescription((int)player.equipedItems[0]);
            GetTouchBtn(1).onClick.RemoveAllListeners();
          GetTouchBtn(1).onClick.AddListener(() =>
            {
                Remove((int)player.equipedItems[0]);
            });

            if (player.equipedItems.Count > 1)
            {
                index = GetRandomIndex();
                GetItemIcon(2).sprite = GetItemSprite((int)player.equipedItems[1]);
                GetText(2).text = GetDescription((int)player.equipedItems[1]);
                GetTouchBtn(2).onClick.RemoveAllListeners();
                GetTouchBtn(2).onClick.AddListener(() =>
                {
                    Remove((int)player.equipedItems[1]);
                });
            }
        }
    }
    public void Refresh()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
        int index = tempIndex;
        GetItemIcon(0).sprite = GetItemSprite(index);
        GetText(0).text = GetDescription(index);
        GetTouchBtn(0).onClick.RemoveAllListeners();
       GetTouchBtn(0).onClick.AddListener(() =>
        {
            StageUIManager.Instance.HideShopPanel();
        });
        GetTouchBtn(0).onClick.AddListener(() =>
        {
            TouchedItem(index);
        });
        if (player.equipedItems.Count <= 0)
        {
            GetItemIcon(1).sprite = defaultIconSprite;
            GetText(1).text = null;
            GetTouchBtn(1).onClick.RemoveAllListeners();
           GetItemIcon(2).sprite = defaultIconSprite;
            GetText(2).text = null;
            GetTouchBtn(2).onClick.RemoveAllListeners();
        }
        else if (player.equipedItems.Count > 0)
        {
            GetItemIcon(1).sprite = GetItemSprite((int)player.equipedItems[0]);
            GetText(1).text = GetDescription((int)player.equipedItems[0]);
            GetTouchBtn(1).onClick.RemoveAllListeners();
         GetTouchBtn(1).onClick.AddListener(() =>
            {
                Remove((int)player.equipedItems[0]);
            });

            if (player.equipedItems.Count > 1)
            {
                GetItemIcon(2).sprite = GetItemSprite((int)player.equipedItems[1]);
                GetText(2).text = GetDescription((int)player.equipedItems[1]);
                GetTouchBtn(2).onClick.RemoveAllListeners();
                GetTouchBtn(2).onClick.AddListener(() =>
                {
                    Remove((int)player.equipedItems[1]);
                });
            }
            else
            {
                GetItemIcon(2).sprite = defaultIconSprite;
                GetText(2).text = null;
            }
        }
    }
    public override string GetDescription(int index)
    {
        return itemDescs[index].description;
    }

    public override Image GetItemIcon(int index)
    {
        return ItemIcons[index];
    }

    public override int GetItemIndexMax()
    {
        return itemDescs.Length;
    }

    public override Sprite GetItemSprite(int index)
    {
        return itemDescs[index].iconSprite;
    }


    public override Text GetText(int index)
    {
        return ItemDescriptions[index];
    }

    public override Button GetTouchBtn(int index)
    {
        return SelectButtons[index];
    }

    public void Remove(int itemIndex)
    {
        player.equipedItems.Remove((ItemType)itemIndex);
        Refresh();
    }
    public override void TouchedItem(int itemIndex)
    {
        player.equipedItems.Add((ItemType)itemIndex);
    }
}
