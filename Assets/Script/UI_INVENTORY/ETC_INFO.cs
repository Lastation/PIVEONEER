using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ETC_INFO : MonoBehaviour
{
    [SerializeField] private ETCItemType    ItemType;
    [SerializeField] private Text[]         ItemCount_Text;
    [SerializeField] private int            ItemCount;

    public ETCItemType  Get_ItemType()          { return ItemType;  }
    public int          Get_ItemCount()         { return ItemCount; }

    public void Set_ItemType(ETCItemType _Type) { ItemType = _Type; }

    public void Set_Info_Text()
    {
        if (ItemCount != 0)
        {
            ItemCount_Text[(int)ItemInfo.Name].text         = CSV_Init.Combination[(int)ItemType]["OBJ_Name"].ToString();
            ItemCount_Text[(int)ItemInfo.Info].text         = CSV_Init.Combination[(int)ItemType]["OBJ_INFO"].ToString();
            ItemCount_Text[(int)ItemInfo.Weight].text       = CSV_Init.Combination[(int)ItemType]["OBJ_Weight"].ToString();
        }
    }

    public void Set_ItemCount(int _Count)
    {
        ItemCount = _Count;
        if (ItemCount_Text.Length == 1)
            ItemCount_Text[0].text = ItemCount.ToString();
    }

    public void Add_ItemCount(int _Count)
    {
        ItemCount += _Count;
        if (ItemCount_Text.Length == 1)
            ItemCount_Text[0].text = ItemCount.ToString();
    }
}