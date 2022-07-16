using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Sound
{
    public string name; //���� �̸�
    public AudioClip clip; // ��
}


public class SoundManager : MonoBehaviour
{
    //�����ڿ����� �����.
    static public SoundManager instance;
    #region sigleton
    //�̱���
    //�̱����� ���ʿ� �ѹ��� �����ϸ� �Ǳ⿡ Awake�� ���
    //��ü ������ ���� ����
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
                Debug.Log("��� ���� AudioSource�� ������Դϴ�.");
                return;
            }
            Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
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
        Debug.Log("��� ���� " + _name + "���尡 �����ϴ�.");
    }
}
