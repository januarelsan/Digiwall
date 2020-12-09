using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfoARPanel : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite[] targetSprites;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "ARTracingGame"){
            targetImage.sprite = targetSprites[ARHijaiyahDataManager.Instance.SelectedHijaiyahIndex];
            targetImage.SetNativeSize();
        } else {
            targetImage.sprite = targetSprites[ARAnimateDataManager.Instance.SelectedModelIndex];
            targetImage.SetNativeSize();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
