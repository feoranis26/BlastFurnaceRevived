// Decompiled with JetBrains decompiler
// Type: BlastFurnace
// Assembly: BlastFurnace, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70B3FF10-FF3B-4E63-9674-4D44D609F41B
// Assembly location: C:\Users\Alp D\Documents\Klei\OxygenNotIncluded\mods\Steam\1892052917\BlastFurnace.dll

using KSerialization;
using System;
using UnityEngine;

namespace BurnersRevived
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class PetrolBurner: KMonoBehaviour, ISim200ms, ISaveLoadable
    {
        private static Tag Petroleum = SimHashes.Petroleum.CreateTag();
        private static Tag Oxygen = SimHashes.Oxygen.CreateTag();
        [Serialize]
        public float current_heat = 0.0f;
        private bool wasOperational = false;
        [MyCmpReq]
        private Operational operational;
        [MyCmpReq]
        private ElementConverter converter;
        [MyCmpReq]
        private KSelectable selectable;
        [MyCmpReq]
        private Storage storage;
        [MyCmpReq]
        private ConduitConsumer consumer;
        [MyCmpReq]
        private Rotatable rotatable;
        private MeterController meter;
        private Guid needs_fuel;
        private Guid heating;
        private int heatedCell;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.UpdateOperationalState();
            CellOffset offset;
            switch (this.rotatable.GetOrientation())
            {
                case Orientation.Neutral:
                    offset = new CellOffset(0, 5);
                    break;
                case Orientation.R90:
                    offset = new CellOffset(5, 0);
                    break;
                case Orientation.R180:
                    offset = new CellOffset(0, -5);
                    break;
                case Orientation.R270:
                    offset = new CellOffset(-5, 0);
                    break;
                default:
                    offset = new CellOffset(0, 5);
                    break;
            }
            this.heatedCell = Grid.OffsetCell(this.GetComponent<Building>().GetCell(), offset);
            this.meter = new MeterController((KAnimControllerBase)this.GetComponent<KBatchedAnimController>(), "dial_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new Vector3(0.0f, 0.0f, -0.1f), new string[3]
            {
        "dial_target",
        "dial",
        "arrow"
            });
        }

        public void UpdateMeter()
        {
            this.meter.SetPositionPercent(Mathf.Clamp01(this.current_heat / 4800f));
        }

        public void Sim200ms(float dt)
        {
            if (this.operational.IsOperational && this.converter.HasEnoughMassToStartConverting())
                this.operational.SetActive(true, false);
            else
                this.operational.SetActive(false, false);
            if (this.operational.IsActive)
            {
                if ((double)this.current_heat < 3600.0)
                {
                    this.current_heat += 80f * dt;
                    this.current_heat = Math.Min(this.current_heat, 4800f);
                }
            }
            else
            {
                this.current_heat -= 53.33333f * dt;
                this.current_heat = Math.Max(0.0f, this.current_heat);
            }
            Element element = Grid.Element[this.heatedCell];
            if ((double)this.current_heat > 0.0 && !element.IsVacuum)
                SimMessages.ModifyEnergy(this.heatedCell, this.current_heat * dt, 3773.15f, SimMessages.EnergySourceID.Burner);
            this.UpdateMeter();
            this.SetStatus();
        }

        public void UpdateOperationalState()
        {
        }

        public bool HasEnoughFuel()
        {
            return this.converter.HasEnoughMass(PetrolBurner.Petroleum);
        }

        public void SetStatus()
        {
            if (this.operational.IsActive)
            {
                if (this.needs_fuel != Guid.Empty)
                    this.needs_fuel = this.selectable.RemoveStatusItem(this.needs_fuel, false);
            }
            else if (this.needs_fuel == Guid.Empty)
                this.needs_fuel = this.selectable.AddStatusItem(PetrolBurnerStatuses.NeedsFuel, (object)this);
            if (!(this.heating == Guid.Empty))
                return;
            this.heating = this.selectable.AddStatusItem(PetrolBurnerStatuses.Heating, (object)this);
        }
    }
}
