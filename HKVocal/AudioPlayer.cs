namespace HKVocals;
public static class AudioPlayer
{
    private static AudioClip GetAudioClip(string text)
    {
        Modding.Logger.Log(text);
        //TODO: Get the audio clip here
        
        return null;
    }
    
    
    public static bool TryPlayAudioFor(string convName)
    {
        HKVocals.instance.LogDebug($"Trying to play audio for {convName}");

        string text = Language.Language.Get(convName);
        var clip = GetAudioClip(text);
        PlayAudio(clip);
        return true;
    }
    
    public static bool TryPlayAudioFor(string convName, int convoNumber)
    {
        HKVocals.instance.LogDebug($"Trying to play audio for {convName}");

        string fullText = Language.Language.Get(convName);
        string textToPlay = fullText.Split(new[] { "<page>" }, StringSplitOptions.None)[convoNumber];
        var clip = GetAudioClip(textToPlay);
        PlayAudio(clip);
        return true;
    }
    
    private static void PlayAudio(AudioClip clip)
    {
        //if its still null create new audio source go
        if (HKVocals.instance.audioSource == null)
        {
            HKVocals.instance.CreateAudioSource();
        }

        var asrc = HKVocals.instance.audioSource;

        asrc.Stop();
        asrc.transform.localPosition = HeroController.instance != null 
            ? HeroController.instance.transform.localPosition :
            new Vector3(15f, 10f, 1f); //for monomon audio

        asrc.SetMixerGroup();

        MixerLoader.SetMixerVolume();
        asrc.PlayOneShot(clip, 1f);
    }

    public static bool IsPlaying() => HKVocals.instance.audioSource.isPlaying;
    public static void StopPlaying() => HKVocals.instance.audioSource.Stop();
}
