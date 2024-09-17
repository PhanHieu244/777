using GleyMobileAds;
using System;
using System.Collections;
/// <summary>
/// Version 1.6.0
/// 
/// For any questions contact us at:
/// gley.mobi@gmail.com
/// or forum
/// https://forum.unity.com/threads/mobile-ads-simple-way-to-integrate-ads-in-your-app.529292/
/// 
/// </summary>

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//each advertiser will be setup using this class
public class Advertiser
{
    public ICustomAds advertiserScript;
    public SupportedAdvertisers advertiser;
    public MediationSettings mediationSettings;
    public List<PlatformSettings> platformSettings;

    public Advertiser(ICustomAds advertiserScript, MediationSettings mediationSettings, List<PlatformSettings> platformSettings)
    {
        this.advertiserScript = advertiserScript;
        this.mediationSettings = mediationSettings;
        this.platformSettings = platformSettings;
        advertiser = mediationSettings.advertiser;
    }
}


public enum SupportedAdTypes
{
    None,
    Banner,
    Interstitial,
    Rewarded
}

public enum UserConsent
{
    Unset = 0,
    Accept = 1,
    Deny = 2
}

public class Advertisements : MonoBehaviour
{
    //name of the PlayerPrefs key to save consent and show ads status

}
