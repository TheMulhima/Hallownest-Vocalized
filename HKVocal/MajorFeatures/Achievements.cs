﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using SFCore;
using UnityEngine;

namespace HKVocals.MajorFeatures;

public static class Achievements
{
    public static void Hook() 
    {
        AchievementHelper.AddAchievement("ImpatientLemm",MiscUtils.LoadSprite("Resources/Impatient.png",Rect.zero,Vector2.zero, 1,1),"Impatient","Leave a relic in Lemm’s deposit box.",false);
        AchievementHelper.AddAchievement("DisdainGrub",MiscUtils.LoadSprite("Resources/Disdain.png",Rect.zero,Vector2.zero, 1,1),"Disdain","Read the dreams of a particularly ungrateful Grub.",false);
        AchievementHelper.AddAchievement("KindnessPaleLurker",MiscUtils.LoadSprite("Resources/Kindness.png",Rect.zero,Vector2.zero,1,1),"Kindness","Show the Pale Lurker a new perspective on life.",false);
        AchievementHelper.AddAchievement("LastLaughOrdeal",MiscUtils.LoadSprite("Resources/Last_Laugh.png",Rect.zero,Vector2.zero, 1,1),"Last Laugh","Hit the lever below The Eternal Ordeal’s Zote statue.",false);
        AchievementHelper.AddAchievement("AlubafarDreamnail",MiscUtils.LoadSprite("Resources/Alubafar.png",Rect.zero,Vector2.zero, 1,1),"Alubafar","Listen to what an Aluba has to say",true);

        AchievementHelper.AddAchievement("CompendiumVocalization",MiscUtils.LoadSprite("Resources/Full_Compendium.png",Rect.zero, Vector2.zero,1,1),"Compendium Vocalization","Listen to every line of vocalized dialogue in Hallownest.",false);
        AchievementHelper.AddAchievement("Consideration",MiscUtils.LoadSprite("Resources/All_Dialogue.png",Rect.zero, Vector2.zero,1,1),"Consideration","Listen to every word of Hallownest’s living inhabitants.",false);
        AchievementHelper.AddAchievement("Ambition",MiscUtils.LoadSprite("Resources/All_Dreams.png",Rect.zero, Vector2.zero,1,1),"Ambition","Uncover every dream, of bug and spirit alike.",false);
        AchievementHelper.AddAchievement("Chronology",MiscUtils.LoadSprite("Resources/All_Lore_Tablets.png",Rect.zero, Vector2.zero,1,1),"Chronology","Find every lore tablet buried under this dead Kingdom.",false);
        AchievementHelper.AddAchievement("Acquisition",MiscUtils.LoadSprite("Resources/All_Items_and_Jrounal.png",Rect.zero, Vector2.zero,1,1),"Acquisition","Review every item and journal entry there is to acquire.",false);




        ModHooks.LanguageGetHook += AchLang;
        On.EnemyDreamnailReaction.RecieveDreamImpact += Aluba;
    }

    private static string AchLang(string key, string sheettitle, string orig)
    {
        return key switch
        {
                "Impatient" => "Impatient",
                "Leave a relic in Lemm’s deposit box." => "Leave a relic in Lemm’s deposit box.",
                "Disdain" => "Disdain",
                "Read the dreams of a particularly ungrateful Grub." => "Read the dreams of a particularly ungrateful Grub.",
                "Kindness" => "Kindness",
                "Show the Pale Lurker a new perspective on life." => "Show the Pale Lurker a new perspective on life.",
                "Last Laugh" => "Last Laugh",
                "Hit the lever below The Eternal Ordeal’s Zote statue." => "Hit the lever below The Eternal Ordeal’s Zote statue.",
                "Alubafar" => "Alubafar",
                "Listen to what an Aluba has to say" => "Listen to what an Aluba has to say",
                "Compendium Vocalization" => "Compendium Vocalization",
                "Listen to every line of vocalized dialogue in Hallownest." => "Listen to every line of vocalized dialogue in Hallownest.",
                "Consideration" => "Consideration",
                "Listen to every word of Hallownest’s living inhabitants." => "Listen to every word of Hallownest’s living inhabitants.",
                "Ambition" => "Ambition",
                "Uncover every dream, of bug and spirit alike." => "Uncover every dream, of bug and spirit alike.",
                "Chronology" => "Chronology",
                "Find every lore tablet buried under this dead Kingdom." => "Find every lore tablet buried under this dead Kingdom.",
                "Acquisition" => "Acquisition",
                "Review every item and journal entry there is to acquire." => "Review every item and journal entry there is to acquire.",
                _ => orig
        };
    }

    private static void Aluba(On.EnemyDreamnailReaction.orig_RecieveDreamImpact orig, EnemyDreamnailReaction self)
    {
        if(self.gameObject.name == "Aluba")
            GameManager.instance.AwardAchievement("AlubafarDreamnail");
        orig(self);
    }
}
