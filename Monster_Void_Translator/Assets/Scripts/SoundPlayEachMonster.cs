using System;
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
    [SerializeField] Button monsterHoldBtn;
    [SerializeField] Image imageLabel;
    [SerializeField] List<Sprite> spriteForLabel = new List<Sprite>();
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
        monsterHoldBtn.GetComponent<Image>().sprite = monsterAvatar[PlayerData.monsterIndexInSound]; 
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
                monsterBtn.GetComponent<Animator>().enabled = false;
                monsterHoldBtn.GetComponent<Animator>().enabled = false;
                monsterBtn.GetComponent<RectTransform>().localScale = Vector3.one;
                monsterHoldBtn.GetComponent<RectTransform>().localScale = Vector3.one;
                if (isLoop)
                {
                    isCountDown = true;
                }
            }
        }

    }

    public void OnMonsterBtnClicked()
    {
        if (isPlaying || isCountDown) return;
        isCountDown = true;
        timeCountDown = 0f;
        noticeTimeCountDown.gameObject.SetActive(true);
        monsterBtn.GetComponent<Animator>().enabled = true;
        monsterHoldBtn.GetComponent<Animator>().enabled = true;
        if(PlayerData.tutorialSound == 0)
            TutorialSound.Instance.OnMonsterClicked();
    }
    public void OnExitMonsterBtn()
    {
        monsterBtn.GetComponent<Animator>().enabled = false;
        monsterHoldBtn.GetComponent<Animator>().enabled = false;
        monsterBtn.GetComponent<RectTransform>().localScale = Vector3.one;
        monsterHoldBtn.GetComponent<RectTransform>().localScale = Vector3.one;
        timeFill.fillAmount = 0f;
        timePlay = 0f;
        timeCountDown = 0f;
        isCountDown = false;
        isPlaying = false;
        played = false;
        timeInPlay.SetActive(false);
        SoundController.Instance.source.Stop();
        if(PlayerData.tutorialSound == 0)
        {
            TutorialSound.Instance.TutorialBack();
        }
    }    
    public void OnBackBtnClicked()
    {
        this.gameObject.SetActive(false);
        SoundController.Instance.source.Stop();
        if(PlayerData.tutorialSound == 0)
        {
            TutorialSound.Instance.EndTutorial();
        }
    }
    public void OnSettingBtnClicked()
    {
        PopUpMovement.Instance.ShowPopUp(settingPopUp);
    }

    public void OnLoopBtnClicked()
    {
        isLoop = !isLoop;
        if(isLoop)
        {
            timeWait = 0f;

        }
        else
        {
            timeWait = timeWaitList[waitIndex];
        }
        if (timeWait == 0f)
        {
            monsterBtn.gameObject.SetActive(false);
            monsterHoldBtn.gameObject.SetActive(true);
        }
        else
        {
            monsterHoldBtn.gameObject.SetActive(false);
            monsterBtn.gameObject.SetActive(true);
        }
        noticeInLoop.SetActive(isLoop);

    }

    public void OnChangeValueInDropDown(int index)
    {
        timeWait = timeWaitList[index];
        if(index == 0)
        {
            monsterBtn.gameObject.SetActive(false) ;
            monsterHoldBtn.gameObject.SetActive(true);
        }
        else
        {
            monsterHoldBtn.gameObject.SetActive(false);
            monsterBtn.gameObject.SetActive(true);
        }
        imageLabel.sprite = spriteForLabel[index];
        waitIndex = index;
        isLoop = false;
        ResetLoopAndTimePlay();
    }
}
