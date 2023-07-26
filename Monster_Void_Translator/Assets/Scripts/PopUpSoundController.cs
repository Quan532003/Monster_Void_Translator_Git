using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSoundController : MonoBehaviour
{
    public RectTransform content;
    List<Button> monsterSounds = new List<Button>();
    List<GameObject> lockSound = new List<GameObject>();
    public static PopUpSoundController Instance;
    public SoundPlayEachMonster SoundEachMonster;
    private void Awake()
    {
        Instance = this;
        for(int i = 0; i < content.childCount; i++)
        {
            monsterSounds.Add(content.GetChild(i).GetComponent<Button>());
        }
        for (int i = 0; i < monsterSounds.Count; i++)
        {
            lockSound.Add(monsterSounds[i].GetComponent<RectTransform>().GetChild(2).gameObject);
        }
        SetLockSound();
        SetSoundBtnClicked();
    }
    public void SetLockSound()
    {
        for(int i = 0; i < lockSound.Count; i++)
        {
            lockSound[i].SetActive(false);
        }
        var index = Helper.ConvertFromMonsterLockToInt();
        for (int i = 0; i < index.Count; i++)
        {
            lockSound[index[i]].SetActive(true);
        }
    }
    public void SetSoundBtnClicked()
    {
        for (int i = 0; i < monsterSounds.Count; i++)
        {
            int index = i;
            monsterSounds[i].onClick.AddListener(() =>
            {
                OnMonsterBtnClicked(index);
            });
        }
    }
    void OnMonsterBtnClicked(int index)
    {
        if (!lockSound[index].activeInHierarchy)
        {
            PlayerData.monsterIndexInSound = index;
            SoundEachMonster.gameObject.SetActive(true);
            SoundEachMonster.ResetLoopAndTimePlay();
            SoundEachMonster.SetName();
            SoundEachMonster.SetSprite();
        }
        else
        {
            lockSound[index].SetActive(false);
            PlayerData.monsterIndexInSound = index;
            SoundEachMonster.gameObject.SetActive(true); 
            SoundEachMonster.ResetLoopAndTimePlay();
            SoundEachMonster.SetName();
            SoundEachMonster.SetSprite();
            Helper.SetLockMonster(index);
        }
        if(PlayerData.tutorialSound == 0)
        {
            TutorialSound.Instance.TutorialPlaySound();
        }
    }
}
