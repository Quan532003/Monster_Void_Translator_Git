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
        StartCoroutine(waitForSecond(0.3f, () =>
        {
            coverHistory.SetActive(true);
            coverRecord.gameObject.SetActive(true);
            var rect = viewPos.sizeDelta;
            var rectCoverRecord = coverRecord.sizeDelta;
            rectCoverRecord.y = rect.y - 200;
            coverRecord.sizeDelta = rectCoverRecord;
            handSelect.SetActive(true);
            textSelect.SetActive(true);
        }));
    }

    public void TutorialShare()
    {
        handPlay.SetActive(false);
        textPlay.SetActive(false);
        handShare.SetActive(true);
        textShare.SetActive(true);
        shareBtn.enabled = true;
        playBtn.enabled = false;
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
        StartCoroutine(waitForSecond(1f, () =>
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
        eraseBtn.enabled = true;
    }

    public void EndTutorial()
    {
        handErase.SetActive(false);
        textErase.SetActive(false);
        coverHistory.SetActive(false);
        coverRecord.gameObject.SetActive(false);
        PlayerData.tutorialHistory = 1;
    }

    IEnumerator waitForSecond(float time, Action A)
    {
        yield return new WaitForSeconds(time);
        A?.Invoke();
    }
}
