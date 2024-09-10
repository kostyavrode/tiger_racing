using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum SoundState
{
    OFF = 0,
    ON = 1
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource mainSourcePrefab;
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioSource[] otherSources;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public GameObject vibrationOnButton;
    public GameObject vibrationOffButton;
    private SoundState soundState;
    private SoundState musicState;
    public void Awake()
    {
        if (mainSource == null)
        {
            try
            {
                mainSource = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioSource>();
            }
            catch
            {
                mainSource = Instantiate(mainSourcePrefab);
            }
        }
        if (!PlayerPrefs.HasKey("SoundState"))
        {
            PlayerPrefs.SetInt("SoundState", 1);
            PlayerPrefs.SetInt("MusicState", 1);
            ChangeSoundState(SoundState.ON);
            ChangeMusicState(SoundState.ON);
        }
        else if (PlayerPrefs.GetInt("SoundState")==0)
        {
            ChangeSoundState(SoundState.OFF);
        }
        else
        {
            //ChangeMusicState(SoundState.ON);
            ChangeSoundState(SoundState.ON);
        }
        if (PlayerPrefs.GetInt("MusicState")==0)
        {
            ChangeMusicState(SoundState.OFF);
            musicOffButton.SetActive(true);
            musicOnButton.SetActive(false);
        }
        else
        {
            ChangeMusicState(SoundState.ON);
        }
        DontDestroyOnLoad(mainSource.gameObject);
    }
    public void ChangeSoundState(SoundState soundState)
    {
        this.soundState = soundState;
        switch (this.soundState)
        {
            case SoundState.OFF:
                PlayerPrefs.SetInt("SoundState", 0);
                foreach (AudioSource source in otherSources)
                {
                    source.volume = 0;
                    //mainSource.Pause();
                }
                break;
            case SoundState.ON:
                PlayerPrefs.SetInt("SoundState", 1);
                foreach (AudioSource source in otherSources)
                {
                    source.volume = 0.5f;
                    //mainSource.Play();
                }
                break;
            default:
                break;
        }
    }
    public void ChangeMusicState(SoundState soundState)
    {
        this.soundState = soundState;
        switch (this.soundState)
        {
            case SoundState.OFF:
                PlayerPrefs.SetInt("MusicState", 0);
                mainSource.Pause();
                break;
            case SoundState.ON:
                PlayerPrefs.SetInt("MusicState", 1);
                mainSource.Play();
                break;
            default:
                break;
        }
    }
}
