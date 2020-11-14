using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneManager : MonoBehaviour
{

    [SerializeField]Sprite[] imageAsset = null;
    int currentImage = 0;

    [SerializeField] UnityEngine.UI.Image image = null;
    // Start is called before the first frame update
    void Start()
    {
        image.sprite = imageAsset[currentImage];   
    }

    // Update is called once per frame
    public void Next()
    {
        currentImage++;
        if (currentImage == imageAsset.Length)
            currentImage = 0;

        image.sprite = imageAsset[currentImage];
    }
    public void Prev()
    {
        currentImage--;
        if (currentImage < 0)
            currentImage = imageAsset.Length-1;

        image.sprite = imageAsset[currentImage];
    }
}
