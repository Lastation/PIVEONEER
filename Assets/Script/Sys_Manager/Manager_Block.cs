using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_Block : MonoBehaviour
{
    [Header("블럭 디졸브 오브젝트")]
    [SerializeField] private GameObject g_DissolveBlocks;
    [SerializeField] private GameObject g_CreateBlocks;

    // Use this for initialization
    public void Init()
    {

    }

    // Use this for Block initialization
    public void Init(Texture _tex)
    {
        g_DissolveBlocks.GetComponent<MeshRenderer>().sharedMaterial.mainTexture    = _tex;
        g_CreateBlocks.GetComponent<MeshRenderer>().sharedMaterial.mainTexture      = _tex;
    }

    // Update is called once per frame
    public void Updated()
    {

    }

    public GameObject Get_DissolveBlocks()  { return g_DissolveBlocks;  }
    public GameObject Get_CreateBlocks()    { return g_CreateBlocks;    }
}
