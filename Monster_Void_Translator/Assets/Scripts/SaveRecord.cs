using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRecord
{
    public static void SaveDataRecord(int length, int monsterIndex, int soundIndex)
    {
        int numberRecord = PlayerData.numberRecord;

        PlayerData.SetRecordDay(numberRecord, Helper.GetRecordDay());
        PlayerData.SetRecordLength(numberRecord, length);
        PlayerData.SetRecordName(numberRecord, "Record " + (PlayerData.idRecord++ + 1));
        PlayerData.SetMonterIndex(numberRecord, monsterIndex);
        PlayerData.SetSoundIndex(numberRecord, soundIndex);
    }
}
