using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] GameObject monsterAvatar;
    float timePlay = 0f;
    public float timeCountDown;
    public GameObject recordImage;
    public GameObject playImage;
    private void Awake()
    {
        indexSound = 0;
        Instance = this;
        playBtn.onClick.AddListener(OnPlayBtnClicked);
        stopPlayBtn.onClick.AddListener(OnStopPlayBtnClicked);

        
    }

    private void Start()
    {
        if (PlayerData.tutorialTrans == 0)
        {
            TutorialTranslatorPopUp.Instance.TutorialRecord();
        }
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
                if(PlayerData.vibration == 1) Handheld.Vibrate();
            }
            SetFillOnPlay();
            if(timePlay >= SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length)
            {
                timePlay = 0f;
                fillInPlay.fillAmount = 0f;
                if (!PopUpTranslatorController.Instance.isLoop)
                {
                    OnStopPlayBtnClicked();

                    Debug.Log("Test1");
                }
                else
                {
                    OnPlayBtnClicked();
                    timeCountDown = 0f;
                }
            }
        }
    }

    public void OnRecordBtnClicked()
    {
        if (PlayerData.tutorialTrans == 0)
        {
            TutorialTranslatorPopUp.Instance.SetActiveTutorialRecord(false);
        }
        recordImage.SetActive(true);
        playImage.SetActive(false);
        playCover.SetActive(true);
        recordBtn.GetComponent<RectTransform>().localScale = Vector3.one * 0.7f;
        var soundList = SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds;
        indexSound = Random.Range(0, soundList.Count);
    }
    public void OnStopRecordBtnClicked()
    {
        playImage.SetActive(false);
        recordImage.SetActive(false);
        playCover.SetActive(false);
        recordBtn.GetComponent<RectTransform>().localScale = Vector3.one;
        SaveRecord.SaveDataRecord((int)SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length, PlayerData.currentMonster, indexSound);
        PlayerData.numberRecord++;

        if(PlayerData.tutorialTrans == 0)
        {
            TutorialTranslatorPopUp.Instance.TutorialPlay();
        }
    }

    public void OnPlayBtnClicked()
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
        if (PlayerData.tutorialTrans == 0)
        {
            TutorialTranslatorPopUp.Instance.SetActiveTutorialPlay(false);
        }
    }
    public void OnStopPlayBtnClicked()
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
        if (PlayerData.tutorialTrans == 0 && TutorialTranslatorPopUp.Instance.isTutoring)
        {
            TutorialTranslatorPopUp.Instance.TutorialMonsterSelect();
        }
    }
    void SetFillOnPlay()
    {
        var length = SoundController.Instance.monsterSounds[PlayerData.currentMonster].monsterSounds[indexSound].length;
        fillInPlay.fillAmount = timePlay / length ;
    }
}
