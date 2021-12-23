using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public static class CubeMesh
{
    public static Vector3[] V3_Normals = new Vector3[6]
    {
        Vector3.forward,
        Vector3.right,
        Vector3.back,
        Vector3.left,
        Vector3.up,
        Vector3.down
    };

    public static Vector4[] V4_Tangents = new Vector4[3]
    {
        new Vector4 (-1.0f, 0.0f, 0.0f, -1.0f),
        new Vector4 (0.0f, 0.0f, 1.0f, -1.0f),
        new Vector4 (0.0f, 0.0f, -1.0f, -1.0f)
    };

    public static void AppendMesh(BlockType Enum_BlockType, byte Byte_FaceData, Vector3[] V3_Vertices, Vector2[] V2_Uvs, int[] I_Triangles, ref int I_VertexIndex, ref int I_TriangleIndex, Vector3Int v3i_offset)
    {
        if ((Byte_FaceData & (byte)Direction.North) != 0)
        {
            V3_Vertices[I_VertexIndex + 0] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 0, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 1] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 0, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 2] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 1, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 3] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 1, v3i_offset.z + 1);

            I_Triangles[I_TriangleIndex + 0] = I_VertexIndex + 1;
            I_Triangles[I_TriangleIndex + 1] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 2] = I_VertexIndex + 0;

            I_Triangles[I_TriangleIndex + 3] = I_VertexIndex + 1;
            I_Triangles[I_TriangleIndex + 4] = I_VertexIndex + 3;
            I_Triangles[I_TriangleIndex + 5] = I_VertexIndex + 2;

            V2_Uvs[I_VertexIndex + 0] = Vector2.zero;
            V2_Uvs[I_VertexIndex + 1] = Vector2.right;
            V2_Uvs[I_VertexIndex + 2] = Vector2.up;
            V2_Uvs[I_VertexIndex + 3] = Vector2.one;

            Manager_Texture.AddTextures(Enum_BlockType, Direction.North, I_VertexIndex, V2_Uvs);

            I_VertexIndex += 4;
            I_TriangleIndex += 6;
        }

        if ((Byte_FaceData & (byte)Direction.East) != 0)
        {
            V3_Vertices[I_VertexIndex + 0] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 0, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 1] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 0, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 2] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 1, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 3] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 1, v3i_offset.z + 1);

            I_Triangles[I_TriangleIndex + 0] = I_VertexIndex + 0;
            I_Triangles[I_TriangleIndex + 1] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 2] = I_VertexIndex + 1;

            I_Triangles[I_TriangleIndex + 3] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 4] = I_VertexIndex + 3;
            I_Triangles[I_TriangleIndex + 5] = I_VertexIndex + 1;

            V2_Uvs[I_VertexIndex + 0] = Vector2.zero;
            V2_Uvs[I_VertexIndex + 1] = Vector2.right;
            V2_Uvs[I_VertexIndex + 2] = Vector2.up;
            V2_Uvs[I_VertexIndex + 3] = Vector2.one;

            Manager_Texture.AddTextures(Enum_BlockType, Direction.East, I_VertexIndex, V2_Uvs);

            I_VertexIndex += 4;
            I_TriangleIndex += 6;
        }

        if ((Byte_FaceData & (byte)Direction.South) != 0)
        {
            V3_Vertices[I_VertexIndex + 0] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 0, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 1] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 0, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 2] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 1, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 3] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 1, v3i_offset.z + 0);

            I_Triangles[I_TriangleIndex + 0] = I_VertexIndex + 0;
            I_Triangles[I_TriangleIndex + 1] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 2] = I_VertexIndex + 1;

            I_Triangles[I_TriangleIndex + 3] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 4] = I_VertexIndex + 3;
            I_Triangles[I_TriangleIndex + 5] = I_VertexIndex + 1;

            V2_Uvs[I_VertexIndex + 0] = Vector2.zero;
            V2_Uvs[I_VertexIndex + 1] = Vector2.right;
            V2_Uvs[I_VertexIndex + 2] = Vector2.up;
            V2_Uvs[I_VertexIndex + 3] = Vector2.one;

            Manager_Texture.AddTextures(Enum_BlockType, Direction.South, I_VertexIndex, V2_Uvs);

            I_VertexIndex += 4;
            I_TriangleIndex += 6;
        }

        if ((Byte_FaceData & (byte)Direction.West) != 0)
        {
            V3_Vertices[I_VertexIndex + 0] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 0, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 2] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 1, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 1] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 0, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 3] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 1, v3i_offset.z + 1);

            I_Triangles[I_TriangleIndex + 0] = I_VertexIndex + 1;
            I_Triangles[I_TriangleIndex + 1] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 2] = I_VertexIndex + 0;

            I_Triangles[I_TriangleIndex + 3] = I_VertexIndex + 1;
            I_Triangles[I_TriangleIndex + 4] = I_VertexIndex + 3;
            I_Triangles[I_TriangleIndex + 5] = I_VertexIndex + 2;

            V2_Uvs[I_VertexIndex + 0] = Vector2.zero;
            V2_Uvs[I_VertexIndex + 1] = Vector2.right;
            V2_Uvs[I_VertexIndex + 2] = Vector2.up;
            V2_Uvs[I_VertexIndex + 3] = Vector2.one;

            Manager_Texture.AddTextures(Enum_BlockType, Direction.West, I_VertexIndex, V2_Uvs);

            I_VertexIndex += 4;
            I_TriangleIndex += 6;
        }

        if ((Byte_FaceData & (byte)Direction.Up) != 0)
        {
            V3_Vertices[I_VertexIndex + 0] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 1, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 1] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 1, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 2] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 1, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 3] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 1, v3i_offset.z + 1);

            I_Triangles[I_TriangleIndex + 0] = I_VertexIndex + 0;
            I_Triangles[I_TriangleIndex + 1] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 2] = I_VertexIndex + 1;

            I_Triangles[I_TriangleIndex + 3] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 4] = I_VertexIndex + 3;
            I_Triangles[I_TriangleIndex + 5] = I_VertexIndex + 1;

            V2_Uvs[I_VertexIndex + 0] = Vector2.zero;
            V2_Uvs[I_VertexIndex + 1] = Vector2.right;
            V2_Uvs[I_VertexIndex + 2] = Vector2.up;
            V2_Uvs[I_VertexIndex + 3] = Vector2.one;

            Manager_Texture.AddTextures(Enum_BlockType, Direction.Up, I_VertexIndex, V2_Uvs);

            I_VertexIndex += 4;
            I_TriangleIndex += 6;
        }

        if ((Byte_FaceData & (byte)Direction.Down) != 0)
        {
            V3_Vertices[I_VertexIndex + 0] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 0, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 1] = new Vector3(v3i_offset.x + 0, v3i_offset.y + 0, v3i_offset.z + 1);
            V3_Vertices[I_VertexIndex + 2] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 0, v3i_offset.z + 0);
            V3_Vertices[I_VertexIndex + 3] = new Vector3(v3i_offset.x + 1, v3i_offset.y + 0, v3i_offset.z + 1);

            I_Triangles[I_TriangleIndex + 0] = I_VertexIndex + 0;
            I_Triangles[I_TriangleIndex + 1] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 2] = I_VertexIndex + 1;

            I_Triangles[I_TriangleIndex + 3] = I_VertexIndex + 2;
            I_Triangles[I_TriangleIndex + 4] = I_VertexIndex + 3;
            I_Triangles[I_TriangleIndex + 5] = I_VertexIndex + 1;

            V2_Uvs[I_VertexIndex + 0] = Vector2.zero;
            V2_Uvs[I_VertexIndex + 1] = Vector2.right;
            V2_Uvs[I_VertexIndex + 2] = Vector2.up;
            V2_Uvs[I_VertexIndex + 3] = Vector2.one;

            Manager_Texture.AddTextures(Enum_BlockType, Direction.Down, I_VertexIndex, V2_Uvs);

            I_VertexIndex += 4;
            I_TriangleIndex += 6;
        }

        //Quaternion q = Quaternion.Euler(180.0f, 0.0f, 0.0f);
        //Vector3 pivot = new Vector3(0.5f + v3i_offset.x, 0.5f + v3i_offset.y, 0.5f + v3i_offset.z);

        //for (int i = 0; i < 24; i++)
        //{
        //    V3_Vertices[i] = (q * (V3_Vertices[i] - pivot)) + pivot;
        //}
    }
}