using Language;
using Satchel;
using UnityEngine.Audio;
using Newtonsoft.Json;

namespace HKVocals;

public sealed class HKVocals: Mod, IGlobalSettings<GlobalSettings>, ILocalSettings<SaveSettings>, ICustomMenuMod
{
    public static GlobalSettings _globalSettings { get; private set; } = new ();
    public void OnLoadGlobal(GlobalSettings s) => _globalSettings = s;
    public GlobalSettings OnSaveGlobal() => _globalSettings;
    public static SaveSettings _saveSettings { get; private set; } = new ();
    public void OnLoadLocal(SaveSettings s) => _saveSettings = s;
    public SaveSettings OnSaveLocal() => _saveSettings;
        
    public AudioSource audioSource;
    internal static HKVocals instance;
    public static NonBouncer CoroutineHolder;
    
    public HKVocals() : base("Hallownest Vocalized")
    {
        OnMenuStyleTitle.AfterOrig.SetTitle += AddCustomBanner;
    }
    
    private static string Version = "0.0.1.0";
    public override string GetVersion() => Version;

    public override void Initialize()
    {
        instance = this;
        
        MixerLoader.LoadAssetBundle();

        MajorFeatures.SpecialAudio.Hook();
        MajorFeatures.NPCDialogue.Hook();
        MajorFeatures.MuteOriginalAudio.Hook();
        MajorFeatures.DampenAudio.Hook();
        MajorFeatures.DreamNailDialogue.Hook();
        MajorFeatures.AutoScroll.Hook();
        MajorFeatures.ScrollLock.Hook();
        MajorFeatures.UITextAudio.Hook();

        UIManager.EditMenus += UI.AudioMenu.AddAudioSliderAndSettingsButton;

        Hooks.PmFsmBeforeStartHook += AddFSMEdits;

        CoroutineHolder = new GameObject("HK Vocals Coroutine Holder").AddComponent<NonBouncer>();
        Object.DontDestroyOnLoad(CoroutineHolder);
        CreateAudioSource();
        
        Log("HKVocals initialized");

    }
    public void CreateAudioSource()
    { 
        LogDebug("creating new asrc"); 
        GameObject audioGO = new GameObject("HK Vocals Audio");
        audioSource = audioGO.AddComponent<AudioSource>();
        
        audioSource.SetMixerGroup();
        Object.DontDestroyOnLoad(audioGO);
    }

    private void AddFSMEdits(PlayMakerFSM fsm)
    {
        string sceneName = MiscUtils.GetCurrentSceneName();
        string gameObjectName = fsm.gameObject.name;
        string fsmName = fsm.FsmName;

        if (FSMEditData.FsmEdits.TryGetValue(new HKVocalsFsmData(sceneName, gameObjectName, fsmName), out var action_1))
        {
            action_1.TryInvokeActions(fsm);
        }
        if (FSMEditData.FsmEdits.TryGetValue(new HKVocalsFsmData(gameObjectName, fsmName), out var action_2))
        {
            action_2.TryInvokeActions(fsm);
        }
        if (FSMEditData.FsmEdits.TryGetValue(new HKVocalsFsmData(fsmName), out var action_3))
        {
            action_3.TryInvokeActions(fsm);
        }
    }
    
    private void AddCustomBanner(OnMenuStyleTitle.Delegates.Params_SetTitle args)
    {
        //only change for english language. i doubt people on other languages want it
        if (Language.Language.CurrentLanguage() == LanguageCode.EN)
        {
            args.self.Title.sprite = AssemblyUtils.GetSpriteFromResources("Resources.Title.png");
        }
        
    }
    
    public static void DoLogDebug(object s) => instance.LogDebug(s);
    public static void DoLog(object s) => instance.Log(s);
    public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggleDelegates) => UI.ModMenu.CreateModMenuScreen(modListMenu);
    public bool ToggleButtonInsideMenu => false;
}
