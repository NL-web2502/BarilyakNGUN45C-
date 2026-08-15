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

            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon) 
            {
                return BaseDamage + weapon.Damage;
            }
            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeItem) && rangeItem is RangeWeapon rangeWeapon && !rangeItem.IsBroken)
            {
                totalDamage += rangeWeapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i] is EconomicItem economicItem) 
                {
                    UseEconomicItem(economicItem);
                    Inventory.TryRemove(items[i]);
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem && _equipment.TryAdd(equipItem.Slot, equipItem)) 
            {
                if (!TryEquipItem(equipItem))
                {
                    base.AddItemToInventory(item);
                }
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
                    Console.WriteLine($"Снят {_equipmentNames[slot]}: {oldItem.Name}");
                }
                _equipment.Remove(slot);
            }

            _equipment[slot] = newItem;
            Console.WriteLine($"Экипирован {_equipmentNames[slot]}: {newItem.Name}");
            return true;
        }

        public bool UseGrindstone()
        {
            
            Item? grindstone = null;
            foreach (var item in Inventory.Items)
            {
                if (item is Grindstone)
                {
                    grindstone = item;
                    break;
                }
            }

            if (grindstone == null)
            {
                Console.WriteLine("You have not a Grindstone!");
                return false;
            }

            bool anyRepaired = false;

            if (_equipment.TryGetValue(EquipSlot.Weapon, out var weapon) && weapon != null && weapon.Durability < weapon.MaxDurability)
            {
                weapon.Repair(weapon.MaxDurability);
                anyRepaired = true;
            }

           
            if (_equipment.TryGetValue(EquipSlot.RangeWeapon, out var rangeWeapon) && rangeWeapon != null && rangeWeapon.Durability < rangeWeapon.MaxDurability)
            {
                rangeWeapon.Repair(rangeWeapon.MaxDurability);
                anyRepaired = true;
            }

            if (_equipment.TryGetValue(EquipSlot.Armour, out var armour) && armour != null && armour.Durability < armour.MaxDurability)
            {
                armour.Repair(armour.MaxDurability);
                anyRepaired = true;
            }

            if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmet) && helmet != null && helmet.Durability < helmet.MaxDurability)
            {
                helmet.Repair(helmet.MaxDurability);
                anyRepaired = true;
            }

            if (!anyRepaired)
            {
                Console.WriteLine("All the equipment is already in perfect condition.!");
                return false;
            }

            Inventory.TryRemove(grindstone);
            Console.WriteLine("Grindstone has been used.!");
            return true;
        }

        private void UseEconomicItem(EconomicItem economicItem)
        {
            if (economicItem is HealthPotion healthPotion) 
            {
                Health = Math.Min(Health + healthPotion.HealthRestore, MaxHealth);
                Console.WriteLine($"{Name} used a Health posion! Health: {Health}/{MaxHealth}");
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            uint totalDefence = 0;
            
            if (_equipment.TryGetValue(EquipSlot.Armour, out var armour) && armour is Armour armourItem && !armourItem.IsBroken)
            {
                totalDefence += armourItem.Defence; 

                if (!armourItem.ReduceDurability(1))
                {
                    Console.WriteLine($"Arnour {armourItem.Name} is broken!");
                    _equipment.Remove(EquipSlot.Armour);
                }
            }

            if (_equipment.TryGetValue(EquipSlot.Helmet, out var helmet) && helmet is Helmet helmetItem && !helmetItem.IsBroken)
            {
                totalDefence += helmetItem.Defence;
                
                if (!helmetItem.ReduceDurability(1))
                {
                    Console.WriteLine($"Helmet {helmetItem.Name} is broken!");
                    _equipment.Remove(EquipSlot.Helmet);
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
                    Console.WriteLine($"RangeWeapon{rangeWeapon.Name} is broken!");
                    _equipment.Remove(EquipSlot.RangeWeapon);
                }
            }

            float damageReduction = Math.Min(totalDefence / 100f, 0.5f);
            return (uint)(damage * (1 - damageReduction));
        }
            return damage;
        }

             protected override void DamageReceiveHandler()
        {
            Console.WriteLine($"{Name} is damaged! Helth: {Health}/{MaxHealth}");
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
            return builder.ToString();

            builder.AppendLine("\n--- Экипировка ---");
            foreach (var slot in Enum.GetValues<EquipSlot>())
            {
                if (_equipment.TryGetValue(slot, out var item) && item != null)
                {
                    string status = item.IsBroken ? " (СЛОМАНА)" : $" (Прочность: {item.Durability}/{item.MaxDurability})";
                    builder.AppendLine($"{_equipmentNames[slot]}: {item.Name}{status}");
                }
                else
                {
                    builder.AppendLine($"{_equipmentNames[slot]}: Пусто");
                }
            }

            builder.AppendLine("\n--- Equipment ---");
            var items = Inventory.Items;
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
            Console.WriteLine("\n=== Equipments ===");
            foreach (var slot in Enum.GetValues<EquipSlot>())
            {
                if (_equipment.TryGetValue(slot, out var item) && item != null)
                {
                    string status = item.IsBroken ? "Broken" : $"{item.Durability}/{item.MaxDurability}";
                    Console.WriteLine($"{_equipmentNames[slot]}: {item.Name} (Endurance: {status})");
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
        }
    }
}
