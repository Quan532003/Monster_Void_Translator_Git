using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTranslatorController : MonoBehaviour
{
    public Image mainMonster;
    public Text monsterName;

    public RectTransform contentsInScroll;
    List<Button> monsterBtns = new List<Button>();

    public List<Sprite> monsterAvatars = new List<Sprite>();

    public Button loopBtn;
    public GameObject noticeLoop;
    public GameObject noticeTimer;
    public Button timerBtn;

    public Dropdown timerSelectDropDown;
    public static PopUpTranslatorController Instance;
    public float waitTime = 0f;
    List<float> waitTimeInSelect = new List<float> { 0, 15, 30, 45, 60, 120, 300}; //15s, 30s, 45s, 1m, 2m, 5m
    public bool isLoop = false;
    
    private void Awake()
    {
        Instance = this;
        for(int i = 0; i < contentsInScroll.childCount; i++)
        {
            monsterBtns.Add(contentsInScroll.GetChild(i).GetComponent<Button>());
        }
        for(int i = 0; i < monsterBtns.Count; i++)
        {
            int index = i;
            monsterBtns[index].onClick.AddListener(() =>
            {
                OnMonsterBtnClicked(index);
            });
        }
        loopBtn.onClick.AddListener(OnLoopBtnClicked);
        timerBtn.onClick.AddListener(OnTimerBtnClicked);
        timerSelectDropDown.onValueChanged.AddListener(OnChangeValueInDropDown);
    }
    void OnMonsterBtnClicked(int index)
    {
        mainMonster.sprite = monsterAvatars[index];
        PlayerData.currentMonster = index;
        monsterName.text = "Monster" + index;
        RecordController.Instance.playCover.SetActive(true);
        RecordController.Instance.recordCover.SetActive(false);
    }
    public void OnLoopBtnClicked()
    {
        noticeLoop.SetActive(!noticeLoop.activeInHierarchy);
        if (noticeLoop.activeInHierarchy) noticeTimer.SetActive(false);
        isLoop = !isLoop;
        waitTime = 0f;
    }

    public void OnTimerBtnClicked()
    {
        noticeTimer.SetActive(!noticeTimer.activeInHierarchy);
        if (noticeTimer.activeInHierarchy) noticeLoop.SetActive(false);
    }

    public void OnChangeValueInDropDown(int index)
    {
        waitTime = waitTimeInSelect[index];
    }
}
