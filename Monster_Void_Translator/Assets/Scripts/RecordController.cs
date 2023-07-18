using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordController : MonoBehaviour
{
    public static RecordController Instance;
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
    public GameObject recordImage;
    public GameObject playImage;
    private void Awake()
    {
        indexSound = 0;
        Instance = this;
        recordBtn.onClick.AddListener(OnRecordBtnClicked);
        stopRecordBtn.onClick.AddListener(OnStopRecordBtnClicked);
        playBtn.onClick.AddListener(OnPlayBtnClicked);
        stopPlayBtn.onClick.AddListener(OnStopPlayBtnClicked);
    }
    float timeVibration = 0f;
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
                playImage.SetActive(true);
                recordImage.SetActive(false);
                played = true;
            }
            timePlay += Time.deltaTime;
            if(timePlay - timeVibration >= 1f)
            {
                timeVibration = timePlay;
                //rung
                Handheld.Vibrate();
            }
            SetFillOnPlay();
            if(timePlay >= SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length)
            {
                timePlay = 0f;
                fillInPlay.fillAmount = 0f;
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
        recordImage.SetActive(true);
        playImage.SetActive(false);
        recordBtn.gameObject.SetActive(false);
        stopRecordBtn.gameObject.SetActive(true);
        playCover.SetActive(true);
        var soundList = SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds;
        indexSound = Random.Range(0, soundList.Count);
    }
    void OnStopRecordBtnClicked()
    {
        playImage.SetActive(false);
        recordImage.SetActive(false);
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
        timeVibration = 0f;
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
        playImage.SetActive(false);
        recordImage.SetActive(false);
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
