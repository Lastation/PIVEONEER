using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UI_MouseOverEvent : MonoBehaviour
{
    [Header("Mouse Over Text & Object")]
    [SerializeField] private Text   TEXT_MouseOver;

    public void Update()
    {
        if (TEXT_MouseOver.text != "") TEXT_MouseOver.transform.position = Input.mousePosition + new Vector3(20.0f, -20.0f);
    }

    public void OnPointerEnter(string TEXT_DATA)
    {
        TEXT_MouseOver.text = TEXT_DATA;

        if (TEXT_MouseOver.text != "")  TEXT_MouseOver.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        TEXT_MouseOver.text = "";
        TEXT_MouseOver.transform.position = Vector3.zero;

        if (TEXT_MouseOver.text == "")  TEXT_MouseOver.gameObject.SetActive(false);
    }
}
