using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                   //UI ����� ����
using UnityEngine.Audio;                                //����� ����� ����
using System;
using UnityEngine.Rendering;                                           //�迭�� ���ٽ� ����� ���ؼ� 

[Serializable]
public class Sound                                      //���� Ŭ���� �̸��� �����ϱ� ���� ���
{
    public string name;                                 //�̸��� �����ش�.
    public AudioClip clip;                              //����� Ŭ��
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }         //static ����Ͽ� �̱��� ���

    //����� Clip �迭 
    public Sound[] musicSounds;                         //����� ���� ����
    public Sound[] sfxSound;

    public AudioSource musicSource;                     //����� ����� �ҽ� ����
    public AudioSource sfxSource;

    //����� �ɼ�
    public AudioMixer mixer;                            //����� ����� �ͼ�
    public Slider musicSlider;                          //�ɼ�â���� ����� MusicSlider
    public Slider sfxSlider;                            //�ɼ�â���� ����� SFXSlider

    const string MIXER_MUSIC = "MusicVolume";           //����� Param �� (Music)
    const string MIXER_SFX = "SFXVolume";               //����� Param �� (SFX)

    //����� �г� ����
    public GameObject AudioPanel;
    public bool AudioPanelFlag = false;                 //����� OnOff �Ǿ��ִ��� ���θ� �˻�

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSlider.value = 1.0f;                                   //���۽� 1�� ����
        sfxSlider.value = 1.0f;                                     //���۽� 1�� ����

        musicSlider.onValueChanged.AddListener(SetMusicVolume);     //�����̴��� ���� ���� �Ǿ����� �ش� �Լ��� ȣ�� �Ѵ�. 
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);     //�����̴��� ���� ���� �Ǿ����� �ش� �Լ��� ȣ�� �Ѵ�. 
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);       //Log10������ 0 ~ 80 �� ������ �����Ҽ� �ְ� ���ش�.    
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);       //Log10������ 0 ~ 80 �� ������ �����Ҽ� �ְ� ���ش�. 
    }

    public void PlayMusic(string name)                      //����� BGM �Լ� ���� 
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);     //Array ���ٽ� name�� ã�Ƽ� ��ȯ

        if (sound == null)                           //name���ε� wav�� ���� ��� Log ���
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = sound.clip;              //������ ����� �ҽ��� CLIP�� �ִ´�.    
            musicSource.Play();                         //�Ϲ� Play ���
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSound, x => x.name == name);     //Array ���ٽ� name�� ã�Ƽ� ��ȯ

        if (sound == null)                           //name���ε� wav�� ���� ��� Log ���
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);                         //�Ϲ� Play ���
        }
    }

    public void PanelOnOff(bool type)                                   //����� �ɼ� �г�
    {
        AudioPanelFlag = type;                                          //���� Ÿ�԰� ����ȭ
        AudioPanel.SetActive(type);                                     //�г��� Ű�� ����. 
    }
}
