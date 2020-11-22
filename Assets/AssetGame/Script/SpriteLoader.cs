using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpriteLoader : MonoBehaviour
{
    [SerializeField] private string fileName;
    private Image resultImage;
    private string path;

    void Awake(){
        resultImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

        path = Application.persistentDataPath + "/" + fileName + ".png";

        if(path != null){

            byte[] pngImageByteArray = null;
                        
            pngImageByteArray = File.ReadAllBytes(path);

            Texture2D tempTexture = new Texture2D(2, 2);
            tempTexture.LoadImage(pngImageByteArray);

            resultImage.sprite = Sprite.Create(tempTexture,new Rect(0,0, tempTexture.width, tempTexture.height) ,new Vector2(0,0), .01f);
        }

    }

    
}
