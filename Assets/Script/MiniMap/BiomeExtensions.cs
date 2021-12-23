using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Biome
{
    Flat_Island_00,
    Flat_Island_01,
    Flat_Island_02,
    Flat_Island_03
}

public static class BiomeExtensions
{
    static Dictionary<Biome, Color> BiomeColorMap = new Dictionary<Biome, Color>()
    {
        {Biome.Flat_Island_00, new Color(035.0f / 255.0f, 090.0f / 255.0f, 090.0f / 255.0f) },
        {Biome.Flat_Island_01, new Color(075.0f / 255.0f, 118.0f / 255.0f, 165.0f / 255.0f) },
        {Biome.Flat_Island_02, new Color(026.0f / 255.0f, 051.0f / 255.0f, 078.0f / 255.0f) },
        {Biome.Flat_Island_03, new Color(162.0f / 255.0f, 151.0f / 255.0f, 177.0f / 255.0f) }
    };

    public static Biome GetBiome(float height)
    {
        if (height > 0      && height <= 96)        return Biome.Flat_Island_03;
        if (height > 96     && height <= 112)       return Biome.Flat_Island_02;
        if (height > 112    && height <= 128)       return Biome.Flat_Island_01;

        return Biome.Flat_Island_00;
    }

    public static Color GetColor(this Biome biome)
    {
        return BiomeColorMap[biome];
    }
}
