using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHistory : MonoBehaviour
{
    [SerializeField] GameObject coverHistory;
    [SerializeField] RectTransform coverRecord;
    [SerializeField] RectTransform viewPos;
    public Button shareBtn, playBtn, eraseBtn;
    [SerializeField] GameObject handSelect, handShare, handPlay, handErase;
    [SerializeField] GameObject textSelect, textShare, textPlay, textErase;
    public static TutorialHistory Instance;

    public bool isHandPlayActive()
    {
        return handPlay.activeInHierarchy;
    }
    private void Awake()
    {
        Instance = this;
    }

    public void SetEnable()
    {
        shareBtn.enabled = false;
        playBtn.enabled = false;
        eraseBtn.enabled = false;
    }

    public void TutorialSelect()
    {
            coverHistory.SetActive(true);
            coverRecord.gameObject.SetActive(true);
            var rect = viewPos.rect.height;
            var rectCoverRecord = coverRecord.sizeDelta;
            rectCoverRecord.y = rect - 200;
            coverRecord.sizeDelta = rectCoverRecord;
            handSelect.SetActive(true);
            textSelect.SetActive(true);
    }

    public void TutorialShare()
    {
        handPlay.SetActive(false);
        textPlay.SetActive(false);
        handShare.SetActive(true);
        textShare.SetActive(true);
        playBtn.enabled = false;
        StartCoroutine(waitForSecond(0.5f, ()=>
        {
            InActiveShare();
            TutorialErase();
            StartCoroutine(waitForSecond(0.5f, EndTutorial));
        }));
    }

    public void InActiveShare()
    {
        handShare.SetActive(false);
        textShare.SetActive(false);
    }

    public void TutorialPlay()
    {
        var size = coverRecord.sizeDelta;
        size.y -= 300;
        coverRecord.sizeDelta = size;
        handSelect.SetActive(false);
        textSelect.SetActive(false);
        StartCoroutine(waitForSecond(0.5f, () =>
        {
            handPlay.SetActive(true);
            textPlay.SetActive(true);
            playBtn.enabled = true;
        }));

    }
    public void InActivePlay()
    {
        handPlay.SetActive(false);
        textPlay.SetActive(false);
    }

    public void TutorialErase()
    {
        handPlay.SetActive(false);
        textPlay.SetActive(false);
        handErase.SetActive(true);
        textErase.SetActive(true);
        playBtn.enabled = false;
    }

    public void EndTutorial()
    {
        handErase.SetActive(false);
        textErase.SetActive(false);
        coverHistory.SetActive(false);
        coverRecord.gameObject.SetActive(false);
        playBtn.enabled = true;
        eraseBtn.enabled = true;
        shareBtn.enabled = true;
        PlayerData.tutorialHistory = 1;
        PopUpHistoryController.Instance.SetClickedBtn();
    }

    IEnumerator waitForSecond(float time, Action A)
    {
        yield return new WaitForSeconds(time);
        A?.Invoke();
    }
}
