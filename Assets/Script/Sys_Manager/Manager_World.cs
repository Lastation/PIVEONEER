using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_World : MonoBehaviour
{
    [Header("텍스쳐 설정")]
    [SerializeField] private Texture        Tex_BlockTexture;
    [SerializeField] private string         Text_BlockTexturePath;

    [Header("블럭 텍스쳐")]
    [SerializeField] private Material[]     M_RenderMat;

    [Header("보이는 최대 청크 갯수")]
    [SerializeField] private int            I_RenderDistance;

    [Header("청크 크기 설정")]
    [SerializeField] private Vector3Int     V3I_ChunkSize;

    // 리스트 - 블럭 매쉬 생성 용도
    private List<WorldChunks>   LIST_WorldChunks_PrevChunks;

    // 큐 - 블럭 체크용도
    private Queue<WorldChunks>  QUEUE_WorldChunks_Create;

    Dictionary<Vector3Int, WorldChunks> D_WorldChunks;
    Dictionary<Vector3Int, WorldChunks> D_NowWorldChunks;

    private Vector3Int V3I_ChunkPos;

    private static Matrix4x4 Matrix4x4_Id;

    public bool     BOOL_INIT_MAP;
    public string   VALUE_MAPSEED;

    [Range(0, 100)]
    public int      I_Random_FillPercent;

    public void Init()
    {
        if (VALUE_MAPSEED == null)
            VALUE_MAPSEED = Time.time.ToString();

        D_WorldChunks                   = new Dictionary<Vector3Int, WorldChunks>();
        D_NowWorldChunks                = new Dictionary<Vector3Int, WorldChunks>();
        LIST_WorldChunks_PrevChunks     = new List<WorldChunks>();

        V3I_ChunkPos = Vector3Int.zero;

        Manager_Texture.Initialize(Text_BlockTexturePath, Tex_BlockTexture);
        Matrix4x4_Id = Matrix4x4.identity;

        // 지형 처음 생성시
        BOOL_INIT_MAP = false;
    }

    // Update is called once per frame
    public void Updated()
    {
        foreach (WorldChunks WorldChunk in D_WorldChunks.Values)
        {
            if (WorldChunk.c_meshBlock != null) Graphics.DrawMesh(WorldChunk.c_meshBlock, Matrix4x4_Id, M_RenderMat[0], 0);
        }
    }

    public IEnumerator MapBuild(int I_PosX, int I_PosY, int I_PosZ)
    {
        if (Manager_GAME.instance.Bool_Debug) yield return null;

        if (D_WorldChunks.Count > 0)    LIST_WorldChunks_PrevChunks = new List<WorldChunks>(D_WorldChunks.Values);

        // 빌드 디버그
        for (int i_y = 3; i_y >= -4; i_y--)
        {
            for (int i_x = -I_RenderDistance; i_x <= I_RenderDistance; i_x++)
            {
                for (int i_z = -I_RenderDistance; i_z <= I_RenderDistance; i_z++)
                {
                    V3I_ChunkPos.x = (I_PosX + i_x) * V3I_ChunkSize.x;
                    V3I_ChunkPos.y = (I_PosY + i_y) * V3I_ChunkSize.y;
                    V3I_ChunkPos.z = (I_PosZ + i_z) * V3I_ChunkSize.z;

                    float F_Distance = Vector3.Distance(new Vector3(Manager_GAME.Get_PlayerScript().transform.position.x, 0, Manager_GAME.Get_PlayerScript().transform.position.z),
                                                        new Vector3(V3I_ChunkPos.x, 0, V3I_ChunkPos.z));

                    if (F_Distance > I_RenderDistance * 16) continue;
                    if (V3I_ChunkPos.y > 256) continue; if (V3I_ChunkPos.y < 0) continue;

                    if (D_NowWorldChunks.ContainsKey(V3I_ChunkPos) && !D_WorldChunks.ContainsKey(V3I_ChunkPos))
                    {
                        D_WorldChunks.Add(D_NowWorldChunks[V3I_ChunkPos].V3I_Pos, D_NowWorldChunks[V3I_ChunkPos]);
                        continue;
                    }
                    else
                    {
                        CreateChunksAt(V3I_ChunkPos.x, V3I_ChunkPos.y, V3I_ChunkPos.z, true);
                    }
                }
            }
        }

        // 첫번째 생성시
        if (!BOOL_INIT_MAP)
        {
            foreach (WorldChunks worldChunks in D_WorldChunks.Values)
            {
                StartCoroutine(GenerateChunkMeshsAt(worldChunks));
            }
            BOOL_INIT_MAP = true;
        }
        else
        {
            // 두번째 이후 생성시
            StartCoroutine(GenerateChunkMeshsAt());
            StartCoroutine(DeleteChunkMeshsAt());
        }

        yield return null;
    }

    public IEnumerator MapBuild_OutSide(int I_PosX, int I_PosY, int I_PosZ)
    {
        for (int i_x = -I_RenderDistance; i_x <= I_RenderDistance; i_x++)
        {
            for (int i_z = -I_RenderDistance; i_z <= I_RenderDistance; i_z++)
            {
                for (int i_y = 0; i_y < 16; i_y++)
                {
                    V3I_ChunkPos.x = (I_PosX + i_x) * V3I_ChunkSize.x;
                    V3I_ChunkPos.y = (I_PosY + i_y) * V3I_ChunkSize.y;
                    V3I_ChunkPos.z = (I_PosZ + i_z) * V3I_ChunkSize.z;

                    if (!D_NowWorldChunks.ContainsKey(V3I_ChunkPos))
                    {
                        CreateChunksAt(V3I_ChunkPos.x, V3I_ChunkPos.y, V3I_ChunkPos.z, false);
                    }
                    yield return null;
                }
            }
        }
    }

    public IEnumerator GenerateChunkMeshsAt(WorldChunks worldChunks)
    {
        StartCoroutine(worldChunks.GenerateMesh());
        yield return new WaitUntil(() => worldChunks.Bool_Render);
    }

    public IEnumerator GenerateChunkMeshsAt()
    {
        List<WorldChunks>   LIST_WorldChunks_Create      = new List<WorldChunks>(D_WorldChunks.Values);
        Queue<WorldChunks>  QUEUE_WorldChunks_Create     = new Queue<WorldChunks>();

        for (int i = 0; i < LIST_WorldChunks_Create.Count; i++)
        {
            if (LIST_WorldChunks_PrevChunks.Contains(LIST_WorldChunks_Create[i]))   continue;
            if (!QUEUE_WorldChunks_Create.Contains(LIST_WorldChunks_Create[i]))     QUEUE_WorldChunks_Create.Enqueue(LIST_WorldChunks_Create[i]);
        }

        while (QUEUE_WorldChunks_Create.Count > 0)
        {
            WorldChunks worldChunks = QUEUE_WorldChunks_Create.Dequeue();

            StartCoroutine(worldChunks.GenerateMesh());
            yield return new WaitUntil(() => worldChunks.Bool_Render);
        }

        BOOL_INIT_MAP = true;
    }

    public IEnumerator DeleteChunkMeshsAt()
    {
        List<WorldChunks>   LIST_WorldChunks_Delete     = new List<WorldChunks>(D_WorldChunks.Values);
        Queue<WorldChunks>  QUEUE_WorldChunks_Delete    = new Queue<WorldChunks>();

        for (int i = 0; i < LIST_WorldChunks_Delete.Count; i++)
        {
            if (QUEUE_WorldChunks_Delete.Contains(LIST_WorldChunks_Delete[i])) continue;

            float F_Distance = Vector3.Distance(new Vector3(Manager_GAME.Get_PlayerScript().transform.position.x, 0, Manager_GAME.Get_PlayerScript().transform.position.z), 
                                                new Vector3(LIST_WorldChunks_Delete[i].V3I_Pos.x, 0, LIST_WorldChunks_Delete[i].V3I_Pos.z));

            if (F_Distance > I_RenderDistance * 16) QUEUE_WorldChunks_Delete.Enqueue(LIST_WorldChunks_Delete[i]);
        }

        while(QUEUE_WorldChunks_Delete.Count > 0)
        {
            WorldChunks worldChunks = QUEUE_WorldChunks_Delete.Dequeue();

            D_WorldChunks.Remove(worldChunks.V3I_Pos);
            yield return null;
        }
    }

    public bool CreateChunksAt(int x, int y, int z, bool RenderMode)
    {
        Vector3Int V3I_Pos = WorldCoordinatesToChunkCoordinates(x, y, z);

        if (D_WorldChunks.ContainsKey(V3I_Pos) == false)
        {
            WorldChunks worldChunks = new WorldChunks();
            worldChunks.Init(V3I_Pos);

            // 리스트에 청크 추가
            if (RenderMode) D_WorldChunks.Add(V3I_Pos, worldChunks);
            D_NowWorldChunks.Add(V3I_Pos, worldChunks);

            StartCoroutine(worldChunks.GenerateBlockArray());
            StartCoroutine(worldChunks.GenerateResources());

            worldChunks = null;

            return true;
        }

        return false;
    }

    // 청크 클래스 반환 - int 형
    public bool GetChunkAt(int i_x, int i_y, int i_z, out WorldChunks chunk)
    {
        Vector3Int key = WorldCoordinatesToChunkCoordinates(i_x, i_y, i_z);

        return D_WorldChunks.TryGetValue(key, out chunk);
    }

    // 청크 클래스 반환 - Vector 형
    public bool GetChunkAt(Vector3 v3_point, Vector3 v3_normal, out WorldChunks chunk)
    {
        Vector3Int key = WorldCoordinatesToChunkCoordinates(v3_point.x, v3_point.y, v3_point.z);

        return D_WorldChunks.TryGetValue(key, out chunk);
    }

    // 청크 위치 변환 - int 형
    public Vector3Int WorldCoordinatesToChunkCoordinates(int x, int y, int z)
    {
        return new Vector3Int
            (
                Mathf.FloorToInt(x / (float)V3I_ChunkSize.x) * V3I_ChunkSize.x,
                Mathf.FloorToInt(y / (float)V3I_ChunkSize.y) * V3I_ChunkSize.y,
                Mathf.FloorToInt(z / (float)V3I_ChunkSize.z) * V3I_ChunkSize.z
            );
    }

    // 청크 위치 변환 - float 형
    public Vector3Int WorldCoordinatesToChunkCoordinates(float x, float y, float z)
    {
        return new Vector3Int
            (
                Mathf.FloorToInt(x / (float)V3I_ChunkSize.x) * V3I_ChunkSize.x,
                Mathf.FloorToInt(y / (float)V3I_ChunkSize.y) * V3I_ChunkSize.y,
                Mathf.FloorToInt(z / (float)V3I_ChunkSize.z) * V3I_ChunkSize.z
            );
    }

    // 청크 크기 반환
    public Vector3Int Get_ChunkSize() { return V3I_ChunkSize; }
}