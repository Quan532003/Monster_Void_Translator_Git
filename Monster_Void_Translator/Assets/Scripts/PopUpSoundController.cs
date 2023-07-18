using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSoundController : MonoBehaviour
{
    public RectTransform content;
    List<Button> monsterSounds = new List<Button>();
    public List<AudioClip> sounds;
    List<GameObject> lockSound = new List<GameObject>();
    public static PopUpSoundController Instance;
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
            SoundController.Instance.PlaySoundInSoundPopUp(sounds[index]);
        }
        else
        {
            lockSound[index].SetActive(false);
            SoundController.Instance.PlaySoundInSoundPopUp(sounds[index]);
            Helper.SetLockMonster(index);
        }
    }
}
