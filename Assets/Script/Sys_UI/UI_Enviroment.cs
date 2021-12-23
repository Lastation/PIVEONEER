using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_Enviroment : MonoBehaviour
{
    [Header("온도 텍스트 설정")]
    [SerializeField] Text TEXT_degree;

    [Header("GPS 텍스트 설정")]
    [SerializeField] Text TEXT_GPS;

    [Header("HP Silder")]
    [SerializeField] Slider SLIDER_Hp_value;

    [Header("Hungry Silder")]
    [SerializeField] Slider SLIDER_Hungry_value;

    private float FLOAT_Hp_Max;
    private float FLOAT_Hungry_Max;

    private PlayerSetting playerScript;
    private Vector3 V3_Playerpos;

    // Use this for initialization
    public void Init()
    {
        playerScript = Manager_GAME.Get_PlayerScript();

        FLOAT_Hp_Max = 20.0f;
        FLOAT_Hungry_Max = 10.0f;
    }

    // Update is called once per frame
    public void Updated()
    {
        if (playerScript)
        {
            // Degree
            V3_Playerpos = playerScript.transform.position;
            TEXT_degree.text = (V3_Playerpos.y / 4).ToString("0.0");

            // GPS
            TEXT_GPS.text = string.Format("GPS : x {0} y {1} z {2} ", (int)V3_Playerpos.x - 6400, (int)V3_Playerpos.y, (int)V3_Playerpos.z - 6400);

            // HP
            SLIDER_Hp_value.value = playerScript.Get_HP_value() / FLOAT_Hp_Max;

            // Hungry
            SLIDER_Hungry_value.value = playerScript.Get_Hungry_value() / FLOAT_Hungry_Max;
        }
    }
}
