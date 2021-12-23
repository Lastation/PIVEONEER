using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshBuilder : ThreadedProcess
{
    Vector3Int      V3I_Pos;
    BlockType[]     Enum_BlocksType;
    MeshType        Enum_MeshType;

    Vector3[]       V3_Vertices;
    Vector2[]       V2_Uvs;
    int[]           I_Triangles;
    private int     I_VertexIndex, I_TriangleIndex;

    private int     I_SizeEstimate;
    private bool    Bool_IsVisible;
    private byte[]  Byte_Faces;

    WorldChunks[] worldChunks_neighbors;

    public MeshBuilder(Vector3Int V3I_Pos, BlockType[] Enum_BlocksType, MeshType Enum_MeshType)
    {
        this.V3I_Pos            = V3I_Pos;
        this.Enum_BlocksType    = Enum_BlocksType;
        this.Enum_MeshType      = Enum_MeshType;

        Byte_Faces = new byte[Manager_GAME.Get_Manager_World_Script().Get_ChunkSize().x * Manager_GAME.Get_Manager_World_Script().Get_ChunkSize().y * Manager_GAME.Get_Manager_World_Script().Get_ChunkSize().z];
        worldChunks_neighbors = new WorldChunks[6];
    }

    public override void ThreadFunction()
    {
        //Generate faces
        int I_Index = 0;
        Manager_World worldBuilder = Manager_GAME.Get_Manager_World_Script();
        Vector3Int V3I_ChunkSize = worldBuilder.Get_ChunkSize();

        bool[] Bool_Exists = new bool[6];

        Bool_Exists[0] = worldBuilder.GetChunkAt(V3I_Pos.x, V3I_Pos.y, V3I_Pos.z + V3I_ChunkSize.z, out worldChunks_neighbors[0]);
        Bool_Exists[1] = worldBuilder.GetChunkAt(V3I_Pos.x + V3I_ChunkSize.x, V3I_Pos.y, V3I_Pos.z, out worldChunks_neighbors[1]);
        Bool_Exists[2] = worldBuilder.GetChunkAt(V3I_Pos.x, V3I_Pos.y, V3I_Pos.z - V3I_ChunkSize.z, out worldChunks_neighbors[2]);
        Bool_Exists[3] = worldBuilder.GetChunkAt(V3I_Pos.x - V3I_ChunkSize.x, V3I_Pos.y, V3I_Pos.z, out worldChunks_neighbors[3]);

        Bool_Exists[4] = worldBuilder.GetChunkAt(V3I_Pos.x, V3I_Pos.y + V3I_ChunkSize.y, V3I_Pos.z, out worldChunks_neighbors[4]);
        Bool_Exists[5] = worldBuilder.GetChunkAt(V3I_Pos.x, V3I_Pos.y - V3I_ChunkSize.y, V3I_Pos.z, out worldChunks_neighbors[5]);

        for (int x = 0; x < V3I_ChunkSize.x; ++x)
        {
            for (int y = 0; y < V3I_ChunkSize.y; ++y)
            {
                for (int z = 0; z < V3I_ChunkSize.z; ++z)
                {
                    if (Enum_BlocksType[I_Index] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] = 0;
                        I_Index++;
                        continue;
                    }

                    if (z == 0 && (Bool_Exists[2] == false || worldChunks_neighbors[2].GetBlockType(V3I_Pos.x + x + 0, V3I_Pos.y + y + 0, V3I_Pos.z + z - 1, Enum_MeshType) == BlockType.Air))
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.South;
                        I_SizeEstimate += 4;
                    }
                    if (z > 0 && Enum_BlocksType[I_Index - 1] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.South;
                        I_SizeEstimate += 4;
                    }

                    if (z == V3I_ChunkSize.z - 1 && (Bool_Exists[0] == false || worldChunks_neighbors[0].GetBlockType(V3I_Pos.x + x + 0, V3I_Pos.y + y + 0, V3I_Pos.z + z + 1, Enum_MeshType) == BlockType.Air))
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.North;
                        I_SizeEstimate += 4;
                    }
                    if (z < V3I_ChunkSize.z - 1 && Enum_BlocksType[I_Index + 1] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.North;
                        I_SizeEstimate += 4;
                    }

                    if (y == 0 && (Bool_Exists[5] == false || worldChunks_neighbors[5].GetBlockType(V3I_Pos.x + x + 0, V3I_Pos.y + y - 1, V3I_Pos.z + z + 0, Enum_MeshType) == BlockType.Air))
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.Down;
                        I_SizeEstimate += 4;
                    }
                    if (y > 0 && Enum_BlocksType[I_Index - V3I_ChunkSize.z] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.Down;
                        I_SizeEstimate += 4;
                    }

                    if (y == V3I_ChunkSize.y - 1 && (Bool_Exists[4] == false || worldChunks_neighbors[4].GetBlockType(V3I_Pos.x + x + 0, V3I_Pos.y + y + 1, V3I_Pos.z + z + 0, Enum_MeshType) == BlockType.Air))
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.Up;
                        I_SizeEstimate += 4;
                    }
                    if (y < V3I_ChunkSize.y - 1 && Enum_BlocksType[I_Index + V3I_ChunkSize.z] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.Up;
                        I_SizeEstimate += 4;
                    }

                    if (x == 0 && (Bool_Exists[3] == false || worldChunks_neighbors[3].GetBlockType(V3I_Pos.x + x - 1, V3I_Pos.y + y + 0, V3I_Pos.z + z + 0, Enum_MeshType) == BlockType.Air))
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.West;
                        I_SizeEstimate += 4;
                    }
                    if (x > 0 && Enum_BlocksType[I_Index - V3I_ChunkSize.z * V3I_ChunkSize.y] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.West;
                        I_SizeEstimate += 4;
                    }

                    if (x == V3I_ChunkSize.x - 1 && (Bool_Exists[1] == false || worldChunks_neighbors[1].GetBlockType(V3I_Pos.x + x + 1, V3I_Pos.y + y + 0, V3I_Pos.z + z + 0, Enum_MeshType) == BlockType.Air))
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.East;
                        I_SizeEstimate += 4;
                    }
                    if (x < V3I_ChunkSize.x - 1 && Enum_BlocksType[I_Index + V3I_ChunkSize.z * V3I_ChunkSize.y] == BlockType.Air)
                    {
                        Byte_Faces[I_Index] |= (byte)Direction.East;
                        I_SizeEstimate += 4;
                    }

                    Bool_IsVisible = true;
                    I_Index++;
                }
            }
        }

        I_Index = 0;
        V3_Vertices = new Vector3[I_SizeEstimate];
        V2_Uvs      = new Vector2[I_SizeEstimate];
        I_Triangles = new int[(int)(I_SizeEstimate * 1.5)];

        for (int x = 0; x < V3I_ChunkSize.x; ++x)
        {
            for (int y = 0; y < V3I_ChunkSize.y; ++y)
            {
                for (int z = 0; z < V3I_ChunkSize.z; ++z)
                {
                    Vector3Int v3i_offset = new Vector3Int(x + V3I_Pos.x, y + V3I_Pos.y, z + V3I_Pos.z);
                    CubeMesh.AppendMesh(Enum_BlocksType[I_Index], Byte_Faces[I_Index], V3_Vertices, V2_Uvs, I_Triangles, ref I_VertexIndex, ref I_TriangleIndex, v3i_offset);

                    I_Index++;
                }
            }
        }
    }

    public Mesh GetMeshData(ref Mesh Mesh_Copy)
    {
        if (Mesh_Copy == null)
        {
            Mesh_Copy = new Mesh();
            Mesh_Copy.MarkDynamic();
        }
        else
            Mesh_Copy.Clear();

        if (Bool_IsVisible == false || I_VertexIndex == 0)
            return Mesh_Copy;

        if (I_VertexIndex > 65535)
            Mesh_Copy.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        Mesh_Copy.vertices      = V3_Vertices;
        Mesh_Copy.uv            = V2_Uvs;
        Mesh_Copy.triangles     = I_Triangles;

        Mesh_Copy.RecalculateNormals();

        return Mesh_Copy;
    }
}
