using UnityEngine;
using System.Collections;

public static class CreateOres
{
    private static bool         Bool_Check;
    private static int          I_OreCount;
    private static Vector3Int   V3_OrePos;

    // 위치 설정
    private static void Init_Ore_Position(Vector3 V3_BlockPos, BlockType[] Enum_BlockType, BlockType _type)
    {
        WorldChunks worldChunks;
        V3_OrePos = Vector3Int.zero;

        V3_OrePos.x = (int)V3_BlockPos.x + Random.Range(-1, 3);
        V3_OrePos.y = (int)V3_BlockPos.y + Random.Range(-1, 3);
        V3_OrePos.z = (int)V3_BlockPos.z + Random.Range(-1, 3);

        if (Manager_GAME.Get_Manager_World_Script().GetChunkAt(V3_OrePos.x, V3_OrePos.y, V3_OrePos.z, out worldChunks))
        {
            if (worldChunks.GetBlockType(V3_OrePos.x, V3_OrePos.y, V3_OrePos.z, MeshType.Block) != BlockType.Air &&
                worldChunks.GetBlockType(V3_OrePos.x, V3_OrePos.y, V3_OrePos.z, MeshType.Block) != BlockType.Badrock)
            {
                worldChunks.SetBlockType(V3_OrePos.x, V3_OrePos.y, V3_OrePos.z, _type);
            }
        }
    }

    // 리튬 생성
    public static void Create_Lithium(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 1 && V3_BlockPos.y <= 10 && Random.Range(0, 1000) < 4)         Bool_Check = true;
        else if (V3_BlockPos.y >= 20 && V3_BlockPos.y <= 35 && Random.Range(0, 1000) < 5)   Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 6);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Lithium);
            }
        }
    }

    // 철광석 생성
    public static void Create_Ironstone(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 21 && V3_BlockPos.y <= 30 && Random.Range(0, 1000) < 4) Bool_Check = true;
        else if (V3_BlockPos.y >= 31 && V3_BlockPos.y <= 39 && Random.Range(0, 1000) < 2) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 4);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.IronStone);
            }
        }
    }

    // 황토석 생성
    public static void Create_Chalcopyrite(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 21 && V3_BlockPos.y <= 30 && Random.Range(0, 1000) < 4) Bool_Check = true;
        else if (V3_BlockPos.y >= 31 && V3_BlockPos.y <= 39 && Random.Range(0, 1000) < 2) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 4);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Chalcopyrite);
            }
        }
    }

    // 석영 생성
    public static void Create_Quartz(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 35 && V3_BlockPos.y <= 40 && Random.Range(0, 1000) < 4) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 6);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Quartz);
            }
        }
    }

    // 보크사이트 생성
    public static void Create_Bauxite(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 10 && V3_BlockPos.y <= 25 && Random.Range(0, 1000) < 5) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 4);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Bauxite);
            }
        }
    }

    // 티타나이트 생성
    public static void Create_Titanite(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 1 && V3_BlockPos.y <= 15 && Random.Range(0, 1000) < 5) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 4);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Titanite);
            }
        }
    }

    // 그라파이트 생성
    public static void Create_Graphite(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 1 && V3_BlockPos.y <= 10 && Random.Range(0, 1000) < 4) Bool_Check = true;
        else if (V3_BlockPos.y >= 11 && V3_BlockPos.y <= 35 && Random.Range(0, 1000) < 3) Bool_Check = true;
        else
            Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 3);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Graphite);
            }
        }
    }

    // 다이아몬드 생성
    public static void Create_Diamond(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 1 && V3_BlockPos.y <= 3 && Random.Range(0, 1000) < 3) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_OreCount = Random.Range(1, 3);

            for (int i = 0; i < I_OreCount; i++)
            {
                Init_Ore_Position(V3_BlockPos, Enum_BlockType, BlockType.Diamond);
            }
        }
    }
}
