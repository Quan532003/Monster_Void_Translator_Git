using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTranslatorPopUp : MonoBehaviour
{
    [SerializeField] GameObject monsterAndRecordCover;
    [SerializeField] GameObject monsterSelectCover;
    [SerializeField] GameObject handRecord;
    [SerializeField] GameObject handPlay;
    [SerializeField] GameObject handMonsterDrag;
    [SerializeField] Text tutorialRecord;
    [SerializeField] Text tutorialPlay;
    [SerializeField] Text dragText;
    [SerializeField] Text selectText;
    [SerializeField] Button recordBtn;
    [SerializeField] Button playBtn;
    public static TutorialTranslatorPopUp Instance;
    [SerializeField] GameObject coverRecord;
    public bool isTutoring;
    [SerializeField] RectTransform top;
    [SerializeField] ScrollRect monsterSelect;
    [SerializeField] GameObject handSelectMonster;
    [SerializeField] RectTransform content;
    private void Awake()
    {
        isTutoring = false;
        Instance = this;
    }
    public void TutorialRecord()
    {
        isTutoring = true;
        StartCoroutine(waitForSecond(1f, () =>
        {
            monsterAndRecordCover.SetActive(true);
            monsterSelectCover.SetActive(false);
            handRecord.SetActive(true);
            handPlay.SetActive(false);
            tutorialRecord.gameObject.SetActive(true);
            tutorialRecord.text = "Hold to record";
            dragText.gameObject.SetActive(false);
            selectText.gameObject.SetActive(false);
            handMonsterDrag.SetActive(false);
        }));
    }
    public void SetActiveTutorialRecord(bool active)
    {
        handRecord.SetActive(active);
        tutorialRecord.gameObject.SetActive(active);
    }
    public void TutorialPlay()
    {
        coverRecord.SetActive(true);
        handRecord.SetActive(false);
        handPlay.SetActive(true);
        recordBtn.enabled = false;
        tutorialRecord.gameObject.SetActive(false);
        tutorialPlay.gameObject.SetActive(true);
        tutorialPlay.text = "Tap to listen";
    }
    public void SetActiveTutorialPlay(bool active)
    {
        handPlay.SetActive(active);
        tutorialPlay.gameObject.SetActive(active);
        playBtn.enabled = false;
    }
    public void TutorialMonsterSelect()
    {
        monsterAndRecordCover.SetActive(false);
        handPlay.SetActive(false);
        tutorialPlay.gameObject.SetActive(false);
        tutorialRecord.gameObject.SetActive(false);
        top.SetSiblingIndex(1);
        StartCoroutine(waitForSecond(1f, ()=>
        {
            handMonsterDrag.SetActive(true);
            monsterSelectCover.SetActive(true);
            dragText.gameObject.SetActive(true);
            dragText.text = "Drag to choose monster";
        }));
        
    }
    public void SetActiveTutorialMonster(bool active)
    {
        handMonsterDrag.SetActive(active);
        dragText.gameObject.SetActive(active);
    }

    public void EndTurorial()
    {
        coverRecord.SetActive(false);

        handMonsterDrag.SetActive(false);
        dragText.gameObject.SetActive(false);
        selectText.gameObject.SetActive(false);
        monsterSelectCover.SetActive(false);
        handSelectMonster.SetActive(false);
        top.SetSiblingIndex(0);
        PlayerData.tutorialTrans = 1;
        monsterSelect.enabled = true;
        isTutoring = false;
        playBtn.enabled = true;
        recordBtn.enabled = true;
    }

    IEnumerator waitForSecond(float time, Action A)
    {
        yield return new WaitForSeconds(time);
        A?.Invoke();
    }


    public void BeginDrag()
    {
        if(PlayerData.tutorialTrans == 0)
        {
            handMonsterDrag.SetActive(false);
            dragText.gameObject.SetActive(false);
        }
    }
    public void EndDrag()
    {
        if (PlayerData.tutorialTrans == 0)
        {
            monsterSelect.enabled = false;
            handMonsterDrag.SetActive(false);
            selectText.gameObject.SetActive(true);
            dragText.gameObject.SetActive(false);
            selectText.text = "Tap to choose monster";

            handSelectMonster.GetComponent<RectTransform>().position = FindMonsterInScreen();
            selectText.GetComponent<RectTransform>().position = FindMonsterInScreen() - new Vector3(0, 1, 0);
            var pos = selectText.GetComponent<RectTransform>().position;
            pos.x = 0;
            selectText.GetComponent<RectTransform>().position = pos;
            handSelectMonster.SetActive(true);
        }
        
    }

    Vector3 FindMonsterInScreen()
    {
        var pos = content.anchoredPosition.x;
        for(int i = 0; i < content.childCount; i++)
        {
            var monster = content.GetChild(i).GetComponent<RectTransform>();
            if (monster.anchoredPosition.x + pos >=100 && monster.anchoredPosition.x + pos <= 800 && !monster.GetChild(0).gameObject.activeInHierarchy)
            {
                return monster.position;
            }
        }
        return Vector3.one;
    }

}
