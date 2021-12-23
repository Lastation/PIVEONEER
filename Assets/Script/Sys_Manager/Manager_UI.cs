using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Manager_UI : MonoBehaviour
{
    GameObject OBJ_UI_Inventory;

    [Header("Canvas")]
    [Tooltip("ESC 눌렀을때")]
    [SerializeField] private    GameObject              OBJ_Pause_Canvas;
    [Tooltip("미니맵")]
    [SerializeField] private    GameObject              OBJ_MiniMap;
    [SerializeField] private    BiomeDisplay            SCRIPT_MiniMap;
    [Tooltip("환경창")]
    [SerializeField] private    GameObject              OBJ_Enviroment;

    [Tooltip("인벤토리 조합")]
    [SerializeField] private    ITEM_Combination        SCRIPT_Combination;

    [Header("Compass Setting")]
    [SerializeField] private    GameObject      OBJ_Compass;
    [SerializeField] private    RawImage        RAWIMG_Compass;
    [SerializeField] private    Text            TEXT_Compass;

    private Vector3 V3_PrevPos;

    public void Init()
    {
        // Position Error Detector
        OBJ_UI_Inventory = Manager_GAME.instance.Get_InventoryCamera();
        OBJ_UI_Inventory.SetActive(true);
        OBJ_UI_Inventory.SetActive(false);

        SCRIPT_MiniMap.Init();
        OBJ_Enviroment.GetComponent<UI_Enviroment>().Init();

        SCRIPT_Combination.Init();
    }

    // Update is called once per frame
    public void Updated()
    {
        // UI KeyKode
        UI_KeyInput_Action();

        // MiniMap Update
        if (OBJ_MiniMap.activeInHierarchy)                  MiniMapUpdate();

        // Compass Update
        if (RAWIMG_Compass.gameObject.activeInHierarchy)    Compass_Update();

        // Enviroment UI Update
        if (OBJ_Enviroment.activeInHierarchy)               OBJ_Enviroment.GetComponent<UI_Enviroment>().Updated();
    }

    public void MiniMapUpdate()
    {
        SCRIPT_MiniMap.Update_Sight();

        if (V3_PrevPos.x != Manager_GAME.Get_PlayerScript().transform.position.x || V3_PrevPos.z != Manager_GAME.Get_PlayerScript().transform.position.z)
        {
            V3_PrevPos.x = Manager_GAME.Get_PlayerScript().transform.position.x;
            V3_PrevPos.z = Manager_GAME.Get_PlayerScript().transform.position.z;

            StartCoroutine(SCRIPT_MiniMap.Updated());
        }
    }

    public void UI_KeyInput_Action()
    {
        // Inventory UI
        if (Input.GetKeyDown(Manager_Key.instance.Inventory_Key))
        {
            if (!OBJ_UI_Inventory.activeInHierarchy)
            {
                // Camera Setting
                Manager_GAME.Get_CameraScript().UnLockCursor();

                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.decision);

                OBJ_UI_Inventory.SetActive(true);
                OBJ_MiniMap.SetActive(false);
                OBJ_Enviroment.SetActive(false);
                OBJ_Compass.SetActive(false);
                return;
            }
            else
            {
                // Camera Setting
                Manager_GAME.Get_CameraScript().LockCursor();

                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.cancel);
                OBJ_UI_Inventory.SetActive(false);
                OBJ_Compass.SetActive(true);
                return;
            }
        }

        // Setting UI
        if (Input.GetKeyDown(Manager_Key.instance.Setting_Key))
        {
            if (OBJ_UI_Inventory.activeInHierarchy)
            {
                // Camera Setting
                Manager_GAME.Get_CameraScript().LockCursor();

                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.cancel);
                OBJ_UI_Inventory.SetActive(false);
                OBJ_Compass.SetActive(true);
                return;
            }

            if (!OBJ_Pause_Canvas.activeInHierarchy)
            {
                // Pause Setting
                Manager_GAME.Get_CameraScript().UnLockCursor();

                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.decision);
                OBJ_Pause_Canvas.SetActive(true);
                return;
            }
            else
            {
                // Pause Setting
                Manager_GAME.Get_CameraScript().LockCursor();

                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.cancel);
                OBJ_Pause_Canvas.SetActive(false);
                return;
            }
        }

        // MiniMap UI
        if (Input.GetKeyDown(Manager_Key.instance.MiniMap_Key))
        {
            if (!OBJ_MiniMap.activeInHierarchy)
            {
                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.mapOpen);
                OBJ_MiniMap.SetActive(true);
                return;
            }
            else
            {
                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.cancel);
                OBJ_MiniMap.SetActive(false);
                return;
            }
        }

        // Enviroment UI
        if (Input.GetKeyDown(Manager_Key.instance.Enviroment_Key))
        {
            if (!OBJ_Enviroment.activeInHierarchy)
            {
                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.decision);
                OBJ_Enviroment.SetActive(true);
                return;
            }
            else
            {
                // Sound
                Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.UI, (int)Sound_UI.cancel);
                OBJ_Enviroment.SetActive(false);
                return;
            }
        }
    }

    public void Compass_Update()
    {
        RAWIMG_Compass.uvRect = new Rect((Manager_GAME.Get_PlayerScript().transform.localEulerAngles.y - 90) / 360, 0, 1, 1);

        Vector3 V3_Forward = Manager_GAME.Get_PlayerScript().transform.forward;

        V3_Forward.y = 0;

        float FLOAT_HeadingAngle = Quaternion.LookRotation(V3_Forward).eulerAngles.y - 90;

        FLOAT_HeadingAngle = 5 * (Mathf.RoundToInt(FLOAT_HeadingAngle / 5.0f));

        int INT_DisplayAngle;
        INT_DisplayAngle = Mathf.RoundToInt(FLOAT_HeadingAngle);

        // 0 이하일시
        if (FLOAT_HeadingAngle < 0)
        {
            FLOAT_HeadingAngle += 360;
            INT_DisplayAngle += 360;
        }

        switch (INT_DisplayAngle)
        {
            case 0:
                TEXT_Compass.text = "N";
                break;
            case 45:
                TEXT_Compass.text = "NE";
                break;
            case 90:
                TEXT_Compass.text = "E";
                break;
            case 135:
                TEXT_Compass.text = "SE";
                break;
            case 180:
                TEXT_Compass.text = "S";
                break;
            case 225:
                TEXT_Compass.text = "SW";
                break;
            case 270:
                TEXT_Compass.text = "W";
                break;
            case 315:
                TEXT_Compass.text = "NW";
                break;
            case 360:
                TEXT_Compass.text = "N";
                break;
            default:
                TEXT_Compass.text = FLOAT_HeadingAngle.ToString();
                break;
        }
    }
}
