using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRecord
{
    public static void SaveDataRecord(int length, int monsterIndex, int soundIndex)
    {
        int numberRecord = PlayerData.numberRecord;

        Debug.Log(numberRecord + " " + length + " " + monsterIndex + " " + soundIndex);
        PlayerData.SetRecordDay(numberRecord, Helper.GetRecordDay());
        PlayerData.SetRecordLength(numberRecord, length);
        PlayerData.SetRecordName(numberRecord, "Record " + numberRecord + 1);
        PlayerData.SetMonterIndex(numberRecord, monsterIndex);
        PlayerData.SetSoundIndex(numberRecord, soundIndex);
    }
}
