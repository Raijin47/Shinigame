using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings: MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _volumeSFX;
    [SerializeField] private Slider _volumeMusic;
    [SerializeField] private Animator _switch;
    private bool isSound;

    private readonly string MasterSave = "MasterEnabled";
    private readonly string SFXSave = "SFXVolume";
    private readonly string MusicSave = "MusicVolume";

    private readonly string Master = "Master";
    private readonly string Music = "Music";
    private readonly string SFX = "SFX";

    private readonly string SwitchON = "Switch On";
    private readonly string SwitchOFF = "Switch Off";

    private void Start()
    {
        isSound = PlayerPrefs.GetInt(MasterSave, 1) == 1;
        SoundSwitch();
        _volumeMusic.value = PlayerPrefs.GetFloat(MusicSave, 1);
        _volumeSFX.value = PlayerPrefs.GetFloat(SFXSave, 1);
    }
    private void SoundSwitch()
    {
        if (isSound)
        {
            _mixer.audioMixer.SetFloat(Master, 0);
            _switch.Play(SwitchON);
        }
        else
        {
            _mixer.audioMixer.SetFloat(Master, -80);
            _switch.Play(SwitchOFF);
        }
    }
    public void SwitchSound()
    {
        isSound = !isSound;

        SoundSwitch();

        PlayerPrefs.SetInt(MasterSave, isSound ? 1 : 0);
    }
    public void ChangeSFX(float volume)
    {
        _mixer.audioMixer.SetFloat(SFX, Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat(SFXSave, volume);
    }
    public void ChangeMusic(float volume)
    {
        _mixer.audioMixer.SetFloat(Music, Mathf.Lerp(-80, 0, volume));

        PlayerPrefs.SetFloat(MusicSave, volume);
    }
}