using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARModelActive : MonoBehaviour
{
    
    [SerializeField] private int index;
    // [SerializeField] private ChromaManager chromaManager;
    [SerializeField] private RenderTextureCamera renderTextureCamera;

    void OnEnable() {
        // chromaManager.SelectedModel(index);
        renderTextureCamera.SetModelIndex(index);

    }
}
