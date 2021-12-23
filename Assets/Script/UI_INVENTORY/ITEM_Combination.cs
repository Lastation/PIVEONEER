using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ITEM_Combination : MonoBehaviour
{
    [Header("조합 관련 설정")]
    [Tooltip("오브젝트 이미지")]
    [SerializeField] Image      TEXTURE_Image;
    [SerializeField] Sprite[]   SPRITE_ICONS;
    [Tooltip("오브젝트 이름")]
    [SerializeField] Text       TEXT_Name;
    [Tooltip("오브젝트 설명")]
    [SerializeField] Text       TEXT_Info;
    [Tooltip("오브젝트 필요한 재료")]
    [SerializeField] Text       TEXT_Material_Name;
    [SerializeField] Text       TEXT_Material_Num;

    private int         INT_NowPage;            // Page
    private string[]    STRING_Materials;       // Materials

    public void Init()
    {
        INT_NowPage = 0;

        Preview_Item_Combination();
    }

    public void Set_Next_Combination()
    {
        INT_NowPage += 1;
        if (INT_NowPage > CSV_Init.Combination.Count - 1)   INT_NowPage = 0;

        Preview_Item_Combination();
    }

    public void Set_Previous_Combination()
    {
        INT_NowPage -= 1;
        if (INT_NowPage < 0)    INT_NowPage = CSV_Init.Combination.Count - 1;

        Preview_Item_Combination();
    }

    public void Item_Combination()
    {
        for (int i = 0; i < STRING_Materials.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                if (Item_Combination_Need(STRING_Materials[i]) > int.Parse(STRING_Materials[i + 1]))
                    continue;
                else
                    return;
            }
        }

        for (int i = 0; i < STRING_Materials.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                Item_Combination_Set(STRING_Materials[i], int.Parse(STRING_Materials[i + 1]));
            }
        }

        Manager_GAME.Get_Manager_Item_Script().Create_ETC_Items(CSV_Init.Combination[INT_NowPage]["OBJ"].ToString(), 1, SPRITE_ICONS[INT_NowPage]);
        Manager_GAME.Get_Manager_Item_Script().Inventory_Update();
        Preview_Item_Combination();
    }

    private void Preview_Item_Combination()
    {
        if (CSV_Init.Combination != null)
        {
            if (INT_NowPage < SPRITE_ICONS.Length) TEXTURE_Image.sprite = SPRITE_ICONS[INT_NowPage];
            TEXT_Name.text = CSV_Init.Combination[INT_NowPage]["OBJ_Name"].ToString();
            TEXT_Info.text = CSV_Init.Combination[INT_NowPage]["OBJ_INFO"].ToString();

            STRING_Materials = CSV_Init.Combination[INT_NowPage]["Materials"].ToString().Split('&',' ');

            TEXT_Material_Name.text = null;
            TEXT_Material_Num.text = null;

            for (int i = 0; i < STRING_Materials.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    TEXT_Material_Name.text += STRING_Materials[i] + "\n";
                    TEXT_Material_Num.text += Item_Combination_Need(STRING_Materials[i]).ToString("00") + "/" + int.Parse(STRING_Materials[i + 1]).ToString("00") + "\n";
                }
            }
        }
    }

    private int Item_Combination_Need(string _Material)
    {
        int INT_ITEM_NUM = 0;

        switch (_Material)
        {
            case "흙":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Soil.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "리튬":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Lithium.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "철":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.IronStone.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "황동석":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Chalcopyrite.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "석영":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Quartz.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "철반석":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Bauxite.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "티타나이트":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Titanite.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "석탄":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Graphite.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "다이아몬드":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Diamond.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "산소":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Oxygen.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            case "에너지":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item().TryGetValue(ItemType.Energy.ToString(), out INT_ITEM_NUM);
                return INT_ITEM_NUM;
            default:
                INT_ITEM_NUM = 0;
                return INT_ITEM_NUM;
        }
    }

    private int Item_Combination_Set(string _Material, int _Num)
    {
        int INT_ITEM_NUM = 0;

        switch (_Material)
        {
            case "흙":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Soil.ToString()]           -= _Num;
                return INT_ITEM_NUM;
            case "리튬":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Lithium.ToString()]        -= _Num;
                return INT_ITEM_NUM;
            case "철":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.IronStone.ToString()]      -= _Num;
                return INT_ITEM_NUM;
            case "황동석":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Chalcopyrite.ToString()]   -= _Num;
                return INT_ITEM_NUM;
            case "석영":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Quartz.ToString()]         -= _Num;
                return INT_ITEM_NUM;
            case "철반석":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Bauxite.ToString()]        -= _Num;
                return INT_ITEM_NUM;
            case "티타나이트":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Titanite.ToString()]       -= _Num;
                return INT_ITEM_NUM;
            case "석탄":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Graphite.ToString()]       -= _Num;
                return INT_ITEM_NUM;
            case "다이아몬드":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Diamond.ToString()]        -= _Num;
                return INT_ITEM_NUM;
            case "산소":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Oxygen.ToString()]         -= _Num;
                return INT_ITEM_NUM;
            case "에너지":
                Manager_GAME.Get_Manager_Item_Script().Get_Inventory_Item()[ItemType.Energy.ToString()]         -= _Num;
                return INT_ITEM_NUM;
            default:
                INT_ITEM_NUM = 0;
                return INT_ITEM_NUM;
        }
    }
}
