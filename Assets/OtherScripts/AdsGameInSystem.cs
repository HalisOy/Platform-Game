using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsGameInSystem : MonoBehaviour
{
#if UNITY_ANDROID
    private string AdBannerID = "ca-app-pub-3940256099942544/6300978111";
    private string AdInterstitialID = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
    private string AdBannerID = "ca-app-pub-3940256099942544/2934735716";
    private string AdInterstitialID = "ca-app-pub-3940256099942544/4411468910";
#else
    private string AdBannerID = "unused";
#endif

    BannerView bannerView;
    InterstitialAd interstitialAd;
    public bool interstitialAdShow { get; set; }
    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) => { });
        interstitialAdShow = true;
        CreateBannerView();
    }

    #region Banner

    public void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerAd();
        }

        bannerView = new BannerView(AdBannerID, AdSize.Banner, AdPosition.Top);
        BannerLoadAd();
    }

    private void BannerLoadAd()
    {
        AdRequest adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        bannerView.LoadAd(adRequest);
    }

    private void DestroyBannerAd()
    {
        bannerView.Destroy();
        bannerView = null;
    }

    #endregion

    #region Interstitial
    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        AdRequest adrequest = new AdRequest();
        adrequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(AdInterstitialID, adrequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
        interstitialAdShow = false;
        RegisterEventHandlers(interstitialAd);
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAdShow = true;
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("no ads to watch");
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitialAd();
        };
    }
    #endregion

    private IEnumerator AdDelay()
    {
        yield return new WaitForSeconds(90f);
    }
}
