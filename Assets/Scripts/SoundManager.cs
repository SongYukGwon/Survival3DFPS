using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Sound
{
    public string name; //곡의 이름
    public AudioClip clip; // 곡
}


public class SoundManager : MonoBehaviour
{
    //공유자원으로 사용함.
    static public SoundManager instance;
    #region sigleton
    //싱글톤
    //싱글톤은 최초에 한번만 실행하면 되기에 Awake를 사용
    //객체 생성시 최초 실행
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    #endregion sigleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] efftetSounds;
    public Sound[] bgmSounds;

    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];    
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < efftetSounds.Length; i++)
        {
            if(_name == efftetSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if(!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = efftetSounds[i].name;
                        audioSourceEffects[i].clip = efftetSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }
            Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
        }
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if(playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("재생 중인 " + _name + "사운드가 없습니다.");
    }
}
