using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();
        private readonly Dictionary<EquipSlot, string> _equipmentNames = new()
        {
            [EquipSlot.Weapon] = "Weapon",
            [EquipSlot.Armour] = "Armour",
            [EquipSlot.Helmet] = "Helmet",
            [EquipSlot.RangeWeapon] = "RangeWeapon"
        };

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {
        }

        public override uint GetUnitDamage()
        {
            uint totalDamage = BaseDamage;

            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon && !item.IsBroken)
            {
                totalDamage += weapon.Damage;
            }
            else if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeItem) && rangeItem is RangeWeapon rangeWeapon && !rangeItem.IsBroken)
            {
                totalDamage += rangeWeapon.Damage;
            }

            return totalDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items.ToList();
            foreach (var economicItem in items.OfType<EconomicItem>())
            {
                if (economicItem is HealthPotion or Gold)
                {
                    UseEconomicItem(economicItem);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                TryEquipItem(equipItem);
                return;
            }
            base.AddItemToInventory(item);
        }

        private bool TryEquipItem(EquipItem newItem)
        {
            var slot = newItem.Slot;

            if (_equipment.TryGetValue(slot, out var oldItem))
            {
                if (oldItem != null && !oldItem.IsBroken)
                {
                    base.AddItemToInventory(oldItem);
                    Console.WriteLine($"Unequipped {_equipmentNames[slot]}: {oldItem.Name}");
                }
                _equipment.Remove(slot);
            }

            _equipment[slot] = newItem;
            Console.WriteLine($"Equipped {_equipmentNames[slot]}: {newItem.Name}");
            return true;
        }

        public bool UseEconomicItem(string itemName)
        {
            var economicItem = Inventory.Items
                .OfType<EconomicItem>()
                .FirstOrDefault(item => item.Name == itemName);

            if (economicItem == null)
            {
                Console.WriteLine($"You don't have a {itemName}!");
                return false;
            }

            return UseEconomicItem(economicItem);
        }

        private bool UseEconomicItem(EconomicItem economicItem)
        {
            switch (economicItem)
            {
                case HealthPotion healthPotion:
                    Heal(healthPotion.HealthRestore);
                    Console.WriteLine($"{Name} used a Health Potion! Health: {Health}/{MaxHealth}");
                    return Inventory.TryRemove(economicItem);

                case Gold:
                    Console.WriteLine($"{Name} collected Gold!");
                    return Inventory.TryRemove(economicItem);

                case Grindstone:
                    return UseGrindstone(economicItem);

                default:
                    return false;
            }
        }

        private bool UseGrindstone(EconomicItem economicItem)
        {
            var equipmentToRepair = _equipment.Values
                .Where(item => item != null && item.Durability < item.MaxDurability)
                .ToList();

            if (equipmentToRepair.Count == 0)
            {
                Console.WriteLine("All equipment is already in perfect condition!");
                return false;
            }

            foreach (var item in equipmentToRepair)
            {
                item.Repair(GameConstants.GrindstoneRepairAmount);
            }

            Inventory.TryRemove(economicItem);
            Console.WriteLine("Grindstone has been used!");
            return true;
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            uint totalDefence = 0;

            foreach (var slot in _equipment.Values)
            {
                if (slot is Armour armour && !slot.IsBroken)
                {
                    totalDefence += armour.Defence;
                    if (!slot.ReduceDurability(1))
                    {
                        Console.WriteLine($"Armour {slot.Name} is broken!");
                        _equipment.Remove(slot.Slot);
                    }
                }
                else if (slot is Helmet helmet && !slot.IsBroken)
                {
                    totalDefence += helmet.Defence;
                    if (!slot.ReduceDurability(1))
                    {
                        Console.WriteLine($"Helmet {slot.Name} is broken!");
                        _equipment.Remove(slot.Slot);
                    }
                }
            }

            if (_equipment.TryGetValue(EquipSlot.Weapon, out var weapon) && weapon != null && !weapon.IsBroken)
            {
                if (!weapon.ReduceDurability(1))
                {
                    Console.WriteLine($"Weapon {weapon.Name} is broken!");
                    _equipment.Remove(EquipSlot.Weapon);
                }
            }

            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeWeapon) && rangeWeapon != null && !rangeWeapon.IsBroken)
            {
                if (!rangeWeapon.ReduceDurability(1))
                {
                    Console.WriteLine($"RangeWeapon {rangeWeapon.Name} is broken!");
                    _equipment.Remove(EquipSlot.RangeWeapon);
                }
            }

            float damageReduction = Math.Min(totalDefence / 100f, 0.5f);
            return (uint)(damage * (1 - damageReduction));
        }

        protected override void DamageReceiveHandler()
        {
            Console.WriteLine($"{Name} is damaged! Health: {Health}/{MaxHealth}");
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine($"Damage: {BaseDamage}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }

            builder.AppendLine("\n--- Equipment ---");
            foreach (var slot in Enum.GetValues<EquipSlot>())
            {
                if (_equipment.TryGetValue(slot, out var item) && item != null)
                {
                    string status = item.IsBroken ? " (BROKEN)" : $" (Durability: {item.Durability}/{item.MaxDurability})";
                    builder.AppendLine($"{_equipmentNames[slot]}: {item.Name}{status}");
                }
                else
                {
                    builder.AppendLine($"{_equipmentNames[slot]}: Empty");
                }
            }

            builder.AppendLine("\n--- Inventory ---");
            if (items.Count == 0)
            {
                builder.AppendLine("Empty");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    string amount = items[i].Stackable ? $" x{items[i].Amount}" : "";
                    string type = items[i] is EquipItem ? " [Equipment]" : " [Consumable]";
                    builder.AppendLine($"{items[i].Name}{amount}{type}");
                }
            }

            return builder.ToString();
        }

        public void ShowEquipmentStatus()
        {
            Console.WriteLine("\n=== Equipment Status ===");
            foreach (var slot in Enum.GetValues<EquipSlot>())
            {
                if (_equipment.TryGetValue(slot, out var item) && item != null)
                {
                    string status = item.IsBroken ? "Broken" : $"{item.Durability}/{item.MaxDurability}";
                    Console.WriteLine($"{_equipmentNames[slot]}: {item.Name} (Durability: {status})");
                }
                else
                {
                    Console.WriteLine($"{_equipmentNames[slot]}: Empty");
                }
            }
            Console.WriteLine();
        }
    }
}   