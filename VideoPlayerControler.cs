using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDK3.Components.Video;
using VRC.SDK3.Video.Components.Base;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;
using VRC.Udon;

public class VideoPlayerControler : UdonSharpBehaviour
{
    private BaseVRCVideoPlayer videoPlayer;
    private int loadedVideoNumber;

    [UdonSynced]private VRCUrl syncedURL;
    [UdonSynced]private int currentVideoNumber;

    public VRCUrl WorldStartUrl;
    public AudioSource[] speakers;
    public Slider sliderVolume;
    public VRCUrlInputField URLField;
    public VRCUrl Insomniac;
    public VRCUrl ADHDJ;
    public VRCUrl ninethreeXtil;
    public VRCUrl rovnine;
    public VRCUrl AfterHours;
    public VRCUrl Lilkizze;
    public VRCUrl Zoomair;
    public VRCUrl Badlands;
    public Text PWNER;
    public string DJ;
    

    void Start()
    {
        videoPlayer = (BaseVRCVideoPlayer)GetComponent(typeof(BaseVRCVideoPlayer));
        PWNER.text = "PWNer: " + Networking.GetOwner(gameObject).displayName;
    }
    public override void OnDeserialization()
    {
        if(loadedVideoNumber != currentVideoNumber)

        {
            loadedVideoNumber = currentVideoNumber;
            if(syncedURL.Get() != "" && syncedURL.Get() != null)
            {
                LoadURL();
            }
        }
    }
    public void PlayPause()
    {
        if (videoPlayer.IsPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
    public void SetVolume()
    {
        foreach (var item in speakers)
        {
            item.volume = sliderVolume.value;
        }
    }
    public void SetUrl()
    {
        if (!Networking.IsOwner(gameObject)) return;

        syncedURL = URLField.GetUrl();
        currentVideoNumber += 1;
        loadedVideoNumber = currentVideoNumber;
        LoadURL();
    }
    public void LoadURL()
    {
        videoPlayer.Stop();
        videoPlayer.LoadURL(syncedURL);
    }
    public void PlayURL()
    {
        videoPlayer.Stop();
        videoPlayer.LoadURL(URLField.GetUrl());
    }
    public override void OnVideoReady()
    {
        videoPlayer.Play();
    }
     public void PlayZoomair()
    {
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = Zoomair;
        currentVideoNumber += 1;
        LoadURL();
    }
     public void PlayLilkizzle()
    {
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = Lilkizze;
        currentVideoNumber += 1;
        LoadURL();
    }
     public void PlayBadlands()
    {
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = Badlands;
        currentVideoNumber += 1;
        LoadURL();
    }

   public void PlayInsomniac()
    {
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = Insomniac;
        currentVideoNumber += 1;
        LoadURL();
    }
    public void PlayADHDJ()
    {
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = ADHDJ;
        currentVideoNumber += 1;
        LoadURL();    
    }    
    public void PlayNineThreeXtil()
    {
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = ninethreeXtil;
        currentVideoNumber += 1;
        LoadURL();
    }    
    public void PlayRovNine()
    {   
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = rovnine;
        currentVideoNumber += 1;
        LoadURL();
    }
    public void PlayAfterHours()
    {   
        if (!Networking.IsOwner(gameObject)) return;
        syncedURL = AfterHours;
        currentVideoNumber += 1;
        LoadURL();
    }

    public void PWN()
    {
        if (!Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            PWNER.text = "PWNer: " + Networking.GetOwner(gameObject).displayName;
        }
    }

    public override void OnOwnershipTransferred()
    {
        PWNER.text = "PWNer: " + Networking.GetOwner(gameObject).displayName;
    }
    public override void OnVideoError(VideoError videoError)
    {
        if(videoError == VideoError.RateLimited)
        {
            loadedVideoNumber -=1;
        }
    }
    public void RestartStreamButton()
    {
        if (!videoPlayer.IsPlaying && syncedURL == null)
        {
            videoPlayer.Stop();
            syncedURL = WorldStartUrl;
            videoPlayer.LoadURL(syncedURL);
        }
        else
        {
            videoPlayer.Stop();
            videoPlayer.LoadURL(syncedURL);
        }
    }

}
