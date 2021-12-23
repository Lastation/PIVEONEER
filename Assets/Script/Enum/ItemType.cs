using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType
{
    Soil            = 00,       // 일반 블럭
    Lithium         = 01,       // 리튬
    IronStone       = 02,       // 철광석
    Chalcopyrite    = 03,       // 황동석
    Quartz          = 04,       // 석영
    Bauxite         = 05,       // 보크사이트 (철반석)
    Titanite        = 06,       // 티타나이트 (설석)
    Graphite        = 07,       // 그라파이트 (흑연)
    Diamond         = 08,       // 다이아몬드
    Oxygen          = 09,       // 산소
    Energy          = 10,       // 에너지
    OxygenCapsule   = 11,       // 아이템 산소 캡슐
    EnergyBattery   = 12,       // 아이템 에너지 베터리
    Iron            = 13,       // 철
    Copper          = 14,       // 구리
    Glass           = 15,       // 유리
    Aluminum        = 16,       // 알루미늄
    Titanium        = 17,       // 티타늄
    Carbon          = 18,       // 탄소
    Graphere        = 19        // 그래핀
}

public enum ETCItemType
{
    Base_GG                 = 00,       // 유리지붕+유리벽
    Base_GT                 = 01,
    Base_TG                 = 02,  
    Base_TT                 = 03,  
    Garage                  = 04,  
    platform                = 05,  
    Aisle_T                 = 06,  
    Aisle_G                 = 07,  
    Corss_1                 = 08,  
    Corss_2                 = 09,  
    Half_1                  = 10,  
    Half_2                  = 11,  
    Vertical_1              = 12,  
    Verticla_2              = 13,  
    Smelt                   = 14,  
    Work_Surface            = 15,  
    Printer                 = 16,  
    research                = 17,  
    Food_Supply             = 18,  
    Battery                 = 19,  
    Oxygen_Generatior       = 20,
    Vehicle_Manufacturer    = 21,
    Solar_Panel             = 22,
    Wind_Turbine            = 23,
    Coal_Generator          = 24,
    Geothermal_Generator    = 25,
    Electric_Power          = 26
}

public enum ItemShape
{
    Cube            = 00,       // 사각형
    OtherMesh       = 01        // 복셀 메쉬
}

public enum ItemInfo
{
    Name            = 00,
    Info            = 01,
    Weight          = 02
}