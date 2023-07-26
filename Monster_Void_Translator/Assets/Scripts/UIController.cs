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
        modeText.text = tittle[index];
        if(index == 0)
        {
            PopUpSoundController.Instance.SetLockSound();
            PopUpSoundController.Instance.SetSoundBtnClicked();
            if(PlayerData.tutorialSound == 0)
            {
                TutorialSound.Instance.TutorialSelectMonster();
            }
        }
        if(index == 1)
        {
            PopUpTranslatorController.Instance.SetLockMonster();
            PopUpTranslatorController.Instance.SetClickMonsterBtn();
        }
        if(index == 2)
        {
            PopUpInforController.Instance.SetLockBtn();
            PopUpInforController.Instance.SetBtnMonsterClicked();
            if(PlayerData.tutorialInfor == 0)
            {
                TutorialInfor.Instance.TutorialSelect();
            }
        }
        if(index == 3)
        {
            PopUpHistoryController.Instance.SetListRecord();
            if(PlayerData.tutorialHistory == 0)
            {
                TutorialHistory.Instance.TutorialSelect();
            }
        }

    }
}
