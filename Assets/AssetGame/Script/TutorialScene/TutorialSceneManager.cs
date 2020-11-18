using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneManager : MonoBehaviour
{

    [SerializeField]Sprite[] imageAsset = null;

    [Header("Image Asset")]
    [SerializeField] Sprite[] heading = null;
    [SerializeField] Sprite[] content = null;
    [SerializeField] Sprite[] background = null;
    [SerializeField] Sprite[] mascot = null;
 
    int currentImage = 0;
    [Header("Animation")]
    [SerializeField] Animation anim = null;

    [Header("ElementFront")]
    [SerializeField] UnityEngine.UI.Image headingImg1 = null;
    [SerializeField] UnityEngine.UI.Image contentImg1 = null;
    [SerializeField] UnityEngine.UI.Image backgroundImg1 = null;
    [SerializeField] UnityEngine.UI.Image mascotImg1 = null;

    [Header("ElementBack")]
    [SerializeField] UnityEngine.UI.Image headingImg2 = null;
    [SerializeField] UnityEngine.UI.Image contentImg2 = null;
    [SerializeField] UnityEngine.UI.Image backgroundImg2 = null;
    [SerializeField] UnityEngine.UI.Image mascotImg2 = null;
    //[SerializeField] UnityEngine.UI.Image image = null;
    // Start is called before the first frame update

    float cooldown = 0;

    void Start()
    {
        contentImg1.sprite = content[currentImage];
        headingImg1.sprite = heading[currentImage];
        backgroundImg1.sprite = background[currentImage % 4];
        mascotImg1.sprite = mascot[currentImage % 4];
    }

    void Update() {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    /*
    Sprite GetBackground(int idx) {
        return background[idx % 4];
    }

    Sprite GetBackground(int idx)
    {
        return background[idx % 4];
    }
    */

    // Update is called once per frame
    public void Next()
    {
        if (cooldown > 0)
            return;

        currentImage++;
        if (currentImage == content.Length)
            currentImage = 0;

        cooldown = 0.4f;
        SetImage();
    }
    public void Prev()
    {
        if (cooldown > 0)
            return;

        currentImage--;
        if (currentImage < 0)
            currentImage = content.Length-1;

        cooldown = 0.4f;
        SetImage();
    }

    void SetImage() {

        anim.Play("Swap");

        contentImg2.sprite = contentImg1.sprite;
        headingImg2.sprite = headingImg1.sprite;
        backgroundImg2.sprite = backgroundImg1.sprite;
        mascotImg2.sprite = mascotImg1.sprite;

        contentImg1.sprite = content[currentImage];
        headingImg1.sprite = heading[currentImage];
        backgroundImg1.sprite = background[currentImage % 4];
        mascotImg1.sprite = mascot[currentImage % 4];
    }

    public void BackToHome() {
        GtionProduction.GtionLoading.ChangeScene("HomeScene");
    }
}
