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
    List<string> tittle = new List<string> { "Sound", "Translator", "Infor", "History" };

    public List<Text> modeTextInBtn;
    private void Awake()
    {
        modeTextInBtn[1].color = Color.yellow;
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

        modeTextInBtn[index].color = Color.yellow;
        modeTextInBtn[currentPopUp].color = Color.black;
        currentPopUp = index;
        modeText.text = tittle[index];
        if(index == 0)
        {
            PopUpSoundController.Instance.SetLockSound();
            PopUpSoundController.Instance.SetSoundBtnClicked();
            Debug.Log("Set" + index);
        }
        if(index == 1)
        {
            Debug.Log("Set" + index);
            PopUpTranslatorController.Instance.SetLockMonster();
            PopUpTranslatorController.Instance.SetClickMonsterBtn();
        }
        if(index == 2)
        {
            Debug.Log("Set" + index);
            PopUpInforController.Instance.SetLockBtn();
            PopUpInforController.Instance.SetBtnMonsterClicked();
        }
        if(index == 3)
        {
            Debug.Log("Set" + index);
            PopUpHistoryController.Instance.SetListRecord();
        }
    }
}
