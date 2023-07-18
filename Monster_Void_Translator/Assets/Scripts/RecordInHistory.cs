using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class RecordInHistory : MonoBehaviour
{
    public string recordName;
    public string recordDay;
    public int recordLength;
    public int monsterIndex;
    public int soundIndex;

    public Text recordNameTxt;
    public Text recordDayTxt;
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
    public void SetTextInRecord()
    {
        recordDayTxt.text = Helper.GetRecordDay() ;
        recordNameTxt.text = recordName;
        recordLengthTxt.text = Helper.ConvertToMinuteSecond(recordLength);
    }
    private void Update()
    {
        if(isPlaying)
        {
            timePlay += Time.deltaTime;
            playFill.fillAmount = timePlay / length;
            if(timePlay - timeVibration >= 1f)
            {
                timeVibration = timePlay;
                Handheld.Vibrate();
            }
            if(timePlay > length)
            {
                isPlaying = false;
                timePlay = 0f;
                timeVibration = 0f;
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
