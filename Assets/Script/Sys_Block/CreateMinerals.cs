using UnityEngine;
using System.Collections;

public static class CreateMinerals
{
    private static bool         Bool_Check;
    private static int          I_MineralCount;
    private static Vector3Int   V3_MineralPos;

    // 위치 설정
    private static void Init_Mineral_Position( Vector3 V3_BlockPos, BlockType[] Enum_BlockType, BlockType _type)
    {
        WorldChunks worldChunks;

        V3_MineralPos = Vector3Int.zero;
        V3_MineralPos.x = (int)V3_BlockPos.x + Random.Range(-1, 3);
        V3_MineralPos.y = (int)V3_BlockPos.y + Random.Range(-1, 3);
        V3_MineralPos.z = (int)V3_BlockPos.z + Random.Range(-1, 3);

        if (Manager_GAME.Get_Manager_World_Script().GetChunkAt(V3_MineralPos.x, V3_MineralPos.y, V3_MineralPos.z, out worldChunks))
        {
            if (worldChunks.GetBlockType(V3_MineralPos.x, V3_MineralPos.y, V3_MineralPos.z, MeshType.Block) != BlockType.Air &&
                worldChunks.GetBlockType(V3_MineralPos.x, V3_MineralPos.y, V3_MineralPos.z, MeshType.Block) != BlockType.Badrock)
            {
                worldChunks.SetBlockType(V3_MineralPos.x, V3_MineralPos.y, V3_MineralPos.z, _type);
            }
        }
    }

    // 산소 생성
    public static void Create_Oxygen(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 1 && V3_BlockPos.y <= 10 && Random.Range(0, 1000) < 4) Bool_Check = true;
        else if (V3_BlockPos.y >= 11 && V3_BlockPos.y <= 30 && Random.Range(0, 1000) < 3) Bool_Check = true;
        else if (V3_BlockPos.y >= 31 && Random.Range(0, 1000) < 2) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_MineralCount = Random.Range(1, 3);

            for (int i = 0; i < I_MineralCount; i++)
            {
                Init_Mineral_Position(V3_BlockPos, Enum_BlockType, BlockType.Oxygen);
            }
        }
    }

    // 애너지 생성
    public static void Create_Energy(Vector3 V3_BlockPos, BlockType[] Enum_BlockType)
    {
        // 확률 설정
        if (V3_BlockPos.y >= 1 && V3_BlockPos.y <= 10 && Random.Range(0, 1000) < 4) Bool_Check = true;
        else if (V3_BlockPos.y >= 11 && V3_BlockPos.y <= 30 && Random.Range(0, 1000) < 3) Bool_Check = true;
        else if (V3_BlockPos.y >= 31 && Random.Range(0, 1000) < 2) Bool_Check = true;
        else Bool_Check = false;

        // 생성
        if (Bool_Check)
        {
            // 뭉쳐나오는 갯수 설정
            I_MineralCount = Random.Range(1, 3);

            for (int i = 0; i < I_MineralCount; i++)
            {
                Init_Mineral_Position(V3_BlockPos, Enum_BlockType, BlockType.Energy);
            }
        }
    }
}