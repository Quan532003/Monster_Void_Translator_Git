using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public List<SoundMonsterSO> monsterSounds;
    public static SoundController Instance;
    public AudioSource source;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    public void PlaySoundInSoundPopUp(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    public void PlaySound(int monsterIndex, int soundIndex)
    {
        var audioClip = monsterSounds[monsterIndex].monsterSounds[soundIndex];
        source.clip = audioClip;
        source.Play();
    }
}
