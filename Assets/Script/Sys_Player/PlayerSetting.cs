using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSetting : MonoBehaviour
{
    [Header("Sound 설정")]
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip[] walkSound;
    [SerializeField] private AudioClip[] runSound;

    [Header("캐릭터 이동속도 설정")]
    [SerializeField] private float FLOAT_MovementSpeed;

    [Header("캐릭터 달리기 설정")]
    [SerializeField] private float runSpeed;

    [Header("캐릭터 점프 설정")]
    [SerializeField] private AnimationCurve JumpFallOff;
    [SerializeField] private float          JumpMultiplier;

    [Header("블럭 인식 오브젝트")]
    [SerializeField] private GameObject Blockhit_Obj;

    [Header("에너지 소모량 설정")]
    [SerializeField] private float FLOAT_Hungry_Walk;
    [SerializeField] private float FLOAT_Hungry_Run;
    [SerializeField] private float FLOAT_Hungry_Jump;
    [SerializeField] private float FLOAT_Energy_Tool;

    private CharacterController charController;
    private BlockType Enum_BlockType;

    private Vector3 V3_CheckHitPos;
    private Vector3 V3_ScreenRay;

    private Ray ray;

    // Player Values
    private float FLOAT_HP_value;
    private float FLOAT_Hungry_value;
    private float FLOAT_Energy_value;
    private float FLOAT_Oxygen_value;

    private float FLOAT_MovementMultiplier;
    private float FLOAT_WalkTime, FLOAT_MiningTime, FLOAT_CreateTime;
    private bool BOOL_isJumping, BOOL_isRunning, BOOL_isMinning, BOOL_isCreating;
    public  bool BOOL_isMove;

    private PlayerToolType Enum_ToolType;
    private WorldChunks worldChunks;

    // Use this for initialization
    public void Init()
    {
        // Init Player Value
        FLOAT_HP_value      = 20.0f;
        FLOAT_Hungry_value  = 10.0f;
        FLOAT_Energy_value  = 20.0f;
        FLOAT_Oxygen_value  = 240.0f;

        // Tool Setting
        Enum_ToolType   = PlayerToolType.Collector;
        // Block Type Setting
        Enum_BlockType  = BlockType.Air;

        charController = GetComponent<CharacterController>();

        V3_ScreenRay = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    // Update is called once per frame
    public void Updated()
    {
        if (!Manager_GAME.instance.Bool_Debug && Manager_GAME.Get_Manager_World_Script().BOOL_INIT_MAP && Cursor.lockState == CursorLockMode.Locked)
        {
            this.GetComponent<CharacterController>().enabled = true;

            PlayerMovement();

            switch (Enum_ToolType)
            {
                case PlayerToolType.Collector:
                    CheckHitBox();
                    break;
                case PlayerToolType.Scanner:
                    Blockhit_Obj.SetActive(false);
                    break;
                case PlayerToolType.Building:
                    Blockhit_Obj.SetActive(false);
                    break;
            }
        }
        else
            this.GetComponent<CharacterController>().enabled = false;
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(Manager_Key.instance.runKey))
        {
            if (!BOOL_isRunning)
            {
                BOOL_isRunning = true;
                playerAudio.Stop();
            }
            FLOAT_MovementMultiplier = runSpeed;
        }
        else
        {
            if (BOOL_isRunning)
            {
                BOOL_isRunning = false;
                playerAudio.Stop();
            }
            FLOAT_MovementMultiplier = 1;
        }

        float horizInput = Input.GetAxis(Manager_Key.instance.horizontalInputName) * FLOAT_MovementSpeed * FLOAT_MovementMultiplier;
        float vertInput = Input.GetAxis(Manager_Key.instance.verticalInputName) * FLOAT_MovementSpeed * FLOAT_MovementMultiplier;

        if (horizInput != 0.0f || vertInput != 0.0f)
        {
            FLOAT_WalkTime += Time.deltaTime;

            if (FLOAT_WalkTime >= 0.25f)
            {
                if (FLOAT_MovementMultiplier == 1)
                {
                    if (!playerAudio.isPlaying)
                    {
                        playerAudio.clip = walkSound[Random.Range(0, walkSound.Length)];
                        playerAudio.Play();
                    }

                    Set_Hungry_value(-FLOAT_Hungry_Walk);
                }
                else if (FLOAT_MovementMultiplier == runSpeed)
                {
                    if (!playerAudio.isPlaying)
                    {
                        playerAudio.clip = runSound[Random.Range(0, runSound.Length)];
                        playerAudio.Play();
                    }
                    Set_Hungry_value(-FLOAT_Hungry_Run);
                }

                FLOAT_WalkTime = 0.0f;
            }
        }
        else if (playerAudio.isPlaying)
        {
            playerAudio.Stop();
        }

        if (playerAudio.isPlaying && BOOL_isJumping)
        {
            playerAudio.Stop();
        }

        Vector3 rightMovement = transform.right * horizInput;
        Vector3 forwardMovement = transform.forward * vertInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        JumpInput();
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(Manager_Key.instance.jumpKey) && !BOOL_isJumping)
        {
            Set_Hungry_value(-FLOAT_Hungry_Jump);
            BOOL_isJumping = true;
            StartCoroutine(JumpEvent());
            playerAudio.Stop();
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = JumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * JumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        BOOL_isJumping = false;
    }

    private void CheckHitBox()
    {
        RaycastHit Hit;
        ray = Camera.main.ScreenPointToRay(V3_ScreenRay);

        if (Physics.Raycast(ray, out Hit, 5.0f, 1 << 9))
        {
            V3_CheckHitPos = Hit.transform.position;

            if (V3_CheckHitPos.y <= 0) return;

            // Set Destory Block Position
            Blockhit_Obj.SetActive(true);
            if (Hit.collider.GetComponent<MeshFilter>())
            {
                Vector3 v3_Size = new Vector3(Hit.transform.localScale.x * 1.02f, Hit.transform.localScale.y * 1.02f, Hit.transform.localScale.z * 1.02f);
                Blockhit_Obj.transform.localScale = v3_Size;
                Blockhit_Obj.transform.rotation = Hit.transform.rotation;
            }
            Blockhit_Obj.transform.position = V3_CheckHitPos;

            // minning!
            MinningBlocks(V3_CheckHitPos, Hit);

            // creating!
            CreateBlocks(V3_CheckHitPos, Hit);
        }
        else
        {
            V3_CheckHitPos = Vector3.zero;
            Blockhit_Obj.SetActive(false);
        }
    }

    // Minning!
    public void MinningBlocks(Vector3 V3_BlockPos, RaycastHit Hit)
    {
        if (Input.GetKey(Manager_Key.instance.ScanTool_Key))
        {
            if (Hit.transform.tag == "CrashBox")
            {
                // Mining Block Time
                FLOAT_MiningTime += Time.deltaTime;

                if (!BOOL_isMinning)
                    BOOL_isMinning = true;

                if (FLOAT_MiningTime >= 0.5f)
                {
                    if (Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x, (int)V3_BlockPos.y, (int)V3_BlockPos.z, out worldChunks))
                    {
                        Enum_BlockType = worldChunks.GetBlockType((int)V3_BlockPos.x, (int)V3_BlockPos.y, (int)V3_BlockPos.z, MeshType.Block);
                        worldChunks.SetBlockType((int)V3_BlockPos.x, (int)V3_BlockPos.y, (int)V3_BlockPos.z, BlockType.Air);

                        // 아이템 생성
                        Manager_GAME.Get_Manager_Item_Script().Create_Items(Enum_BlockType, 1);

                        // 주변 청크 체크
                        CheckNeighbour(V3_BlockPos);

                        // 기준 위치 체크
                        StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(worldChunks));

                        // DissolveBlocks
                        Instantiate(Manager_GAME.Get_Manager_Block_Script().Get_DissolveBlocks(), Hit.transform.position, Hit.transform.rotation);
                    }

                    Set_Energy_value(-FLOAT_Energy_Tool);
                    BOOL_isMove         = true;
                    FLOAT_MiningTime    = 0.0f;

                    //Sound
                    Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.Charactor, (int)Sound_Charactor._break);
                }
            }
        }
        else
        {
            if (BOOL_isMinning)
                BOOL_isMinning = false;

            FLOAT_MiningTime = 0.0f;
        }
    }

    // Creating!
    public void CreateBlocks(Vector3 V3_BlockPos, RaycastHit Hit)
    {
        if (Vector3.Distance(this.transform.position, V3_BlockPos) <= 2)
            return;

        if (Input.GetKey(Manager_Key.instance.CreateBlock_Key))
        {
            if (Hit.transform.tag == "CrashBox")
            {
                // Mining Block Time
                FLOAT_CreateTime += Time.deltaTime;

                if (!BOOL_isCreating)
                    BOOL_isCreating = true;

                if (FLOAT_CreateTime >= 0.5f)
                {
                    if (Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x, (int)V3_BlockPos.y, (int)V3_BlockPos.z, out worldChunks))
                    {
                        // Set Position
                        Vector3Int V3I_NewBlockPos = Vector3Int.zero;

                        if      (Hit.point.x - V3_BlockPos.x == +0.5)   V3I_NewBlockPos.x = (int)V3_BlockPos.x + 1;
                        else if (Hit.point.x - V3_BlockPos.x == -0.5)   V3I_NewBlockPos.x = (int)V3_BlockPos.x - 1;
                        else                                            V3I_NewBlockPos.x = (int)V3_BlockPos.x + 0;

                        if      (Hit.point.y - V3_BlockPos.y == +0.5)   V3I_NewBlockPos.y = (int)V3_BlockPos.y + 1;
                        else if (Hit.point.y - V3_BlockPos.y == -0.5)   V3I_NewBlockPos.y = (int)V3_BlockPos.y - 1;
                        else                                            V3I_NewBlockPos.y = (int)V3_BlockPos.y + 0;

                        if      (Hit.point.z - V3_BlockPos.z == +0.5)   V3I_NewBlockPos.z = (int)V3_BlockPos.z + 1;
                        else if (Hit.point.z - V3_BlockPos.z == -0.5)   V3I_NewBlockPos.z = (int)V3_BlockPos.z - 1;
                        else                                            V3I_NewBlockPos.z = (int)V3_BlockPos.z + 0;

                        Enum_BlockType = worldChunks.GetBlockType((int)V3_BlockPos.x, (int)V3_BlockPos.y, (int)V3_BlockPos.z, MeshType.Block);

                        worldChunks.SetBlockType(V3I_NewBlockPos.x, V3I_NewBlockPos.y, V3I_NewBlockPos.z, Enum_BlockType);

                        // 아이템 제거
                        // Manager_GAME.Get_Manager_Item_Script().Create_Items(Enum_BlockType, 1);

                        // 주변 청크 체크
                        CheckNeighbour(V3I_NewBlockPos);

                        //// 기준 위치 체크
                        StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(worldChunks));
                    }

                    Set_Energy_value(-FLOAT_Energy_Tool);
                    BOOL_isMove         = true;
                    FLOAT_CreateTime    = 0.0f;

                    // Sound
                    Manager_GAME.Get_Manager_Sound_Script().Play_Sound(SoundType.Charactor, (int)Sound_Charactor.create);
                }
            }
        }
        else
        {
            if (BOOL_isCreating)
                BOOL_isCreating = false;

            FLOAT_CreateTime = 0.0f;
        }
    }

    public void CheckNeighbour(Vector3 V3_BlockPos)
    {
        WorldChunks anotherChunks;
        // X 좌표 체크
        Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x + 1, (int)V3_BlockPos.y + 0, (int)V3_BlockPos.z + 0, out anotherChunks);
        if (anotherChunks.V3I_Pos != worldChunks.V3I_Pos) StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(anotherChunks));
        Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x - 1, (int)V3_BlockPos.y - 0, (int)V3_BlockPos.z - 0, out anotherChunks);
        if (anotherChunks.V3I_Pos != worldChunks.V3I_Pos) StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(anotherChunks));

        // Y 좌표 체크
        Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x + 0, (int)V3_BlockPos.y + 1, (int)V3_BlockPos.z + 0, out anotherChunks);
        if (anotherChunks.V3I_Pos != worldChunks.V3I_Pos) StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(anotherChunks));
        Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x - 0, (int)V3_BlockPos.y - 1, (int)V3_BlockPos.z - 0, out anotherChunks);
        if (anotherChunks.V3I_Pos != worldChunks.V3I_Pos) StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(anotherChunks));

        // Z 좌표 체크
        Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x + 0, (int)V3_BlockPos.y + 0, (int)V3_BlockPos.z + 1, out anotherChunks);
        if (anotherChunks.V3I_Pos != worldChunks.V3I_Pos) StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(anotherChunks));
        Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_BlockPos.x - 0, (int)V3_BlockPos.y - 0, (int)V3_BlockPos.z - 1, out anotherChunks);
        if (anotherChunks.V3I_Pos != worldChunks.V3I_Pos) StartCoroutine(Manager_GAME.Get_Manager_World_Script().GenerateChunkMeshsAt(anotherChunks));
    }

    // Get Resources
    public BlockType Get_Resources_Type()           { return Enum_BlockType;    }

    // UI Enviroment
    public float    Get_HP_value()                  { return FLOAT_HP_value;        }
    public void     Set_HP_value(float value)       { FLOAT_HP_value += value;      }

    public float    Get_Hungry_value()              { return FLOAT_Hungry_value;    }
    public void     Set_Hungry_value(float value)   { FLOAT_Hungry_value += value;  }

    // UI Main
    public float    Get_Energy_value()              { return FLOAT_Energy_value;    }
    public void     Set_Energy_value(float value)   { FLOAT_Energy_value += value;  }

    public float    Get_Oxygen_value()              { return FLOAT_Oxygen_value;    }
    public void     Set_Oxygen_value(float value)   { FLOAT_Oxygen_value += value;  }
}
