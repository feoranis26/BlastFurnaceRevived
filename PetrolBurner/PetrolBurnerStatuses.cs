// Decompiled with JetBrains decompiler
// Type: BlastFurnaceStatuses
// Assembly: BlastFurnace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70B3FF10-FF3B-4E63-9674-4D44D609F41B
// Assembly location: C:\Users\Alp D\Documents\Klei\OxygenNotIncluded\mods\Steam\1892052917\BlastFurnace.dll

using STRINGS;
using System;
using System.Collections.Generic;

namespace BurnersRevived
{
    public class PetrolBurnerStatuses
    {
        private static Tag Petroleum = SimHashes.Petroleum.CreateTag();
        public static StatusItem NeedsFuel = new StatusItem(nameof(NeedsFuel), "BUILDING", "status_item_need_resource", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022)
        {
            resolveStringCallback = (Func<string, object, string>)((str, data) =>
            {
                PetrolBurner blastFurnace = (PetrolBurner)data;
                if (!blastFurnace.HasEnoughFuel())
                    return (PetrolBurnerStatuses.Format((Tag)PetrolBurnerStatuses.Petroleum.ProperName()) + "\n");
                return null;
            })
        };
        public static StatusItem Heating = new StatusItem(nameof(Heating), "BUILDING", (string)null, StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022)
        {
            resolveStringCallback = (Func<string, object, string>)((str, data) =>
            {
                float currentHeat = ((PetrolBurner)data).current_heat;
                return str.Replace("{HeatAmount}", GameUtil.GetFormattedHeatEnergyRate(currentHeat * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic));
            })
        };

        private static string Format(Tag tag)
        {
            return string.Format((string)BUILDING.STATUSITEMS.NEEDRESOURCEMASS.LINE_ITEM, (object)tag.ProperName());
        }
    }
}
