// Decompiled with JetBrains decompiler
// Type: StoragePod.StoragePodConfig
// Assembly: StoragePod, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 09A3E785-E834-4FE6-93EB-3F349F3ADA8C
// Assembly location: C:\Users\Alp D\Documents\Klei\OxygenNotIncluded\mods\Steam\1873476551\StoragePod.dll

using System.Collections.Generic;
using TUNING;
using UnityEngine;
using Harmony;
using SkyLib;

namespace BurnersRevived
{
    internal class StoragePod: IBuildingConfig
    {
        public static string Effect = "Stores the Solid resources of your choosing. Compact and can be built anywhere.";
        public const string ID = "StoragePodConfig";
        public const string DisplayName = "Storage Pod";
        public const string Description = "Now you, too, can store things in pods.";
        public static bool didStartupBuilding = false;

        public override BuildingDef CreateBuildingDef()
        {
            string id = nameof(StoragePod);
            int width = 1;
            int height = 1;
            string anim = "storagePod_kanim";
            int hitpoints = 30;
            float construction_time = 10f;
            float[] tieR2 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
            string[] refinedMetals = MATERIALS.REFINED_METALS;
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR2, refinedMetals, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
            buildingDef.Floodable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.Overheatable = false;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
            Prioritizable.AddRef(go);
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.allowItemRemoval = true;
            storage.showDescriptor = true;
            List<Tag> tagList = new List<Tag>();
            tagList.AddRange((IEnumerable<Tag>)STORAGEFILTERS.NOT_EDIBLE_SOLIDS);
            storage.storageFilters = tagList;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
            go.AddOrGet<StorageLocker>();
            go.GetComponent<Storage>().capacityKg = 5000;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }
        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Path
        {
            public static void Prefix()
            {
                if (StoragePod.didStartupBuilding)
                    return;
                OniUtils.AddBuildingStrings("StoragePodConfig", "Storage Pod", "Now you, too, can store things in pods.", StoragePod.Effect);
                OniUtils.AddBuildingToBuildMenu((HashedString)"Base", "StoragePodConfig", (string)null);
                StoragePod.didStartupBuilding = true;
            }
        }
    }
}
