using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumBuyHandler : MonoBehaviour
{
    
    static PremiumBuyHandler _Main = null;
    public static PremiumBuyHandler Main
    {
        get
        {
            return _Main;
        }
        private set
        {
            _Main = value;
        }
    }
    public static void InstantiatePremiumBuyOnScene(OnSuccess successCallback = null)
    {
        if (Main != null)
        {
            Destroy(Main.gameObject);
        }

        PremiumBuyHandler tempMain = Resources.Load<PremiumBuyHandler>("PremiumBuy/PremiumBuy");
        Main = Instantiate(tempMain);
        Main.onComplete = successCallback;

    }

    public delegate void OnSuccess();
    OnSuccess onComplete = null;

    [SerializeField] GameObject failed = null;
    [SerializeField] Animation anim = null;

    [Header("Container")]
    [SerializeField] GameObject container = null;
    [SerializeField] GameObject aktivasi = null;
    [SerializeField] GameObject sukses = null;

    [Header("BuyPage")]
    [SerializeField] GameObject buyPage = null;

    private void Start()
    {
        OpenTryReedem();
    }

    public void CheckVoucher(string value) {
        if (value != "QWERTY")
        {
            failed.SetActive(true);
            GtionProduction.Vibration.Vibrate(200);
        }
        else {
            OpenSuccessReedem();
        }
    }

    public void CloseTab() {
        anim.Play("HidePremiumBuy");
        Destroy(gameObject, 0.6f);
    }

    public void OpenBuy() {
        container.SetActive(false);
        buyPage.SetActive(true);
    }

    public void OpenSuccessReedem() {
        container.SetActive(true);
        buyPage.SetActive(false);
        aktivasi.SetActive(false);
        sukses.SetActive(true);

        PlayerPrefs.SetInt(GlobalKey.IS_PREMIUM, 1);

        if (Main.onComplete != null)
        {
            Main.onComplete();
        }

    }

    public void OpenTryReedem()
    {
        container.SetActive(true);
        buyPage.SetActive(false);
        aktivasi.SetActive(true);
        sukses.SetActive(false);
    }

    public void GotoLink()
    {
        Application.OpenURL("http://unity3d.com/");
    }
}
