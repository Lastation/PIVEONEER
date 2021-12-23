using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ITEM_INFO : MonoBehaviour
{
    [Header("Item Info")]
    [Tooltip("아이템 모양 설정")]
    [SerializeField] private ItemShape  ItemShape;
    [Tooltip("아이템 타입 설정")]
    [SerializeField] private ItemType   ItemType;
    [Tooltip("아이템 갯수 설정")]
    [SerializeField] private Text[]     ItemCount_Text;
    [SerializeField] private int        ItemCount;

    public ItemShape    Get_ItemShape()     { return ItemShape;     }
    public ItemType     Get_ItemType()      { return ItemType;      }
    public int          Get_ItemCount()     { return ItemCount;     }

    public void         Set_ItemShape   (ItemShape  _Shape)     { ItemShape = _Shape;   }
    public void         Set_ItemType    (ItemType   _Type)      { ItemType  = _Type;    }

    public void Set_Info_Text()
    {
        if (ItemCount != 0)
        {
            ItemCount_Text[(int)ItemInfo.Name].text     = CSV_Init.ITEM_INFO[(int)ItemType]["OBJ_Name"].ToString();
            ItemCount_Text[(int)ItemInfo.Info].text     = CSV_Init.ITEM_INFO[(int)ItemType]["OBJ_INFO"].ToString();
            ItemCount_Text[(int)ItemInfo.Weight].text   = CSV_Init.ITEM_INFO[(int)ItemType]["OBJ_Weight"].ToString();
            Manager_GAME.Get_Manager_Item_Script().Preview_Block_Item(ItemShape, ItemType);
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