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

    
    [SerializeField] Animation anim = null;

    [Header("Container")]
    [SerializeField] GameObject container = null;
    [SerializeField] GameObject aktivasi = null;
    [SerializeField] GameObject sukses = null;

    [Header("Activation")]
    [SerializeField] UnityEngine.UI.Text heading = null;
    [SerializeField] UnityEngine.UI.Text description = null;
    [SerializeField]
    UnityEngine.UI.InputField input = null;
    [SerializeField] GameObject failed = null;
    [SerializeField] Color colorTry = new Color();//FD6048
    [SerializeField] Color colorFailed = new Color();

    [Header("BuyPage")]
    [SerializeField] GameObject buyPage = null;

    private void Start()
    {
        if ((PlayerPrefs.GetInt(GlobalKey.IS_PREMIUM, 0) != 1))
            OpenTryReedem();
        else
            OpenSuccessReedem();
    }

    public void CheckVoucher(string value) {
        if (value != "generasialfa2020")
        {
            if (value == "") { 
                setTryReedem();
                return;
            }
            //failed.SetActive(true);
            setFailedReedem();
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

        setTryReedem();
    }

    public void setTryReedem() {
        input.text = "";
        failed.SetActive(false);
        heading.color = colorTry;
        heading.text = "Aktivasi dengan Kode Voucher";
        description.text = "Masukan kode voucher yang terlampir dalam kemasan Produk Digiwall";
    }
    public void setFailedReedem()
    {
        failed.SetActive(true);
        heading.color = colorFailed;
        heading.text = "Aktivasi Produk Gagal";
        description.text = "Kode yang anda masukan salah\natau tidak dikenal";
    }

    public void GotoLink()
    {
        Application.OpenURL("https://alfaarena.orderonline.id/App-orderdigiwall");
    }
}
