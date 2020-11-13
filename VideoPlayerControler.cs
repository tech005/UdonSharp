
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

    public AudioSource[] speakers;
    public Slider sliderVolume;
    public VRCUrlInputField URLField;
    public VRCUrl Insomniac;
    public VRCUrl ADHDJ;
    public VRCUrl ninethreeXtil;
    public VRCUrl rovnine;
    public Text PWNER;
    

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
    public void SetOwnerName()
    {
        
    }
    public override void OnVideoError(VideoError videoError)
    {
        if(videoError == VideoError.RateLimited)
        {
            loadedVideoNumber -=1;
        }
    }
}
