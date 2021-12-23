using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Manager_ITEM : MonoBehaviour
{
    [Header("Item sprite")]
    [SerializeField] private Texture[]      Mat_ItemTexture;

    [Header("Item Preview")]
    [Tooltip("인벤토리 배열")]
    [SerializeField] private GameObject[]   OBJ_Inven_Slot;
    [Tooltip("인벤토리 아이템 추가")]
    [SerializeField] private GameObject     OBJ_Inven_Item_Add;
    [Tooltip("인벤토리 아이템 오브젝트")]
    [SerializeField] private GameObject[]   OBJ_Inven_Item_Preview;

    [Header("ETC Preview")]
    [Tooltip("기타 인벤토리")]
    [SerializeField] private GameObject     OBJ_ETC;
    [Tooltip("기타 인벤토리 배열")]
    [SerializeField] private GameObject[]   OBJ_ETC_Slot;
    [Tooltip("기타 아이템 추가")]
    [SerializeField] private GameObject     OBJ_ETC_Item_Add;
    [Tooltip("기타 아이템 오브젝트")]
    [SerializeField] private GameObject[]   OBJ_ETC_Item_Preview;

    private Dictionary<string, int>         DIC_Inventory_Item;
    private Dictionary<string, int>         DIC_ETC_Item;
    private GameObject[]                    ARRAY_Item_Slot;
    private GameObject[]                    ARRAY_ETC_Slot;

    public void Init()
    {
        DIC_Inventory_Item  = new Dictionary<string, int>();
        DIC_ETC_Item        = new Dictionary<string, int>();
        ARRAY_Item_Slot     = new GameObject[30];
        ARRAY_ETC_Slot      = new GameObject[30];

        OBJ_ETC.SetActive(false);

        Create_Items(BlockType.Lithium, 100);
        Create_Items(BlockType.IronStone, 100);
        Create_Items(BlockType.Chalcopyrite, 100);
        Create_Items(BlockType.Quartz, 100);
        Create_Items(BlockType.Bauxite, 100);
        Create_Items(BlockType.Titanite, 100);
        Create_Items(BlockType.Graphite, 100);
        Create_Items(BlockType.Diamond, 100);
        Create_Items(BlockType.Oxygen, 100);
        Create_Items(BlockType.Energy, 100);
    }

    public void Updated()
    {

    }

    public void Preview_Block_Item(ItemShape _ItemShape, ItemType _ItemType)
    {
        // Init Item Shape
        for (int i = 0; i < OBJ_Inven_Item_Preview.Length; i++)
            OBJ_Inven_Item_Preview[i].SetActive(false);

        // Set Active Items
        switch (_ItemShape)
        {
            case ItemShape.Cube:
                OBJ_Inven_Item_Preview[0].SetActive(true);
                OBJ_Inven_Item_Preview[0].GetComponent<MeshRenderer>().sharedMaterial.mainTexture = Mat_ItemTexture[(int)_ItemType];
                break;
        }
    }

    public void Create_Items(BlockType _type, int _count)
    {
        ItemType _itemtype;

        if (!System.Enum.IsDefined(typeof(ItemType), _type.ToString()))
            _itemtype = ItemType.Soil;
        else
            _itemtype = (ItemType)Enum.Parse(typeof(ItemType), _type.ToString());

        if (DIC_Inventory_Item.ContainsKey(_itemtype.ToString()))
        {
            for (int i = 0; i < OBJ_Inven_Slot.Length; i++)
            {
                if (ARRAY_Item_Slot[i] != null && ARRAY_Item_Slot[i].name == _itemtype.ToString())
                {
                    ARRAY_Item_Slot[i].GetComponent<ITEM_INFO>().Add_ItemCount(_count);
                    DIC_Inventory_Item[_itemtype.ToString()] = ARRAY_Item_Slot[i].GetComponent<ITEM_INFO>().Get_ItemCount();

                    ITEM_INFO _Inven_INFO = OBJ_Inven_Slot[i].GetComponent<ITEM_INFO>();
                    _Inven_INFO.Set_ItemCount(ARRAY_Item_Slot[i].GetComponent<ITEM_INFO>().Get_ItemCount());
                    break;
                }
            }
        }
        else
        {
            ITEM_INFO _INFO = OBJ_Inven_Item_Add.GetComponent<ITEM_INFO>();

            _INFO.Set_ItemType(_itemtype);
            _INFO.Set_ItemShape(ItemShape.Cube);
            _INFO.Set_ItemCount(_count);

            for (int i = 0; i < OBJ_Inven_Slot.Length; i++)
            {
                if (ARRAY_Item_Slot[i] == null)
                {
                    ARRAY_Item_Slot[i] = Instantiate(OBJ_Inven_Item_Add, OBJ_Inven_Slot[i].transform);
                    ARRAY_Item_Slot[i].GetComponent<RawImage>().texture = Mat_ItemTexture[(int)_INFO.Get_ItemType()];
                    ARRAY_Item_Slot[i].name = _itemtype.ToString();
                    DIC_Inventory_Item.Add(_itemtype.ToString(), ARRAY_Item_Slot[i].GetComponent<ITEM_INFO>().Get_ItemCount());

                    ITEM_INFO _Inven_INFO = OBJ_Inven_Slot[i].GetComponent<ITEM_INFO>();
                    _Inven_INFO.Set_ItemType(_itemtype);
                    _Inven_INFO.Set_ItemShape(ItemShape.Cube);
                    _Inven_INFO.Set_ItemCount(ARRAY_Item_Slot[i].GetComponent<ITEM_INFO>().Get_ItemCount());
                    break;
                }
            }
        }
    }

    public void Create_ETC_Items(String _type, int _count, Sprite _sprite)
    {
        ETCItemType _itemtype;

        if (!System.Enum.IsDefined(typeof(ETCItemType), _type.ToString()))
            return;
        else
            _itemtype = (ETCItemType)Enum.Parse(typeof(ETCItemType), _type.ToString());

        if (DIC_ETC_Item.ContainsKey(_itemtype.ToString()))
        {
            for (int i = 0; i < OBJ_ETC_Slot.Length; i++)
            {
                if (ARRAY_ETC_Slot[i] != null && ARRAY_ETC_Slot[i].name == _itemtype.ToString())
                {
                    ARRAY_ETC_Slot[i].GetComponent<ETC_INFO>().Add_ItemCount(_count);
                    DIC_ETC_Item[_itemtype.ToString()] = ARRAY_ETC_Slot[i].GetComponent<ETC_INFO>().Get_ItemCount();

                    ETC_INFO _ETC_INFO = OBJ_ETC_Slot[i].GetComponent<ETC_INFO>();
                    _ETC_INFO.Set_ItemCount(ARRAY_ETC_Slot[i].GetComponent<ETC_INFO>().Get_ItemCount());
                    break;
                }
            }
        }
        else
        {
            ETC_INFO _INFO = OBJ_ETC_Item_Add.GetComponent<ETC_INFO>();

            _INFO.Set_ItemType(_itemtype);
            _INFO.Set_ItemCount(_count);

            for (int i = 0; i < OBJ_ETC_Slot.Length; i++)
            {
                if (ARRAY_ETC_Slot[i] == null)
                {
                    ARRAY_ETC_Slot[i] = Instantiate(OBJ_ETC_Item_Add, OBJ_ETC_Slot[i].transform);
                    ARRAY_ETC_Slot[i].GetComponent<Image>().sprite = _sprite;
                    ARRAY_ETC_Slot[i].name = _itemtype.ToString();
                    DIC_ETC_Item.Add(_itemtype.ToString(), ARRAY_ETC_Slot[i].GetComponent<ETC_INFO>().Get_ItemCount());

                    ETC_INFO _ETC_INFO = OBJ_ETC_Slot[i].GetComponent<ETC_INFO>();
                    _ETC_INFO.Set_ItemType(_itemtype);
                    _ETC_INFO.Set_ItemCount(ARRAY_ETC_Slot[i].GetComponent<ETC_INFO>().Get_ItemCount());
                    break;
                }
            }
        }
    }

    public void Inventory_Update()
    {
        for (int i = 0; i < OBJ_Inven_Slot.Length; i++)
        {
            if (ARRAY_Item_Slot[i] != null)
            {
                ITEM_INFO _Inven_INFO = OBJ_Inven_Slot[i].GetComponent<ITEM_INFO>();
                Debug.Log(DIC_ETC_Item[ARRAY_ETC_Slot[i].name]);
                if (DIC_ETC_Item.ContainsKey(ARRAY_ETC_Slot[i].name))
                    _Inven_INFO.Set_ItemCount(DIC_ETC_Item[ARRAY_ETC_Slot[i].name]);
                _Inven_INFO.Set_Info_Text();
            }
        }
    }

    public Dictionary<string, int> Get_Inventory_Item() { return DIC_Inventory_Item;    }
    public Dictionary<string, int> Get_ETC_Item()       { return DIC_ETC_Item;          }
}
