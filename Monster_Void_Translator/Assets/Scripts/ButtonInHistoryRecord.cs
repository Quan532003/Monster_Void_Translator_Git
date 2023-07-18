using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInHistoryRecord : MonoBehaviour
{
    public static ButtonInHistoryRecord Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void OnPlayBtnInHistoryRecord(int index)
    {
        var monsterIndex = PlayerData.GetMonsterIndex(index);
        var soundIndex = PlayerData.GetSoundIndex(index);
        SoundController.Instance.PlaySound(monsterIndex, soundIndex);
        Debug.Log(monsterIndex + " " + soundIndex);
    }
    public void OnShareBtnInHistoryRecord(int index)
    {
        Debug.Log(index);
    }

    public void OnEraseBtnInHistoryRecord(int index)
    {
        int numberRecord = PlayerData.numberRecord;
        for(int i = index; i < numberRecord; i++)
        {
            PlayerData.SetRecordDay(i, Helper.GetRecordDay());
            PlayerData.SetRecordLength(i, PlayerData.GetRecordLength(i + 1));
            PlayerData.SetRecordName(i, PlayerData.GetRecordName(i + 1));
            PlayerData.SetMonterIndex(i, PlayerData.GetMonsterIndex(i + 1));
            PlayerData.SetSoundIndex(i, PlayerData.GetSoundIndex(i + 1));
        }
        PlayerData.numberRecord--;
        PopUpHistoryController.Instance.SetListRecord();
    }
}
