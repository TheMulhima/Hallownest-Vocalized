namespace HKVocals;

public class GlobalSettings
{
    public int volume = 10;
    public bool autoScroll = false;
    public MajorFeatures.AutoScroll.ScrollSpeed scrollSpeed = MajorFeatures.AutoScroll.ScrollSpeed.Normal;
    public bool dnDialogue = true;
    public bool scrollLock = false;
    public bool dampenAudio = true;
}
public class SaveSettings
{
    public List<string> FinishedConvos = new List<string>();
}
