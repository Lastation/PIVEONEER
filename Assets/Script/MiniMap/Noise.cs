using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Noise
{
    public float min, max;

    public float[] amplitudes;
    public float[] frequencies;

    private float f_xCoord, f_zCoord;

    public float[,] GetNoiseValues(int size)
    {
        float[,] values = new float[size, size];

        for (int i_x = 0; i_x < size; i_x++)
        {
            for (int i_z = 0; i_z < size; i_z++)
            {
                Vector3 V3_PlayerPos = Manager_GAME.Get_PlayerScript().transform.position - new Vector3(128.0f, 0.0f, 128.0f);

                int I_TerrainValue = Mathf.CeilToInt(Mathf.PerlinNoise((i_x + V3_PlayerPos.x) / 32.0f,       (i_z + V3_PlayerPos.z) / 32.0f) * 25.0f + 44.0f +
                                                     Mathf.PerlinNoise((i_x + V3_PlayerPos.x) / 64.0f,       (i_z + V3_PlayerPos.z + 84) / 64.0f) * 27.0f +
                                                     Mathf.PerlinNoise((i_x + V3_PlayerPos.x - 612) / 16.0f, (i_z + V3_PlayerPos.z) / 16.0f) * 5.0f +
                                                     Mathf.PerlinNoise((i_x + V3_PlayerPos.x) / 4.0f,        (i_z + V3_PlayerPos.z) / 4.0f + 64) +
                                                     Mathf.PerlinNoise((i_x + V3_PlayerPos.x + 8) / 24.0f,   (i_z + V3_PlayerPos.z) / 24.0f - 8) * 12.0f +
                                                     Mathf.PerlinNoise((i_x + V3_PlayerPos.x + 80) / 64.0f,  (i_z + V3_PlayerPos.z) / 64.0f - 80) * 40.0f);
                values[i_x, (size - 1) - i_z] = I_TerrainValue;
            }
        }

        return values;
    }
}
