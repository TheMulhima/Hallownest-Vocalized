using System.Text.RegularExpressions;

namespace HKVocals.MajorFeatures;

public static class DreamNailDialogue
{
    private static GameObject lastDreamnailedEnemy;
    
    public delegate void OnPlayDreamDialogueHandler();

    public static event OnPlayDreamDialogueHandler OnPlayDreamDialogue;

    public static void Hook()
    {
        ModHooks.LanguageGetHook += PlayDreamNailDialogue;
        OnEnemyDreamnailReaction.AfterOrig.Start += AddCancelDreamDialogueOnDeath;
        OnEnemyDreamnailReaction.BeforeOrig.ShowConvo += SetLastDreamNailedEnemy;
    }

    private static string PlayDreamNailDialogue(string key, string sheetTitle, string orig) {
        
        // Make sure this is dreamnail text or ABD text or dont play audio if the dn dialogue toggle is off
        if (lastDreamnailedEnemy == null || !HKVocals._globalSettings.dnDialogue) return orig;
        
        // Prevent it from running again incorrectly
        lastDreamnailedEnemy = null;

        MixerLoader.SetSnapshot(Snapshots.Dream);

        bool didPlay = AudioPlayer.TryPlayAudioFor(key, sheetTitle);

        if (didPlay) {
            OnPlayDreamDialogue?.Invoke();
        }
        
        return orig;
    }

    private static void SetLastDreamNailedEnemy(OnEnemyDreamnailReaction.Delegates.Params_ShowConvo args) 
    {
        lastDreamnailedEnemy = args.self.gameObject;
    }

    private static void AddCancelDreamDialogueOnDeath(OnEnemyDreamnailReaction.Delegates.Params_Start args)
    {
        args.self.gameObject.AddComponent<CancelDreamDialogueOnDeath>();
    }
}