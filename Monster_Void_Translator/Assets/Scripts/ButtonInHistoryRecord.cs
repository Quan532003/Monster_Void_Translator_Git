using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

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
        if(PlayerData.tutorialHistory == 0)
        {
            TutorialHistory.Instance.InActivePlay();
        }
    }
    public void OnShareBtnInHistoryRecord(int index)
    {
        if (PlayerData.tutorialHistory == 0)
        {
            TutorialHistory.Instance.InActiveShare();
        }
        StartCoroutine(TakeScreenshotAndShare(index));
    }
    private IEnumerator TakeScreenshotAndShare(int index)
    {
        yield return new WaitForEndOfFrame();
        byte[] wavData = Helper.EncodeToWAV(SoundController.Instance.monsterSounds[PlayerData.GetMonsterIndex(index)].monsterSounds[PlayerData.GetSoundIndex(index)]);

        string filePath = Path.Combine(Application.temporaryCachePath, "shared_audio.wav");
        File.WriteAllBytes(filePath, wavData);
        new NativeShare().AddFile(filePath)
        .SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://github.com/yasirkula/UnityNativeShare")
        .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
        .Share();
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
