using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraSetting : MonoBehaviour
{
    [Header("마우스 입력 설정")]
    [SerializeField] private string mouseXInputName;
    [SerializeField] private string mouseYInputName;
    [Header("마우스 감도 설정")]
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform playerbody;

    private float   xAxisClamp;

    public void Init()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState    = CursorLockMode.Locked;
        Cursor.visible      = false;
        xAxisClamp          = 0.0f;
    }

    public void UnLockCursor()
    {
        Cursor.lockState    = CursorLockMode.None;
        Cursor.visible      = true;
    }

    public void Updated()
    {

    }

    public void FixedUpdated()
    {
        if (Cursor.lockState == CursorLockMode.Locked && Cursor.visible == false)
            CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.fixedDeltaTime;

        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerbody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}