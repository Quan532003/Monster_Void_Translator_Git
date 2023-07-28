using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public List<Button> modeButtons = new List<Button>();
    public List<GameObject> popUps = new List<GameObject>();
    public Text modeText;
    int currentPopUp = 1;
    List<string> tittle = new List<string> { "Sound", "Translator", "Informations", "History" };
    public List<GameObject> activeBtn = new List<GameObject>();
    public List<GameObject> inActiveBtn = new List<GameObject>();
    [SerializeField] Image tittleImage;
    [SerializeField] List<Sprite> tittleSprites;
    [SerializeField] RectTransform modeTittle;
    List<int> sizeForMode = new List<int> { 400, 600, 700, 450 };
    [SerializeField] Image fillRecord;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        for (int i = 0; i < modeButtons.Count; i++)
        {
            int index = i;
            modeButtons[index].onClick.AddListener(() =>
            {
                OnModeBtnClicked(index);
            });
        }
        FillRandom();
    }
    void FillRandom()
    {
        float fill = (float)UnityEngine.Random.Range(0f, 1f);
        fillRecord.DOFillAmount(fill, 0.2f).OnComplete(() =>
        {
            FillRandom();
        });
    }
    void OnModeBtnClicked(int index)
    {
        if (index == currentPopUp) return;
        PopUpMovement.Instance.ChangePopUp(popUps[index], popUps[currentPopUp], index < currentPopUp);
        activeBtn[index].SetActive(true);
        inActiveBtn[index].SetActive(false);

        activeBtn[currentPopUp].SetActive(false);
        inActiveBtn[currentPopUp].SetActive(true);
        currentPopUp = index;
        tittleImage.sprite = tittleSprites[index];
        tittleImage.SetNativeSize();

        if(index == 0)
        {
            PopUpSoundController.Instance.SetLockSound();
            PopUpSoundController.Instance.SetSoundBtnClicked();
            if(PlayerData.tutorialSound == 0)
            {
                TutorialSound.Instance.TutorialSelectMonster();
            }
            RecordController.Instance.OnStopPlayBtnClicked();
        }
        if(index == 1)
        {
            PopUpTranslatorController.Instance.SetLockMonster();
            PopUpTranslatorController.Instance.SetClickMonsterBtn();
        }
        if(index == 2)
        {
            RecordController.Instance.OnStopPlayBtnClicked();
            PopUpInforController.Instance.SetLockBtn();
            PopUpInforController.Instance.SetBtnMonsterClicked();
            if(PlayerData.tutorialInfor == 0)
            {
                TutorialInfor.Instance.TutorialSelect();
            }
        }
        if(index == 3)
        {
            RecordController.Instance.OnStopPlayBtnClicked();
            RecordController.Instance.OnStopRecordBtnClicked();
            PopUpHistoryController.Instance.SetListRecord();
            if(PlayerData.tutorialHistory == 0)
            {
                TutorialHistory.Instance.TutorialSelect();
            }
        }

    }
}
