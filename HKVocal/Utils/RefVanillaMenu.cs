using UnityEngine.Audio;
using Satchel;

namespace HKVocals;

/// <summary>
/// contains references to the various gameobjects in the audio menu in options
/// </summary>
public static class RefVanillaMenu
{
    private static MenuScreen _audioMenuScreen;
    private static GameObject _audioMenuScreenContent;
    private static GameObject _musicVolumeSliderHolder;
    private static GameObject _musicVolumeSlider;
    private static AudioMixer _masterMixer;
    private static GameObject _defaultAudioSettingsButton;

    #region AudioMenu
    public static MenuScreen AudioMenuScreen
    {
        get
        {
            if (_audioMenuScreen == null)
            {

                _audioMenuScreen = UIManager.instance.UICanvas.Find("AudioMenuScreen").GetComponent<MenuScreen>();
            }

            return _audioMenuScreen;
        }
    } 
    public static GameObject AudioMenuScreenContent
    {
        get
        {
            if (_audioMenuScreenContent == null)
            {

                _audioMenuScreenContent = AudioMenuScreen.Find("Content");
            }

            return _audioMenuScreenContent;
        }
    } 
    public static GameObject MusicVolumeSliderHolder
    {
        get
        {
            if (_musicVolumeSliderHolder == null)
            {
                _musicVolumeSliderHolder = AudioMenuScreenContent.Find("MusicVolume");
            }

            return _musicVolumeSliderHolder;
        }
    }
    
    public static GameObject MusicVolumeSlider
    {
        get
        {
            if (_musicVolumeSlider == null)
            {
                _musicVolumeSlider = MusicVolumeSliderHolder.Find("MusicSlider");
            }

            return _musicVolumeSlider;
        }
    }
    public static AudioMixer MasterMixer
    {
        get
        {
            if (_masterMixer == null)
            {
                _masterMixer = AudioMenuScreenContent.Find("MasterVolume").Find("MasterSlider").GetComponent<MenuAudioSlider>().masterMixer;
            }

            return _masterMixer;
        }
    }
    
    public static GameObject DefaultAudioSettingsButton
    {
        get
        {
            if (_defaultAudioSettingsButton == null)
            {
                _defaultAudioSettingsButton = AudioMenuScreen.Find("Controls/DefaultsButton");
            }

            return _defaultAudioSettingsButton;
        }
    }
    
    #endregion
}