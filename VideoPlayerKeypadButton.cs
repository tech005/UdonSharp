using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class VideoPlayerKeypadButton : UdonSharpBehaviour
{
    public Text ButtonValue;
    public VideoPlayerKeypadLogic targetProgram;



    public void ButtonPress()
    {
        Debug.Log("Pressed a button");
        Debug.Log(ButtonValue.text);
        targetProgram.inputKey = ButtonValue.text;
        targetProgram.keyPressed();
    }
}
