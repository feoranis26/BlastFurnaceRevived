// Decompiled with JetBrains decompiler
// Type: SteamTurbineConfig3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 597C0323-093D-411A-AD17-7F3B85DCB105
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\OxygenNotIncluded\OxygenNotIncluded_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TUNING;
using UnityEngine;
using Harmony;

namespace BurnersRevived
{
    public class PowerTweakSteamTurbine
    {
        [HarmonyPatch(typeof(SteamTurbineConfig2), "CreateBuildingDef")]
        public class PatchSteamTurbinePower
        {
            public static void Postfix(ref BuildingDef __result)
            {
                __result.GeneratorWattageRating = 5000f;
                __result.GeneratorBaseCapacity = 5000f;
                //return __result;
            }
        }
        [HarmonyPatch(typeof(SteamTurbineConfig2), "DoPostConfigureComplete")]
        public class PatchSteamTurbinePumpRate
        {
            public static void Postfix(ref GameObject go)
            {
                SteamTurbine steamTurbine = go.AddOrGet<SteamTurbine>();
                steamTurbine.pumpKGRate = 10f;
            }
        }
    }

}