using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum SoundType
{
    Enviorment  = 0,
    UI          = 1,
    Object      = 2,
    Charactor   = 3,
    MainMenu    = 4
}

public enum Sound_Enviorment
{
    magma_lake  = 0,
    wind1       = 1,
    wind2       = 2,
    rain        = 3,
    weed        = 4,
    cave        = 5,
}

public enum Sound_UI
{
    button          = 0,
    decision        = 1,
    cancel          = 2,
    mouseWheel      = 3,
    inventoryArrow  = 4,
    combination     = 5,
    mapOpen         = 6
}

public enum Sound_Object
{
    locker_open     = 0,
    locker_close    = 1,
    autodoor_open   = 2,
    autodoor_close  = 3,
    item_get        = 4
}

public enum Sound_Charactor
{
    eat         = 0,
    collector   = 1,
    scanner     = 2,
    hammer      = 3,
    _break      = 4,
    create      = 5
}

public enum Sound_MainMenu
{
    main        = 0,
    start       = 1
}
