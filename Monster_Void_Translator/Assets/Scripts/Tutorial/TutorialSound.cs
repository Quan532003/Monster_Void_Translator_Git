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
    public static TutorialSound Instance;
    private void Awake()
    {
        Instance = this;
    }



    public void TutorialSelectMonster()
    {
        monsterSelectScroll.enabled = false;
        for(int i = 0; i < soundCanNotUse.Count; i++)
        {
            soundCanNotUse[i].enabled = false;
        }

    }

    IEnumerator waitForSecond(float time, Action A)
    {
        yield return new WaitForSeconds(1f);
        A?.Invoke();
    }

}
