// Decompiled with JetBrains decompiler
// Type: BlastFurnaceConfig
// Assembly: BlastFurnace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70B3FF10-FF3B-4E63-9674-4D44D609F41B
// Assembly location: C:\Users\Alp D\Documents\Klei\OxygenNotIncluded\mods\Steam\1892052917\BlastFurnace.dll

using TUNING;
using UnityEngine;

namespace BurnersRevived
{
    internal class PetrolBurnerConfig : IBuildingConfig
    {
        public static string EFFECT = "Burns Petroleum to rapidly heat the tile directly above this building, outputting superhot CO2 and waste Polluted Water in the process. Dealing with the CO2 waste is important, as it may cause the Flare Stack to overheat and the Polluted Water waste to boil instantly";
        public const string ID = "petroleumburner";
        public const string NAME = "Petroleum Flare Stack";
        public const string DESC = "After many complaints by his friends about their equipment running out of power all the time, Otto came up with this Flare Stack to make a very large power plant instead of wasting all his time tuning the generators up.";
        public const float OXY_RATE = 0.8f;
        public const float CARBON_RATE = 4f;
        public const float EXHAUST_RATE = 5f;
        public const float EXHAUST_MIN_TEMP = 1073.15f;
        public const float MAX_HEATING_KDTU = 4800f;
        public const float TIME_TO_WARM_UP = 60f;
        public const float TIME_TO_COOL_DOWN = 90f;

        public override BuildingDef CreateBuildingDef()
        {
            int width = 3;
            int height = 3;
            string anim = "blastfurnace_kanim";
            int hitpoints = 100;
            float construction_time = 480f;
            string[] construction_materials = new string[2]
            {
        "Ceramic",
        "Steel"
            };
            EffectorValues tieR5 = NOISE_POLLUTION.NOISY.TIER5;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("petroleumburner", width, height, anim, hitpoints, construction_time, new float[2]
            {
        BUILDINGS.CONSTRUCTION_MASS_KG.TIER6[0],
        BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0]
            }, construction_materials, 2400f, BuildLocationRule.OnFoundationRotatable, BUILDINGS.DECOR.PENALTY.TIER1, tieR5, 0.2f);
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.Overheatable = true;
            buildingDef.OverheatTemperature = 0.15f;
            buildingDef.RequiresPowerInput = true;
            buildingDef.PowerInputOffset = new CellOffset(0, 0);
            buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
            buildingDef.InputConduitType = ConduitType.Liquid;
            //buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
            //buildingDef.OutputConduitType = ConduitType.Liquid;
            buildingDef.EnergyConsumptionWhenActive = 360f;
            buildingDef.ExhaustKilowattsWhenActive = 4f;
            buildingDef.SelfHeatKilowattsWhenActive = 4f;
            buildingDef.AudioCategory = "HollowMetal";
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Storage storage = go.AddComponent<Storage>();
            storage.capacityKg = 1480f;
            storage.allowItemRemoval = false;
            storage.showInUI = true;/*
            ManualDeliveryKG manualDeliveryKg = go.AddComponent<ManualDeliveryKG>();
            manualDeliveryKg.SetStorage(storage);
            manualDeliveryKg.requestedItemTag = SimHashes.RefinedCarbon.CreateTag();
            manualDeliveryKg.capacity = 400f;
            manualDeliveryKg.refillMass = 200f;
            manualDeliveryKg.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;*/
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.consumptionRate = 1f;
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.capacityTag = SimHashes.Petroleum.CreateTag();
            conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
            conduitConsumer.capacityKG = 80f;
            ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
            elementConverter.SetStorage(storage);
            elementConverter.showDescriptors = false;
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[1]
            {
        new ElementConverter.ConsumedElement(SimHashes.Petroleum.CreateTag(), 1f),
            };
            elementConverter.outputElements = new ElementConverter.OutputElement[2]
            {
        new ElementConverter.OutputElement(0.25f, SimHashes.CarbonDioxide, 1073.15f, false, false, 0.0f, 0.5f, 1f, byte.MaxValue, 0),
        new ElementConverter.OutputElement(0.375f, SimHashes.DirtyWater, 273.3f, true, false, 0.0f, 0.5f, 1f, byte.MaxValue, 0)
            };
            go.AddOrGet<PetrolBurner>();
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            //GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            //GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            //GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
        }
    }
}
