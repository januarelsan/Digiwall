using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChromaManager : MonoBehaviour
{   
    [SerializeField] private MeshRenderer[] meshes; 
    [SerializeField] private Material material;
    [SerializeField] private Slider slider;

    private int selectedModelIndex;
    
    
    // Start is called before the first frame update
    void Start()
    {
        selectedModelIndex = ARAnimateDataManager.Instance.SelectedModelIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateChroma(){
        meshes[selectedModelIndex].material.SetFloat("_Sens", slider.value);        
        
        // Debug.Log("Sens Value: " + meshes[selectedModelIndex].materials[0].GetFloat("_Sens"));
        
    }

    public void SelectedModel(int index){
        selectedModelIndex = index;
        slider.value = 0.5f;
    }
}
