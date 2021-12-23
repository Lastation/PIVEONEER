using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_Key : MonoBehaviour
{
    [Header("// 플레이어 관련 설정")]
    [Header("이동 키")]
    public string horizontalInputName;  // 수직 ( W, S ) 키
    public string verticalInputName;    // 수평 ( A, D ) 키
    [Header("달리기 키")]
    public KeyCode runKey;          // 달리기 키
    [Header("점프 키")]
    public KeyCode jumpKey;         // 점프키

    [Header("// UI 관련 설정")]
    [Header("환경창 키")]
    public KeyCode Enviroment_Key;  // 환경창
    [Header("인벤토리창 키")]
    public KeyCode Inventory_Key;   // 인벤토리창
    [Header("건물건설창 키")]
    public KeyCode Building_Key;    // 건물건설창
    [Header("설정창 키")]
    public KeyCode Setting_Key;     // 설정창
    [Header("미니맵 키")]
    public KeyCode MiniMap_Key;     // 미니맵

    [Header("// 도구 관련 설정")]
    [Header("스캔 키")]
    public KeyCode ScanTool_Key;    // 광물 스캔
    [Header("블럭 생성 키")]
    public KeyCode CreateBlock_Key; // 블럭 생성 
    [Header("건설 키")]
    public KeyCode BuildTool_Key;   // 건물 건설
    [Header("손전등 키")]
    public KeyCode FlashLight_Key;  // 손전등

    [Header("// 건물 키 관련 설정")]
    [Header("건물 회전 키")]
    public KeyCode Rotate_Key;      // 건물 회전 키

    public static Manager_Key instance;

    public void Awake()
    {
        instance = this;
    }
}