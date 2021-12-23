using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Texture
{
    public static Dictionary<string, Vector2[]> D_TextureMap = new Dictionary<string, Vector2[]>();

    public static void Initialize(string S_TexturePath, Texture texture)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(S_TexturePath);

        foreach (Sprite sprite in sprites)
        {
            Vector2[] V2_Uvs = new Vector2[4];

            V2_Uvs[0] = new Vector2(sprite.rect.xMin / texture.width, sprite.rect.yMin / texture.height);
            V2_Uvs[1] = new Vector2(sprite.rect.xMax / texture.width, sprite.rect.yMin / texture.height);
            V2_Uvs[2] = new Vector2(sprite.rect.xMin / texture.width, sprite.rect.yMax / texture.height);
            V2_Uvs[3] = new Vector2(sprite.rect.xMax / texture.width, sprite.rect.yMax / texture.height);

            D_TextureMap.Add(sprite.name, V2_Uvs);
        }
    }

    public static bool AddTextures(BlockType Enum_BlockType, Direction Enum_Direction, int I_Index, Vector2[] V2_Uvs)
    {
        string key = Enum_BlockType.ToString();
        Vector2[] V2_Text;
        if (D_TextureMap.TryGetValue(key, out V2_Text))
        {
            V2_Uvs[I_Index + 0] = V2_Text[0];
            V2_Uvs[I_Index + 1] = V2_Text[1];
            V2_Uvs[I_Index + 2] = V2_Text[2];
            V2_Uvs[I_Index + 3] = V2_Text[3];
            return true;
        }

        V2_Text = D_TextureMap["default"];

        V2_Uvs[I_Index + 0] = V2_Text[0];
        V2_Uvs[I_Index + 1] = V2_Text[1];
        V2_Uvs[I_Index + 2] = V2_Text[2];
        V2_Uvs[I_Index + 3] = V2_Text[3];
        return false;
    }
}