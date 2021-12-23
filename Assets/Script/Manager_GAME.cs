using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_GAME : MonoBehaviour
{
    public static Manager_GAME instance;

    [Header("디버그 모드")]
    public bool Bool_Debug;

    [Header("암호화 코드")]
    [SerializeField] string     STRING_Password;

    [Header("플레이어 게임 오브젝트")]
    [SerializeField] GameObject OBJ_PlayerSetting;
    static PlayerSetting        Script_PlayerSetting;

    [Header("카메라 오브젝트")]
    [SerializeField] GameObject OBJ_PlayerCamera;
    [SerializeField] GameObject OBJ_InventoryCamera;
    static CameraSetting        Script_CameraSetting;  

    [Header("Manager CrashCheck 스크립트")]
    public Manager_CrashCheck   Scirpt_Manager_CrashCheck;

    [Header("Manager World 스크립트")]
    [SerializeField] GameObject OBJ_Manager_World;
    static Manager_World        Script_Manager_World;

    [Header("Manager Block 스크립트")]
    [SerializeField] GameObject OBJ_Manager_Block;
    static Manager_Block        Script_Manager_Block;

    [Header("Manager UI 스크립트")]
    [SerializeField] GameObject OBJ_Manager_UI;
    static Manager_UI           Script_Manager_UI;

    [Header("Manager Sound 스크립트")]
    [SerializeField] GameObject OBJ_Manager_Sound;
    static Manager_Sound        Script_Manager_Sound;

    [Header("Manager Item 스크립트")]
    [SerializeField] GameObject OBJ_Manager_Item;
    static Manager_ITEM         Script_Manager_Item;

    [Header("Manager Tool 스크립트")]
    [SerializeField] GameObject OBJ_Manager_Tool;
    static Manager_Tool         Script_Manager_Tool;

    Vector3Int V3I_PrevPos, V3I_NowPos;
    Vector3Int V3_CrashCheckPos;

    // Use this for initialization
    public void Awake()
    {
        instance = this;
        instance.Init();
    }

    // Update is called once per frame
    public void Update()
    {
        instance.Updated();
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    void Init()
    {
        V3_CrashCheckPos = Vector3Int.zero;
        V3I_PrevPos = new Vector3Int();
        V3I_NowPos = new Vector3Int();

        // CSV Setting
        CSV_Init.Init();

        // Player Setting
        Script_PlayerSetting = OBJ_PlayerSetting.GetComponent<PlayerSetting>();
        Script_PlayerSetting.Init();

        // Camera Setting
        Script_CameraSetting = OBJ_PlayerCamera.GetComponent<CameraSetting>();
        Script_CameraSetting.Init();

        // Manager_World Setting
        Script_Manager_World = OBJ_Manager_World.GetComponent<Manager_World>();
        Script_Manager_World.Init();

        // Player Pos Setting
        V3I_NowPos.x = (int)OBJ_PlayerSetting.transform.position.x / Get_Manager_World_Script().Get_ChunkSize().x;
        V3I_NowPos.y = (int)OBJ_PlayerSetting.transform.position.y / Get_Manager_World_Script().Get_ChunkSize().y;
        V3I_NowPos.z = (int)OBJ_PlayerSetting.transform.position.z / Get_Manager_World_Script().Get_ChunkSize().z;

        // Manager Block Setting
        Script_Manager_Block = OBJ_Manager_Block.GetComponent<Manager_Block>();
        Script_Manager_Block.Init();

        // Manager Item Setting
        Script_Manager_Item = OBJ_Manager_Item.GetComponent<Manager_ITEM>();
        Script_Manager_Item.Init();

        // Manager UI Setting
        Script_Manager_UI = OBJ_Manager_UI.GetComponent<Manager_UI>();
        Script_Manager_UI.Init();

        // Manager Sound Setting
        Script_Manager_Sound = OBJ_Manager_Sound.GetComponent<Manager_Sound>();
        Script_Manager_Sound.Init();

        // Manager Tool Setting
        Script_Manager_Tool = OBJ_Manager_Tool.GetComponent<Manager_Tool>();
        Script_Manager_Tool.Init();

        // 충돌체크 설정
        V3I_PrevPos = V3I_NowPos;

        // 맵 빌드
        StartCoroutine(Get_Manager_World_Script().MapBuild(V3I_NowPos.x, V3I_NowPos.y, V3I_NowPos.z));
        StartCoroutine(Get_Manager_World_Script().MapBuild_OutSide(V3I_NowPos.x, V3I_NowPos.y, V3I_NowPos.z));

        // 충돌체크 박스
        Scirpt_Manager_CrashCheck.CreateColliderPool(4, 4, 4);
    }

    void Updated()
    {
        // Player Pos Setting
        V3I_NowPos.x = (int)OBJ_PlayerSetting.transform.position.x / Get_Manager_World_Script().Get_ChunkSize().x;
        V3I_NowPos.y = (int)OBJ_PlayerSetting.transform.position.y / Get_Manager_World_Script().Get_ChunkSize().y;
        V3I_NowPos.z = (int)OBJ_PlayerSetting.transform.position.z / Get_Manager_World_Script().Get_ChunkSize().z;

        // Player Update
        Script_PlayerSetting.Updated();

        // Camera Update
        Script_CameraSetting.Updated();

        // Manager_World Update
        Script_Manager_World.Updated();

        // Manager Block Update
        Script_Manager_Block.Updated();

        // Manager UI Update
        Script_Manager_UI.Updated();

        // Manager Tool Update
        Script_Manager_Tool.Updated();

        if (V3I_PrevPos.x != V3I_NowPos.x || V3I_PrevPos.y != V3I_NowPos.y || V3I_PrevPos.z != V3I_NowPos.z)
        {
            V3I_PrevPos = V3I_NowPos;

            // 맵 빌드
            StartCoroutine(Get_Manager_World_Script().MapBuild(V3I_NowPos.x, V3I_NowPos.y, V3I_NowPos.z));
        }

        if ((int)OBJ_PlayerSetting.transform.position.x != V3_CrashCheckPos.x ||
            (int)OBJ_PlayerSetting.transform.position.y != V3_CrashCheckPos.y ||
            (int)OBJ_PlayerSetting.transform.position.z != V3_CrashCheckPos.z ||
            Get_PlayerScript().BOOL_isMove)
        {
            V3_CrashCheckPos.x = (int)OBJ_PlayerSetting.transform.position.x;
            V3_CrashCheckPos.y = (int)OBJ_PlayerSetting.transform.position.y;
            V3_CrashCheckPos.z = (int)OBJ_PlayerSetting.transform.position.z;
            StartCoroutine(Scirpt_Manager_CrashCheck.SetCollidersAtChunk(4, 4, 4, OBJ_PlayerSetting.transform.position));

            Get_PlayerScript().BOOL_isMove = false;
        }
    }

    private void FixedUpdate()
    {
        // Camera Update
        Script_CameraSetting.FixedUpdated();
    }

    // Get Xor Key
    public          string          Get_XOR_Key()                   { return STRING_Password;       }

    // Get Player
    public          GameObject      Get_PlayerObject()              { return OBJ_PlayerSetting;     }
    public static   PlayerSetting   Get_PlayerScript()              { return Script_PlayerSetting;  }
                                                      
    // Get Camera                                     
    public          GameObject      Get_PlayerCamera()              { return OBJ_PlayerCamera;      }
    public          GameObject      Get_InventoryCamera()           { return OBJ_InventoryCamera;   }
    public static   CameraSetting   Get_CameraScript()              { return Script_CameraSetting;  }

    // Get Manager_World
    public          GameObject      Get_Manager_World_Object()      { return OBJ_Manager_World;     }
    public static   Manager_World   Get_Manager_World_Script()      { return Script_Manager_World;  }

    // Get Manager Block
    public          GameObject      Get_Manager_Block_Object()      { return OBJ_Manager_Block;     }
    public static   Manager_Block   Get_Manager_Block_Script()      { return Script_Manager_Block;  }

    // Get Manager UI
    public          GameObject      Get_Manager_UI_Object()         { return OBJ_Manager_UI;        }
    public static   Manager_UI      Get_Manager_UI_Script()         { return Script_Manager_UI;     }

    // Get Manager Sound
    public          GameObject      Get_Manager_Sound_Object()      { return OBJ_Manager_Sound;     }
    public static   Manager_Sound   Get_Manager_Sound_Script()      { return Script_Manager_Sound;  }

    // Get Manager Item
    public          GameObject      Get_Manager_Item_Object()       { return OBJ_Manager_Item;      }
    public static   Manager_ITEM    Get_Manager_Item_Script()       { return Script_Manager_Item;   }

    // Get Manager Tool
    public          GameObject      Get_Manager_Tool_Object()       { return OBJ_Manager_Tool;      }
    public static   Manager_Tool    Get_Manager_Tool_Script()       { return Script_Manager_Tool;   }
}
