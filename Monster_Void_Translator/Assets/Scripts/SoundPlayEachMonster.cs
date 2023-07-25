using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayEachMonster : MonoBehaviour
{
    public Button backBtn;
    public Button settingBtn;
    public Button monsterBtn;
    public Button loopBtn;
    public Image fillPlay;
    public Dropdown timerSelector;
    public GameObject settingPopUp;
    public Image monsterImage;
    List<int> timeWaitList = new List<int>
    {
        0, 15, 30, 45, 60, 120, 300
    };
    public List<AudioClip> sounds = new List<AudioClip>();
    public List<Sprite> monsterAvatar = new List<Sprite>();
    public Image timeFill;
    public GameObject timeInPlay;
    public GameObject noticeInLoop;
    bool isLoop = false;
    float timeWait = 0f;
    bool isPlaying = false;
    float timePlay = 0f;
    bool isCountDown = false;
    float timeCountDown = 0f;
    bool played = false;
    public Text noticeTimeCountDown;
    public Text nameTxt;
    int waitIndex;
    private void Awake()
    {
        ResetLoopAndTimePlay();
        backBtn.onClick.AddListener(OnBackBtnClicked);
        settingBtn.onClick.AddListener(OnSettingBtnClicked);
        monsterBtn.onClick.AddListener(OnMonsterBtnClicked);
        loopBtn.onClick.AddListener(OnLoopBtnClicked);
        timerSelector.onValueChanged.AddListener(OnChangeValueInDropDown);
    }
    public void ResetLoopAndTimePlay()
    {
        isLoop = false;
        noticeInLoop.SetActive(false);
        timeFill.fillAmount = 0f;
        timePlay = 0f;
        timeCountDown = 0f;
        isCountDown = false;
        isPlaying = false;
        played = false;
        noticeTimeCountDown.gameObject.SetActive(false);
        timeInPlay.SetActive(false);
        SoundController.Instance.source.Stop();
    }
    public void SetSprite()
    {
        monsterImage.sprite = monsterAvatar[PlayerData.monsterIndexInSound];
    }
    public void SetName()
    {
        nameTxt.text = Helper.monsterName[PlayerData.monsterIndexInSound];
    }
    private void Update()
    {
        if(isCountDown)
        {
            timeCountDown += Time.deltaTime;
            noticeTimeCountDown.text = "The sound will be played affter " + Helper.ConvertToMinuteSecond((int)(timeWait - timeCountDown));
            if (timeCountDown >= timeWait)
            {
                timeCountDown = 0f;
                isCountDown = false;
                isPlaying = true;
                noticeTimeCountDown.gameObject.SetActive(false);
            }
        }
        if(isPlaying)
        {
            if(!played)
            {
                SoundController.Instance.PlaySoundInSoundPopUp(sounds[PlayerData.monsterIndexInSound]);
                played = true;
                timeInPlay.SetActive(true);
            }
            timePlay += Time.deltaTime;
            timeFill.fillAmount = timePlay / sounds[PlayerData.monsterIndexInSound].length;
            if(timePlay >= sounds[PlayerData.monsterIndexInSound].length)
            {
                isPlaying = false;
                played = false;
                timeInPlay.SetActive(false);
                timePlay = 0f;
                timeFill.fillAmount = 0f;
                if(isLoop)
                {
                    isCountDown = true;
                }
            }
        }

    }

    public void OnMonsterBtnClicked()
    {
        isCountDown = true;
        timeCountDown = 0f;
        noticeTimeCountDown.gameObject.SetActive(true);
    }
    public void OnBackBtnClicked()
    {
        this.gameObject.SetActive(false);
        SoundController.Instance.source.Stop();
    }
    public void OnSettingBtnClicked()
    {
        PopUpMovement.Instance.ShowPopUp(settingPopUp);
    }

    public void OnLoopBtnClicked()
    {
        isLoop = !isLoop;
        if(isLoop) timeWait = 0f;
        else
        {
            timeWait = timeWaitList[waitIndex];
        }
        noticeInLoop.SetActive(isLoop);

    }

    public void OnChangeValueInDropDown(int index)
    {
        timeWait = timeWaitList[index];
        waitIndex = index;
        isLoop = false;
        ResetLoopAndTimePlay();
    }
}
