using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BiomeDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject OBJ_PlayerSight;
    public enum DisplayMode { Noise, Biomes }
    public DisplayMode mode;

    public RawImage img_minimap;
    public Noise noise;

    private Texture2D texture;
    private Color[] pixels;

    public  bool    recalculate;
    private float   FLOAT_UpdateMap;

    public void Init()
    {
        texture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        pixels = new Color[256 * 256];

        noise = new Noise();
    }

    public void Update_Sight()
    {
        Quaternion V3_Rotation = Manager_GAME.Get_PlayerScript().transform.rotation;
        V3_Rotation.x = 0.0f;
        V3_Rotation.z = -V3_Rotation.y;
        V3_Rotation.y = 0.0f;

        OBJ_PlayerSight.transform.rotation = V3_Rotation;
    }

    public IEnumerator Updated()
    {
        if (img_minimap == null && !recalculate)
            yield return null;
        else
        {
            if (FLOAT_UpdateMap > 0.0f)
                FLOAT_UpdateMap -= Time.fixedDeltaTime;
            else
            {
                if (mode == DisplayMode.Noise)
                    RenderNoise();
                if (mode == DisplayMode.Biomes)
                    RenderBiomes();

                recalculate = false;
                FLOAT_UpdateMap = 0.5f;
            }
        }

        yield return null;
    }

    void RenderNoise()
    {
        float[,] noisevalues = noise.GetNoiseValues(256);

        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 256; y++)
            {
                pixels[x * 256 + y] = Color.Lerp(Color.black, Color.white, noisevalues[x, y]);
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        img_minimap.texture = texture;
    }

    public void RenderBiomes()
    {
        float[,] noisevalues = noise.GetNoiseValues(256);

        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 256; y++)
            {
                pixels[x * 256 + y] = BiomeExtensions.GetBiome(noisevalues[x, y]).GetColor();
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();

        img_minimap.texture = texture;
    }
}