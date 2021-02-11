// Decompiled with JetBrains decompiler
// Type: BlastFurnacePatch
// Assembly: BlastFurnace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70B3FF10-FF3B-4E63-9674-4D44D609F41B
// Assembly location: C:\Users\Alp D\Documents\Klei\OxygenNotIncluded\mods\Steam\1892052917\BlastFurnace.dll

using Harmony;
using STRINGS;
using SkyLib;

namespace BurnersRevived
{
    public class PetrolBurnerPatch
    {
        public static bool didStartUp_Building;
        public static bool didStartUp_Db;

        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
                SkyLib.Logger.StartLogging();
                OniUtils.AddStatusItem("NeedsFuel", "NAME", (string)BUILDING.STATUSITEMS.NEEDRESOURCEMASS.NAME, "BUILDING");
                OniUtils.AddStatusItem("NeedsFuel", "TOOLTIP", (string)BUILDING.STATUSITEMS.NEEDRESOURCEMASS.TOOLTIP, "BUILDING");
                OniUtils.AddStatusItem("Heating", "NAME", "Internal heating rate: {HeatAmount}", "BUILDING");
                OniUtils.AddStatusItem("Heating", "TOOLTIP", "This blast furnace is currently heating the targt tile.", "BUILDING");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Path
        {
            public static void Prefix()
            {
                if (PetrolBurnerPatch.didStartUp_Building)
                    return;
                OniUtils.AddBuildingStrings("petroleumburner", PetrolBurnerConfig.NAME, PetrolBurnerConfig.DESC, PetrolBurnerConfig.EFFECT);
                OniUtils.AddBuildingToBuildMenu((HashedString)"Utilities", "petroleumburner");
                PetrolBurnerPatch.didStartUp_Building = true;
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                if (PetrolBurnerPatch.didStartUp_Db)
                    return;
                PetrolBurnerPatch.didStartUp_Db = true;
            }
        }
    }
}
