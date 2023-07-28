using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSound : MonoBehaviour
{
    [SerializeField] GameObject coverSound;
    [SerializeField] ScrollRect monsterSelectScroll;
    [SerializeField] List<Button> soundCanNotUse = new List<Button>();
    [SerializeField] GameObject handInSelect;
    [SerializeField] Text selectText;
    [SerializeField] GameObject handInPlaySound;
    [SerializeField] Text playText;
    [SerializeField] Text textBack;
    [SerializeField] GameObject handInBack;
    [SerializeField] GameObject coverInPlaySound;
    [SerializeField] GameObject coverInPlaySoundBack;
    [SerializeField] RectTransform top;
    [SerializeField] Animator monsterAnimator;
    public static TutorialSound Instance;
    private void Awake()
    {
        Instance = this;
        monsterAnimator.enabled = false;
    }



    public void TutorialSelectMonster()
    {
        monsterSelectScroll.enabled = false;
        for(int i = 0; i < soundCanNotUse.Count; i++)
        {
            soundCanNotUse[i].enabled = false;
        }
        coverSound.SetActive(true);
        handInSelect.SetActive(true);
        selectText.gameObject.SetActive(true);
    }

    public void TutorialPlaySound()
    {
        monsterSelectScroll.enabled = true;
        for (int i = 0; i < soundCanNotUse.Count; i++)
        {
            soundCanNotUse[i].enabled = true;
        }
        selectText.gameObject.SetActive(false);
        coverSound.SetActive(false);
        handInSelect.SetActive(false);
        coverInPlaySound.SetActive(true);
        handInPlaySound.SetActive(true);
        playText.gameObject.SetActive(true);
        playText.text = "Hold to listen";

    }
    public void OnMonsterClicked()
    {
        handInPlaySound.SetActive(false);
        playText.gameObject.SetActive(false);
        monsterAnimator.enabled = true;
    }
    public void TutorialBack()
    {
        monsterAnimator.enabled = false;
        top.SetSiblingIndex(3);
        coverInPlaySound.SetActive(false);
        coverInPlaySoundBack.SetActive(true);
        handInBack.SetActive(true);
        textBack.gameObject.SetActive(true);
        textBack.text = "Tap to back";
    }
    public void EndTutorial()
    {
        top.SetSiblingIndex(1);
        monsterAnimator.enabled = false;
        coverInPlaySoundBack.SetActive(false);
        coverInPlaySound.SetActive(false);
        handInPlaySound.SetActive(false);
        handInBack.SetActive(false);
        textBack.gameObject.SetActive(false);
        PlayerData.tutorialSound = 1;
    }


    IEnumerator waitForSecond(float time, Action A)
    {
        yield return new WaitForSeconds(time);
        A?.Invoke();
    }

}
