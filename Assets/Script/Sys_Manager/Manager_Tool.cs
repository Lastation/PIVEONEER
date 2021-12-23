using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Manager_Tool : MonoBehaviour
{
    [Header("FlashLight Setting")]
    [SerializeField] private GameObject FlashLight_Obj;
    [SerializeField] private float FlashLight_Intensity;

    // Use this for initialization
    public void Init()
    {
    }

    // Update is called once per frame
    public void Updated()
    {
        Tool_Setactive_Key();
    }

    public void Tool_Setactive_Key()
    {
        if (Input.GetKeyDown(Manager_Key.instance.FlashLight_Key))
        {
            if (FlashLight_Obj.activeInHierarchy)
            {
                if (FlashLight_Obj.GetComponent<Light>().intensity != FlashLight_Intensity)
                    FlashLight_Obj.GetComponent<Light>().intensity = FlashLight_Intensity;

                FlashLight_Obj.SetActive(false);
            }
            else FlashLight_Obj.SetActive(true);
        }
    }
}
