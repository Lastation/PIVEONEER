
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BlockType : ushort
{
    Air             = 0x0000,
    Flat_Island_00  = 0x0100,
    Flat_Island_01  = 0x0200,
    Flat_Island_02  = 0x0300,
    Flat_Island_03  = 0x0400,
    Flat_Forest_00  = 0x0500,
    Flat_Forest_01  = 0x0600,
    Flat_Forest_02  = 0x0700,
    Flat_Forest_03  = 0x0800,
    Grass           = 0x0900,        // 미정

    Lithium         = 0x1000,        // 리튬
    IronStone       = 0x1100,        // 철광석
    Chalcopyrite    = 0x1200,        // 황동석
    Quartz          = 0x1300,        // 석영
    Bauxite         = 0x1400,        // 보크사이트 (철반석)
    Titanite        = 0x1500,        // 티타나이트 (설석)
    Graphite        = 0x1600,        // 그라파이트 (흑연)
    Diamond         = 0x1700,        // 다이아몬드

    Oxygen          = 0x2100,
    Energy          = 0x2200,

    Lava            = 0xFE00,
    Badrock         = 0xFF00
};

public enum BlockParent : int
{
    Badrocks        = 0,
    Blocks          = 1,
    Ores            = 2,
    Minerals        = 3,
    Structures      = 4,
    Lava            = 5
}

public enum Blocks : int
{
    Flat_Island_00  = 0,
    Flat_Island_01  = 1,
    Flat_Island_02  = 2,
    Flat_Island_03  = 3,
    Flat_Forest_00  = 4,
    Flat_Forest_01  = 5,
    Flat_Forest_02  = 6,
    Flat_Forest_03  = 7,
    Grass           = 8
}

public enum Ores : int
{
    Lithium         = 0,        // 리튬
    IronStone       = 1,        // 철광석
    Chalcopyrite    = 2,        // 황동석
    Quartz          = 3,        // 석영
    Bauxite         = 4,        // 보크사이트 (철반석)
    Titanite        = 5,        // 티타나이트 (설석)
    Graphite        = 6,        // 그라파이트 (흑연)
    Diamond         = 7         // 다이아몬드
}

public enum Structures : int
{
    Tree            = 0,
    Plant04         = 1,
    Plant03         = 2,
    Plant02         = 3,
    Plant01         = 4,
}

public enum Minerals : int
{
    Oxygen          = 0,
    Energy          = 1
}

public enum Shape : byte
{
    Cube        = 0x00,
    Stair       = 0x10,
    Half        = 0x20,
    Slant       = 0x30,
    Fence       = 0x40,
    Cone        = 0x50
};

public enum Direction : byte
{
    North = 1,
    East = 2,
    South = 4,
    West = 8,
    Up = 16,
    Down = 32
}

public enum Rotation : byte
{
    X0Y0        = 0x00,
    X0Y90       = 0x01,
    X0Y180      = 0x02,
    X0Y270      = 0x03,
    X90Y0       = 0x04,
    X90Y90      = 0x05,
    X90Y180     = 0x06,
    X90Y270     = 0x07,
    X180Y0      = 0x08,
    X180Y90     = 0x09,
    X180Y180    = 0x0A,
    X180Y270    = 0x0B,
    X270Y0      = 0x0C,
    X270Y90     = 0x0D,
    X270Y180    = 0x0E,
    X270Y270    = 0x0F
};

public enum MeshType
{
    Block       = 0,
    Structure   = 1
}