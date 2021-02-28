using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Video.Components.Base;
using VRC.SDKBase;


public class VideoPlayerKeypadLogic : UdonSharpBehaviour
{
    public string ItemToggleCode;
    public GameObject[] ItemsToToggle;
    public string ItemToggleText;
    public string TeleportCode;
    public Transform TeleportLocation;
    public string TeleportText;
    [HideInInspector]public string inputKey = "";
    public string code;
    public Text console;
    public BaseVRCVideoPlayer VideoPlayer;

    private string databuffer = "";

    public void ClearConsole()
    {
        console.text = "";
        databuffer = "";
        Debug.Log("Console cleared");
    }

    public void keyPressed()
    {
        Debug.Log("Key pressed function");
        if (inputKey == "ok")
        {
            if (databuffer == code)
            {
                VideoPlayer.GetComponent<VideoPlayerControler>().PWN();
                Debug.Log("code accepted");
                console.text = "Granted";
                databuffer = "";
            }
            else if (databuffer == TeleportCode)
            {
                Networking.LocalPlayer.TeleportTo(TeleportLocation.position, TeleportLocation.rotation);
                Debug.Log("code accepted teleport");
                console.text = TeleportText;
                databuffer = "";
            }
            else if (databuffer == ItemToggleCode)
            {
                Debug.Log("Toggle Items");
                foreach (GameObject Item in ItemsToToggle)
                {
                    Item.SetActive(!Item.activeSelf);
                }
                console.text = ItemToggleText;
            }
            else
            {
                Debug.Log("code denied");
                console.text = "Denied";
                databuffer = "";
            }
        }
        else if (inputKey == "clr")
        {
            ClearConsole();

        }
        else if (databuffer.Length < 4)
        {
            if (console.text == "Granted" ^ console.text == "Denied")
            {
                ClearConsole();
            }
            databuffer += inputKey;
            console.text += "*";
        }
        else
        {
            Debug.Log("Limit reached!");
        }
    }
}
