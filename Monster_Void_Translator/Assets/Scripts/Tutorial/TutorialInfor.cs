using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TutorialInfor : MonoBehaviour
{
    [SerializeField] GameObject coverSelect;
    [SerializeField] ScrollRect seclectScroll;
    [SerializeField] List<Button> monster;
    [SerializeField] Text tutorialSelect;
    [SerializeField] GameObject handSelect;
    [SerializeField] GameObject coverback;
    [SerializeField] Text backText;
    [SerializeField] GameObject backHand;

    public static TutorialInfor Instance;
    private void Awake()
    {
        Instance = this;
        PlayerData.tutorialInfor = 0;
    }

    public void TutorialSelect()
    {
        seclectScroll.enabled = false;
        for(int i = 0; i < monster.Count; i++)
        {
            monster[i].enabled = false;
        }
        StartCoroutine(waitForSecond(0.5f, () =>
        {
            coverSelect.SetActive(true);
            handSelect.SetActive(true);
            tutorialSelect.gameObject.SetActive(true);
            tutorialSelect.text = "Tap to choose monster";
        }));
    }


    public void Tutorialback()
    {
        seclectScroll.enabled = true;
        for (int i = 0; i < monster.Count; i++)
        {
            monster[i].enabled = true;
        }
        coverSelect.SetActive(false);
        handSelect.SetActive(false);
        tutorialSelect.gameObject.SetActive(false);
        StartCoroutine(waitForSecond(2f, () =>
        {
            backHand.SetActive(true);
            coverback.SetActive(true);
            backText.gameObject.SetActive(true);
            backText.text = "Tap to back";
        }));
    }


    public void EndTutorial()
    {
        backHand.SetActive(false);
        backText.gameObject.SetActive(false);
        coverback.SetActive(false);
        PlayerData.tutorialInfor = 1;
    }
    IEnumerator waitForSecond(float time, Action A)
    {
        yield return new WaitForSeconds(time);
        A?.Invoke();
    }
}
