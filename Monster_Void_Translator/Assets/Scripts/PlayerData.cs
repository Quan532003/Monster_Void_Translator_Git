using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData
{
    const string CurrentMonster = "currentMonster";
    const string Vibration = "vibration";
    const string RecordDay = "RecordDay";
    const string RecordName = "RecordName";
    const string RecordLength = "RecordLength";
    const string MonsterIndex = "MonsterIndex";
    const string SoundIndex = "SoundIndex";
    const string NumberRecordHistory = "NumberRecord";
    const string IdRecord = "IdRecord";
    const string LockMonster = "LockMonster";
    const string NoAD = "NoAD";
    const string Rate = "Rate";
    const string MonsterIndexInSoundPopUp = "MonsterInSound";
    const string TutorialTrans = "TutorialTrans";
    const string TutorialSound = "TutorialSound";
    const string TutorialHistory = "TutorialHistory";
    const string TutorialInfor = "TutorialInfor";


    public static int tutorialTrans
    {
        get
        {
            return PlayerPrefs.GetInt(TutorialTrans, 0);
        }
        set
        {
            PlayerPrefs.SetInt(TutorialTrans, value);
        }
    }
    public static int tutorialSound
    {
        get
        {
            return PlayerPrefs.GetInt(TutorialSound, 0);
        }
        set
        {
            PlayerPrefs.SetInt(TutorialSound, value);
        }
    }
    public static int tutorialHistory
    {
        get
        {
            return PlayerPrefs.GetInt(TutorialHistory, 0);
        }
        set
        {
            PlayerPrefs.SetInt(TutorialHistory, value);
        }
    }
    public static int tutorialInfor
    {
        get
        {
            return PlayerPrefs.GetInt(TutorialInfor, 0);
        }
        set
        {
            PlayerPrefs.SetInt(TutorialInfor, value);
        }
    }

    public static int idRecord
    {
        set
        {
            PlayerPrefs.SetInt(IdRecord, value);
        }
        get
        {
            return PlayerPrefs.GetInt(IdRecord, 0);
        }
    }
    public static int monsterIndexInSound
    {
        set
        {
            PlayerPrefs.SetInt(MonsterIndexInSoundPopUp, value);
        }
        get
        {
            return PlayerPrefs.GetInt(MonsterIndexInSoundPopUp, -1);
        }
    }
    public static int noAD
    {
        set
        {
            PlayerPrefs.SetInt(NoAD, value);
        }
        get
        {
            return PlayerPrefs.GetInt(NoAD, 0);
        }
    }

    public static int rate
    {
        set
        {
            PlayerPrefs.SetInt(Rate, value);
        }
        get
        {
            return PlayerPrefs.GetInt(Rate, 0);
        }
    }
    public static string lockMonster
    {
        set
        {
            PlayerPrefs.SetString(LockMonster, value);
        }
        get
        {
            return PlayerPrefs.GetString(LockMonster, "2,3,4,5,10,16");
        }
    }
    public static int numberRecord
    {
        set
        {
            PlayerPrefs.SetInt(NumberRecordHistory, value);
        }
        get
        {
            return PlayerPrefs.GetInt(NumberRecordHistory, 0);
        }
    }
    public static void SetRecordDay(int index, string id)
    {
        PlayerPrefs.SetString(RecordDay + index, id);
    }
    public static string GetRecordDay(int index)
    {
        return PlayerPrefs.GetString(RecordDay + index, "");
    }

    public static void SetRecordName(int index, string name)
    {
        PlayerPrefs.SetString(RecordName + index, name);
    }

    public static string GetRecordName(int index)
    {
        return PlayerPrefs.GetString(RecordName + index, "");
    }

    public static void SetRecordLength(int index, int length)
    {
        PlayerPrefs.SetInt(RecordLength + index, length);
    }

    public static int GetRecordLength(int index)
    {
        return PlayerPrefs.GetInt(RecordLength + index, 0);
    }

    public static void SetMonterIndex(int index, int type)
    {
        PlayerPrefs.SetInt(MonsterIndex + index, type);
    }
    public static int GetMonsterIndex(int index)
    {
        return PlayerPrefs.GetInt(MonsterIndex + index, 0);
    }
    public static void SetSoundIndex(int index, int value)
    {
        PlayerPrefs.SetInt(SoundIndex + index, value);
    }

    public static int GetSoundIndex(int index)
    {
        return PlayerPrefs.GetInt(SoundIndex + index, 0);
    }
    public static int vibration
    {
        set
        {
            PlayerPrefs.SetInt(Vibration, value);
        }
        get
        {
            return PlayerPrefs.GetInt(Vibration, 1);
        }
    }
    public static int currentMonster
    {
        set
        {
            PlayerPrefs.SetInt(CurrentMonster, value);
        }
        get
        {
            return PlayerPrefs.GetInt(CurrentMonster, 0);
        }
    }


}
