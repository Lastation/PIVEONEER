using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_CrashCheck : MonoBehaviour
{
    Vector3 v3_Position;

    BoxCollider[] g_Colliders;

    public void CreateColliderPool(int I_PosX, int I_PosY, int I_PosZ)
    {     
        int I_Index = 0;

        g_Colliders = new BoxCollider[(I_PosX * 2 + 1) * (I_PosY * 2 + 1) * (I_PosZ * 2 + 1)];

        for (int x = -I_PosX; x <= I_PosX; x++)
        {
            for (int y = -I_PosY; y <= I_PosY; y++)
            {
                for (int z = -I_PosZ; z <= I_PosZ; z++)
                {
                    GameObject g_CrashBox   = new GameObject();
                    g_Colliders[I_Index]    = g_CrashBox.AddComponent<BoxCollider>();
                    g_CrashBox.name         = "CrashBox" + I_Index.ToString("000");
                    g_CrashBox.tag          = "CrashBox";
                    g_CrashBox.layer        = 9; // CrashLayer;
                    g_CrashBox.transform.SetParent(this.transform);

                    I_Index++;
                }
            }
        }
    }

    public IEnumerator SetCollidersAtChunk(int I_PosX, int I_PosY, int I_PosZ, Vector3 V3_Pos)
    {
        WorldChunks worldChunks = null;

        yield return new WaitUntil(() => Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_Pos.x, (int)V3_Pos.y, (int)V3_Pos.z, out worldChunks));

        yield return new WaitUntil(() => worldChunks.Bool_Render);

        int I_Index = 0;

        for (int x = -I_PosX; x <= I_PosX; x++)
        {
            for (int y = -I_PosY; y <= I_PosY; y++)
            {
                for (int z = -I_PosZ; z <= I_PosZ; z++)
                {
                    v3_Position.x = (int)V3_Pos.x + x + 0.5f;
                    v3_Position.y = (int)V3_Pos.y + y + 0.5f;
                    v3_Position.z = (int)V3_Pos.z + z + 0.5f;

                    g_Colliders[I_Index].transform.position = v3_Position;

                    if (Manager_GAME.Get_Manager_World_Script().GetChunkAt((int)V3_Pos.x + x, (int)V3_Pos.y + y, (int)V3_Pos.z + z, out worldChunks))
                    {
                        if (worldChunks.GetBlockType((int)V3_Pos.x + x, (int)V3_Pos.y + y, (int)V3_Pos.z + z, (int)MeshType.Block) == BlockType.Air)
                        {
                            g_Colliders[I_Index].gameObject.SetActive(false);
                        }
                        else
                            g_Colliders[I_Index].gameObject.SetActive(true);
                    }
                    I_Index++;
                }
            }
        }
    }
}
