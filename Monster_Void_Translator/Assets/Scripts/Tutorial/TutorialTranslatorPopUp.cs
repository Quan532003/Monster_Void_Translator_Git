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
    [SerializeField] Text tutorialRecordAndPlayText;
    [SerializeField] Text selectMonsterText;
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
            tutorialRecordAndPlayText.gameObject.SetActive(true);
            tutorialRecordAndPlayText.text = "Hold to record";
            selectMonsterText.gameObject.SetActive(false);
            handMonsterDrag.SetActive(false);
        }));
    }
    public void SetActiveTutorialRecord(bool active)
    {
        handRecord.SetActive(active);
        tutorialRecordAndPlayText.gameObject.SetActive(active);
    }
    public void TutorialPlay()
    {
        coverRecord.SetActive(true);
        handRecord.SetActive(false);
        handPlay.SetActive(true);
        tutorialRecordAndPlayText.gameObject.SetActive(true);
        tutorialRecordAndPlayText.text = "Tap to listen";
    }
    public void SetActiveTutorialPlay(bool active)
    {
        handPlay.SetActive(active);
        tutorialRecordAndPlayText.gameObject.SetActive(active);
    }
    public void TutorialMonsterSelect()
    {
        monsterAndRecordCover.SetActive(false);
        handPlay.SetActive(false);
        tutorialRecordAndPlayText.gameObject.SetActive(false);
        top.SetSiblingIndex(1);
        StartCoroutine(waitForSecond(1f, ()=>
        {
            handMonsterDrag.SetActive(true);
            selectMonsterText.gameObject.SetActive(true);
            monsterSelectCover.SetActive(true);
            selectMonsterText.text = "Drag to choose monster";
        }));
        
    }
    public void SetActiveTutorialMonster(bool active)
    {
        handMonsterDrag.SetActive(active);
        selectMonsterText.gameObject.SetActive(active);
    }

    public void EndTurorial()
    {
        coverRecord.SetActive(false);

        handMonsterDrag.SetActive(false);
        selectMonsterText.gameObject.SetActive(false);
        monsterSelectCover.SetActive(false);
        handSelectMonster.SetActive(false);
        top.SetSiblingIndex(0);
        PlayerData.tutorialTrans = 1;
        monsterSelect.enabled = true;
        isTutoring = false;
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
        }
    }
    public void EndDrag()
    {
        if (PlayerData.tutorialTrans == 0)
        {
            monsterSelect.enabled = false;
            handMonsterDrag.SetActive(false);
            selectMonsterText.text = "Tap to choose monster";

            handSelectMonster.GetComponent<RectTransform>().position = FindMonsterInScreen();
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
