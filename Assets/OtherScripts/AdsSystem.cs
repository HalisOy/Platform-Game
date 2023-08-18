using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsSystem : MonoBehaviour
{
    AdsGameInSystem GameInAdsSystem;

    private void Start()
    {
        GameInAdsSystem = transform.GetComponent<AdsGameInSystem>();
    }

    public void LoadAds()
    {
        if (GameInAdsSystem.interstitialAdShow)
            GameInAdsSystem.LoadInterstitialAd();
    }
}
