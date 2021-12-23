using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public static class CSV_Init
{
    public static List<Dictionary<string, object>> Combination;                 // Combination table
    public static List<Dictionary<string, object>> ITEM_INFO;                   // Combination table

    // Use this for initialization
    public static void Init()
    {
        Combination =   CSV_Reader.Read("CSV/ITEM_Combination");
        ITEM_INFO   =   CSV_Reader.Read("CSV/ITEM_Info");
    }
}