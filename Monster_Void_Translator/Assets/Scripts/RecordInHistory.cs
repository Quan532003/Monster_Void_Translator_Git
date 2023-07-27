using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RecordInHistory : MonoBehaviour
{
    public string recordName;
    public string recordDay;
    public float recordLength;
    public int monsterIndex;
    public int soundIndex;
    public GameObject playImage;
    public Text recordNameTxt;
    public Text recordDayTxt;
    [SerializeField] Text firstTimeText;
    [SerializeField] Text secondTimeText;
    public Text recordLengthTxt;
    public Button playBtn;
    public Button stopBtn;
    public Button shareBtn;
    public Button eraseBtn;
    public Image playFill;
    public bool isPlaying = false;
    public float timePlay = 0;
    public float length;
    float timeVibration = 0f;

    public void OnRecordClicked(int index, bool active)
    {
        SetTextPlay(index);
        recordLengthTxt.gameObject.SetActive(active);
        isPlaying = false;
    }
    public void SetTextInRecord()
    {
        recordDayTxt.text = Helper.GetRecordDay() ;
        recordNameTxt.text = recordName;
        recordLengthTxt.text = Helper.ConvertToMinuteSecond(recordLength);
    }

    public void SetTextPlay(int index)
    {
        firstTimeText.text = Helper.ConvertToMinuteSecond(0);
        secondTimeText.text = Helper.ConvertToMinuteSecond(PlayerData.GetRecordLength(index));
    }


    private void Update()
    {
        if(isPlaying)
        {
            timePlay += Time.deltaTime;
            playFill.fillAmount = timePlay / length;
            firstTimeText.text = Helper.ConvertToMinuteSecond((int)timePlay);
            secondTimeText.text = Helper.ConvertToMinuteSecond((int)(length - timePlay) + 1);
            if(timePlay - timeVibration >= 1f)
            {
                timeVibration = timePlay;
                if(PlayerData.vibration == 1) Handheld.Vibrate();
            }
            if(timePlay > length)
            {
                if(!TutorialHistory.Instance.isHandPlayActive())
                {
                    if (PlayerData.tutorialHistory == 0)
                    {
                        TutorialHistory.Instance.TutorialShare();
                    }
                }
                firstTimeText.text = Helper.ConvertToMinuteSecond(0);
                secondTimeText.text = Helper.ConvertToMinuteSecond((int)length);
                isPlaying = false;
                timePlay = 0f;
                timeVibration = 0f;
                playFill.fillAmount = 0f;
                playBtn.gameObject.SetActive(true);
                stopBtn.gameObject.SetActive(false);
            }
        }
    }
    

    public void ResetButtonAndImage()
    {
        playFill.fillAmount = 0f;
        timePlay = 0f;
        timeVibration = 0f;
        playBtn.gameObject.SetActive(true);
        stopBtn.gameObject.SetActive(false);
    }
}
