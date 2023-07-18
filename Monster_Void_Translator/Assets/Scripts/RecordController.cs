using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordController : MonoBehaviour
{
    public Button recordBtn;
    public Button stopRecordBtn;
    public Button playBtn;
    public Button stopPlayBtn;
    public Image fillInPlay;
    public Text timeCountDownTxt;
    bool played = false;
    public int indexSound;
    public GameObject playCover;
    public GameObject recordCover;
    bool isPlaying = false;
    bool isCountDown = false;
    float timePlay = 0f;
    float timeCountDown;
    private void Awake()
    {
        indexSound = 0;
        recordBtn.onClick.AddListener(OnRecordBtnClicked);
        stopRecordBtn.onClick.AddListener(OnStopRecordBtnClicked);
        playBtn.onClick.AddListener(OnPlayBtnClicked);
        stopPlayBtn.onClick.AddListener(OnStopPlayBtnClicked);
    }

    private void Update()
    {
        if(isCountDown)
        {
            timeCountDown -= Time.deltaTime;
            timeCountDownTxt.text = "The sound will be played after " + Helper.ConvertToMinuteSecond((int)timeCountDown);
            if(timeCountDown <= 0)
            {
                isCountDown = false;
                isPlaying = true;
                timeCountDownTxt.gameObject.SetActive(false);
            }
        }
        if(isPlaying)
        {
            if(!played)
            {
                SoundController.Instance.PlaySound(PlayerData.currentMonster, indexSound);
                played = true;
            }
            timePlay += Time.deltaTime;
            
            SetFillOnPlay();
            if(timePlay >= SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length)
            {
                if(!PopUpTranslatorController.Instance.isLoop)
                    OnStopPlayBtnClicked();
                else
                {
                    OnPlayBtnClicked();
                    timeCountDown = 0f;
                }
            }
        }
    }

    void OnRecordBtnClicked()
    {
        recordBtn.gameObject.SetActive(false);
        stopRecordBtn.gameObject.SetActive(true);
        playCover.SetActive(true);
        var soundList = SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds;
        indexSound = Random.Range(0, soundList.Count);
    }
    void OnStopRecordBtnClicked()
    {
        playCover.SetActive(false);
        stopRecordBtn.gameObject.SetActive(false);
        recordBtn.gameObject.SetActive(true);
        SaveRecord.SaveDataRecord((int)SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length, PlayerData.currentMonster, indexSound);
        PlayerData.numberRecord++;
    }

    void OnPlayBtnClicked()
    {
        timeCountDown = PopUpTranslatorController.Instance.waitTime;
        isCountDown = true;
        recordCover.SetActive(true);
        played = false;
        timePlay = 0f;
        fillInPlay.fillAmount = 0f;
        timeCountDownTxt.gameObject.SetActive(true);
        playBtn.gameObject.SetActive(false);
        stopPlayBtn.gameObject.SetActive(true);

    }
    void OnStopPlayBtnClicked()
    {
        if(isCountDown)
        {
            isCountDown = false;
        }
        else
            isPlaying = false;
        SoundController.Instance.source.Stop();
        recordCover.SetActive(false);
        timeCountDownTxt.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(true);
        stopPlayBtn.gameObject.SetActive(false);
    }
    void SetFillOnPlay()
    {
        var length = SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length;
        fillInPlay.fillAmount = timePlay / length ;
    }
}
