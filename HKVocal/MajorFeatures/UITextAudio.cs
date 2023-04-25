﻿namespace HKVocals.MajorFeatures;
public class UITextAudio
{
    private static bool HunterNotesUnlocked = true;
    public static bool OpenShopMenu = false;
    public static bool OpenInvMenu = false;
    public static bool ShopMenuClosed = true;
    public static bool InvMenuClosed = true;
    
    public static void Hook()
    {
        FSMEditData.AddGameObjectFsmEdit("Enemy List", "Item List Control", PlayJournalText );
        FSMEditData.AddGameObjectFsmEdit ("Inv", "Update Text", PlayInventoryText );
        FSMEditData.AddGameObjectFsmEdit ("Charms", "Update Text", PlayCharmText );
        FSMEditData.AddGameObjectFsmEdit ("Item List", "Item List Control", PlayShopText );
        FSMEditData.AddGameObjectFsmEdit ("Shop Menu", "shop_control", ShopMenuOpenClose );
        FSMEditData.AddGameObjectFsmEdit ("Inventory", "Inventory Control", InventoryOpenClose );
    }
    
    public static void PlayCharmText(PlayMakerFSM fsm)
    {
        fsm.AddFsmMethod("Change Text", () => { fsm.PlayUIText("Convo Desc", "UI",  UIAudioType.Other); });
    }

    public static void PlayInventoryText(PlayMakerFSM fsm)
    {
        fsm.AddFsmMethod("Change Text", () => { fsm.PlayUIText("Convo Desc", "UI", UIAudioType.Other); });
    }

    public static void PlayJournalText(PlayMakerFSM fsm)
    {
        fsm.AddFsmMethod("Display Kills", () => { HunterNotesUnlocked = false; });
        fsm.AddFsmMethod("Get Notes", () => { HunterNotesUnlocked = true; });

        fsm.AddFsmMethod("Get Details", () =>
        {
            if (HunterNotesUnlocked == true)
            {
                fsm.PlayUIText("Item Notes Convo", "Journal", UIAudioType.Other);
            }
            else if (HunterNotesUnlocked == false)
            {
                fsm.PlayUIText("Item Desc Convo", "Journal", UIAudioType.Other);
            }
        });
    }


    public static void ShopMenuOpenClose(PlayMakerFSM fsm)
    {
        //Checks when you open the shop keeper menu
        fsm.AddFsmMethod("Open Window", () => { OpenShopMenu = true; ShopMenuClosed = false; });
        //Checks when you close a shop keeper menu
        fsm.AddFsmMethod("Down", () => { AudioPlayer.StopPlaying(); ShopMenuClosed = true; OpenShopMenu = false; });
    }

    public static void InventoryOpenClose(PlayMakerFSM fsm)
    {
        fsm.AddFsmMethod("Open", () => { OpenInvMenu = true; InvMenuClosed = false; });
        fsm.AddFsmMethod("Close", () => { AudioPlayer.StopPlaying(); InvMenuClosed = true; OpenInvMenu = false; });
    }

    public static void PlayShopText(PlayMakerFSM fsm)
    {
        fsm.AddFsmMethod("Get Details Init", () => { fsm.PlayUIText("Item Desc Convo", "UI", UIAudioType.Shop); });
        fsm.AddFsmMethod("Get Details", () => { fsm.PlayUIText("Item Desc Convo", "UI", UIAudioType.Shop); });
    }
}

public enum UIAudioType
{
    Shop,
    Other
}