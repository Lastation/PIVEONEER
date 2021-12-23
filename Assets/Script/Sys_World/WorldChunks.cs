using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldChunks
{
    public  Vector3Int  V3I_Pos;
    public  bool        Bool_Render;
    private Vector3Int  V3I_ChunkSize;

    private BlockType[] Enum_Blocks;

    MeshBuilder     meshBuilder_blocks;

    public Mesh c_meshBlock;

    public void Init(Vector3Int v_pos)
    {
        V3I_Pos = v_pos;

        // 청크 배열 크기 설정
        V3I_ChunkSize   = Manager_GAME.Get_Manager_World_Script().Get_ChunkSize();
        Enum_Blocks     = new BlockType[V3I_ChunkSize.x * V3I_ChunkSize.y * V3I_ChunkSize.z];
    }

    public IEnumerator GenerateBlockArray()
    {
        int i_index = 0;
        int I_TerrainValue;
        int I_CaveValue_Min, I_CaveValue_Max;

        // 블럭 생성
        for (int x = 0; x < V3I_ChunkSize.x; ++x)
        {
            for (int y = 0; y < V3I_ChunkSize.y; ++y)
            {
                for (int z = 0; z < V3I_ChunkSize.z; ++z)
                {
                    Enum_Blocks[i_index] = BlockType.Air;

                    I_TerrainValue  = Mathf.CeilToInt(Mathf.PerlinNoise((x + V3I_Pos.x) / 32.0f,        (z + V3I_Pos.z) / 32.0f) * 25.0f + 44.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x) / 64.0f,        (z + V3I_Pos.z + 84) / 64.0f) * 27.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x - 612) / 16.0f,  (z + V3I_Pos.z) / 16.0f) * 5.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x) / 4.0f,         (z + V3I_Pos.z) / 4.0f + 64) +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x + 8) / 24.0f,    (z + V3I_Pos.z) / 24.0f - 8) * 12.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x + 80) / 64.0f,   (z + V3I_Pos.z) / 64.0f - 80) * 40.0f);

                    I_CaveValue_Min = Mathf.CeilToInt(Mathf.PerlinNoise((x + V3I_Pos.x) / 64.0f, (z + V3I_Pos.z) / 64.0f) * 25.0f + 32.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x) / 128.0f, (z + V3I_Pos.z + 84) / 128.0f) * 40.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x - 256) / 16.0f, (z + V3I_Pos.z) / 16.0f) * 32.0f);

                    I_CaveValue_Max = Mathf.CeilToInt(Mathf.PerlinNoise((x + V3I_Pos.x) / 16.0f, (z + V3I_Pos.z) / 16.0f) * 32.0f + 52.0f +
                                                      Mathf.PerlinNoise((x + V3I_Pos.x + 80) / 64.0f,   (z + V3I_Pos.z) / 64.0f - 80) * 15.0f);

                    if (y + V3I_Pos.y > I_TerrainValue - 8  && y + V3I_Pos.y <= I_TerrainValue)         Enum_Blocks[i_index] = BlockType.Flat_Island_00;
                    if (y + V3I_Pos.y > I_TerrainValue - 24 && y + V3I_Pos.y <= I_TerrainValue - 8)     Enum_Blocks[i_index] = BlockType.Flat_Island_01;
                    if (y + V3I_Pos.y > I_TerrainValue - 48 && y + V3I_Pos.y <= I_TerrainValue - 24)    Enum_Blocks[i_index] = BlockType.Flat_Island_02;
                    if (y + V3I_Pos.y <= I_TerrainValue - 48)                                           Enum_Blocks[i_index] = BlockType.Flat_Island_03;
                    if (y + V3I_Pos.y < 3)                                                              Enum_Blocks[i_index] = BlockType.Badrock;

                    // Create Caves
                    if (y + V3I_Pos.y > I_CaveValue_Min && y + V3I_Pos.y < I_CaveValue_Max)             Enum_Blocks[i_index] = BlockType.Air;

                    i_index++;
                }
            }
        }

        yield return null;
    }

    public IEnumerator GenerateResources()
    {
        // 자원 생성
        for (int x = 0; x < V3I_ChunkSize.x; ++x)
        {
            for (int y = 0; y < V3I_ChunkSize.y; ++y)
            {
                for (int z = 0; z < V3I_ChunkSize.z; ++z)
                {
                    // 위치값 설정
                    Vector3 V3_BlockPos = new Vector3(x + V3I_Pos.x, y + V3I_Pos.y, z + V3I_Pos.z);

                    // 미네랄 생성
                    CreateMinerals.Create_Energy    (V3_BlockPos, Enum_Blocks);      // 애너지
                    CreateMinerals.Create_Oxygen    (V3_BlockPos, Enum_Blocks);      // 산소

                    // 광물 생성
                    CreateOres.Create_Lithium       (V3_BlockPos, Enum_Blocks);      // 리튬
                    CreateOres.Create_Ironstone     (V3_BlockPos, Enum_Blocks);      // 철광석
                    CreateOres.Create_Chalcopyrite  (V3_BlockPos, Enum_Blocks);      // 황동석
                    CreateOres.Create_Quartz        (V3_BlockPos, Enum_Blocks);      // 석영
                    CreateOres.Create_Bauxite       (V3_BlockPos, Enum_Blocks);      // 보크사이트
                    CreateOres.Create_Titanite      (V3_BlockPos, Enum_Blocks);      // 티타나이트
                    CreateOres.Create_Graphite      (V3_BlockPos, Enum_Blocks);      // 그라파이트
                    CreateOres.Create_Diamond       (V3_BlockPos, Enum_Blocks);      // 다이아몬드
                }
            }
        }

        yield return null;
    }

    public BlockType GetBlockType(int x, int y, int z, MeshType i_meshType)
    {
        x -= V3I_Pos.x;
        y -= V3I_Pos.y;
        z -= V3I_Pos.z;

        if (IsPointWithinBounds(x, y, z))
        {
            switch (i_meshType)
            {
                case MeshType.Block:
                    return Enum_Blocks[x * V3I_ChunkSize.y * V3I_ChunkSize.z + y * V3I_ChunkSize.z + z];
            }
        }

        return BlockType.Air;
    }

    public bool SetBlockType(int i_x, int i_y, int i_z, BlockType e_blockType)
    {
        i_x -= V3I_Pos.x;
        i_y -= V3I_Pos.y;
        i_z -= V3I_Pos.z;

        if (IsPointWithinBounds(i_x, i_y, i_z))
        {
            Enum_Blocks[i_x * V3I_ChunkSize.y * V3I_ChunkSize.z + i_y * V3I_ChunkSize.z + i_z] = e_blockType;
            return false;
        }
        return true;
    }

    public IEnumerator GenerateMesh()
    {
        meshBuilder_blocks = new MeshBuilder(V3I_Pos, Enum_Blocks, MeshType.Block);

        meshBuilder_blocks.Start();

        yield return new WaitUntil(() => meshBuilder_blocks.Update());
        c_meshBlock   = meshBuilder_blocks.GetMeshData(ref c_meshBlock);
       
        Bool_Render = true;
        meshBuilder_blocks = null;
    }

    public bool IsPointWithinBounds(int x, int y, int z)
    {
        return x >= 0 && y >= 0 && z >= 0 && z < V3I_ChunkSize.z && y < V3I_ChunkSize.y && x < V3I_ChunkSize.x;
    }
}