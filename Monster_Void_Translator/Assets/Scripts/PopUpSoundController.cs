using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSoundController : MonoBehaviour
{
    public RectTransform content;
    List<Button> monsterSounds = new List<Button>();
    public List<AudioClip> sounds;
    private void Awake()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            monsterSounds.Add(content.GetChild(i).GetComponent<Button>());
        }
        for(int i = 0; i < monsterSounds.Count; i++)
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
        var soundListSO = SoundController.Instance.monsterSounds;
        var soundList = soundListSO[index].monsterSounds;

        SoundController.Instance.PlaySoundInSoundPopUp(sounds[index]);
    }
}
