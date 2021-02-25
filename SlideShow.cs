
using System.Collections;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SlideShow : UdonSharpBehaviour
{
    private bool isActive;
    private float timerCount;
    public float timer = 10;

    public int currentPic;

    public GameObject thingy;
    public int count;
    public GameObject[] thingyparts;
    void Start()
    {
        currentPic = count;
        count = 0;
        foreach (Transform i in thingy.transform)
        {
            count++;
        }
        thingyparts = new GameObject[count];
        count = 0;
        foreach (Transform i in thingy.transform)
        {
            thingyparts[count] = i.gameObject;
            count++;
        }
        thingyparts[0].SetActive(true);
        StartSlideShow();
    }

    private void StartSlideShow()
    {
        isActive = true;
    }

    private void NextPicture()
    {
        thingyparts[currentPic].SetActive(false);
        currentPic++;
        if(currentPic >= count)
        {
            currentPic = 0;
        }
        thingyparts[currentPic].SetActive(true);

    }
    private void Update()
    {
        if (isActive)
        {
            if(timerCount >= timer)
            {

                timerCount = 0;
                isActive = false;
                NextPicture();
                StartSlideShow();
            }
            else
            {
                timerCount += Time.deltaTime;
            }
        }
    }
}
