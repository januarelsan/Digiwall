using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesGetTexture : MonoBehaviour
{
    public RenderTextureCamera renderTextureCamera;

    public Material[] mats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTexture(){
        foreach (Material mat in mats)
        {            
            mat.mainTexture = renderTextureCamera.GetRenderTexture();
        }
    }
}
