using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockDissolve : MonoBehaviour
{
    public Material m_dissolveMat;
    private float f_speed, f_Intensity;

    private void Awake()
    {
        f_Intensity = 0.0f;
    }

    private void Update()
    {
        if (f_Intensity < 0.7f)
        {
            m_dissolveMat.SetFloat("_DissolveIntensity", f_Intensity);
            f_Intensity += Time.deltaTime * 2.0f;
        }
        else if (f_Intensity >= 0.7f)
        {
            m_dissolveMat.SetFloat("_DissolveIntensity", 0);
            Destroy(this.gameObject);
        }
    }
}