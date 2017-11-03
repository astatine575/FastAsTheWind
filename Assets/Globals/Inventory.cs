using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public List<GameItem> inventoryList = new List<GameItem>();

    private int size = 0; //Number of objects in inventoryList *NOT ARRAY LENGTH*

    public void AddItem(GameItem item, int quantity = 1)
    {
        int index = inventoryList.FindIndex(x => x.GetName().Equals(item.GetName(), StringComparison.Ordinal)); // in short, looks for a GameItem in inventory with same name as item

        if (index >= 0) //If true, then will return if the item can be placed not at the end of the array
        {
            if (item.GetStackable())
            {
                inventoryList[index].quantity += quantity;
                size += quantity;
                //Debug.Log("Added " + quantity.ToString() + " " + item.GetName() + " to player inventory");
                return;
            }
            else if (inventoryList[index].quantity == 0) //If the inventory contains 0 of a non-stackable item, replace it with the new item of the same name
            {
                item.invLoc = index;
                inventoryList[index] = item;
                size++;
                //Debug.Log("Added a " + item.GetName() + " to player inventory");
                return;
            }
            else
            {
                inventoryList.Insert(inventoryList.Count, item);
                item.invLoc = inventoryList.Count - 1;
                //Debug.Log("Added a " + item.GetName() + " to player inventory");
                size++;
            }
        }
        else
        {
            inventoryList.Insert(inventoryList.Count, item);
            item.invLoc = inventoryList.Count-1;
            if (item.GetStackable())
            {
                inventoryList[inventoryList.Count-1].quantity = quantity;
                size += quantity;
                //Debug.Log("Added " + quantity.ToString() + " " + item.GetName() + "(s) to player inventory");
            }
            else
            {
                //Debug.Log("Added a " + item.GetName() + " to player inventory");
                size++;
            }
        }
    }
    
    public Boolean RemoveItem(GameItem item)
    {
        if (item.quantity <= 0)
            return false;

        item.quantity--;
        size--;
        return true;
    }

    public int InventoryPosition(GameItem item)
    {
        return 0;
    }
}

[System.Serializable]
public abstract class GameItem
{
    public int id;

    public int quantity = 1; //0 possible, indicates that the item can be replaced by a new instance even if it isn't stackable
    //-1 indicates that the item is in use, either by the ship or a crew member
    public int invLoc; //0-based location in array; assigned on add

    public string childAttributes = "";

    public bool canUse(int num = 1) //Returns true, unless there is no item to use
    {
        if (quantity < num) return false;

        return true;
    }

    public void Drop()
    {
        PlayerStatus.Inventory.RemoveItem(this);
    }

    public abstract string GetName();

    public abstract string GetItemType();

    public abstract string GetItemDescription();

    public abstract int GetValue();

    public abstract bool GetStackable();

    public string stackableString()
    {
        if (GetStackable() == true) return "Quantity: " + quantity + "\n";
        return "";
    }

    public abstract string GetAttributes();
}

[System.Serializable]
public class Consumable : GameItem
{

    public enum Effect
    {
        HEAL,
        DAMAGE,
        REPAIR
    }

    public readonly string _name;
    public readonly string _description;
    public readonly int _value;
    public readonly Effect _effect;
    public readonly int _magnitude;
    public readonly bool _stackable;

    public Consumable(string name, string description, int value, Effect effect, int magnitude, bool stackable = true)
    {
        _name = name;
        _description = description;
        _value = value;
        _effect = effect;
        _magnitude = magnitude;
        _stackable = stackable;
    }

    public override string GetName()
    {
        return _name;
    }

    public override string GetItemType()
    {
        return "Consumable";
    }

    public string GetEffect()
    {
        if (_effect == Effect.HEAL)
            return "Heals";
        else if (_effect == Effect.DAMAGE)
            return "Damages";
        else if (_effect == Effect.REPAIR)
            return "Repairs";
        else
            return "Unknown";
    }

    public override string GetItemDescription()
    {
        return _description;
    }

    public override int GetValue()
    {
        return _value;
    }

    public override bool GetStackable()
    {
        return _stackable;
    }

    public override string GetAttributes()
    {
        childAttributes = "Material: " + GetEffect() + "\n" + "Strength: " + _magnitude;

        return "Name: " + GetName() + "\n" + stackableString() + "Classification: " + GetItemType() + "\n" + "Value: " + _value + "\n\n"
            + childAttributes + "\n\n"
            + GetItemDescription();
    }
}
[System.Serializable]
public class CrewWeapon : GameItem
{
    public enum WeaponMaterial
    {
        STEEL,
        IRON
    }

    public readonly string _name;
    public readonly string _description;
    public readonly int _value;
    public readonly WeaponMaterial _material;
    public readonly int _condition;
    public readonly int _maxCondition;
    public readonly int _damage;

    public CrewWeapon(string name, string description, int value, WeaponMaterial material, int condition, int maxCondition, int damage)
    {
        _name = name;
        _description = description;
        _value = value;
        _material = material;
        _condition = condition;
        _maxCondition = maxCondition;
        _damage = damage;
    }

    public override string GetName()
    {
        return _name;
    }

    public override string GetItemType()
    {
        return "Personal Weapon";
    }

    public string GetWeaponMaterial()
    {
        if (_material == WeaponMaterial.IRON)
            return "Iron";
        else if (_material == WeaponMaterial.STEEL)
            return "Steel";
        else
            return "Unknown";
    }

    public override string GetItemDescription()
    {
        return _description;
    }

    public override int GetValue()
    {
        return _value;
    }

    public override bool GetStackable()
    {
        return false;
    }

    public override string GetAttributes()
    {
        childAttributes = "Material: " + GetWeaponMaterial() + "\n" + "Condition: " + _condition + "/" + _maxCondition + "\n" + "Damage: " + _damage;

        return "Name: " + GetName() + "\n" + stackableString() + "Classification: " + GetItemType() + "\n" + "Value: " + _value + "\n\n"
            + childAttributes + "\n\n"
            + GetItemDescription();
    }

}

[System.Serializable]
public class ShipWeapon : GameItem
{

    public enum WeaponType
    {
        CANNON,
        SPELL,
        MELEE
    }

    public readonly string _name;
    public readonly string _description;
    public readonly int _value;
    public readonly WeaponType _weaponType;
    public readonly Ammunition.AmmoType _ammoType;
    public readonly int _cooldown;
    public readonly int _damage;

    public ShipWeapon(string name, string description, int value, WeaponType weaponType, Ammunition.AmmoType ammoType, int cooldown, int damage)
    {
        _name = name;
        _description = description;
        _value = value;
        _weaponType = weaponType;
        _ammoType = ammoType;
        _cooldown = cooldown;
        _damage = damage;
    }

    

    public override string GetName()
    {
        return _name;
    }

    public override string GetItemType()
    {
        return "Ship Weapon";

    }

    public string GetWeaponType()
    {
        if (_weaponType == WeaponType.CANNON)
            return "Cannon";
        else if (_weaponType == WeaponType.SPELL)
            return "Spell";
        else
            return "Unknown";
    }

    public string GetAmmoType()
    {
        if (_ammoType == Ammunition.AmmoType.CANNONBALL)
            return "Cannon Balls";
        else if (_ammoType == Ammunition.AmmoType.MANASHARD)
            return "Mana Shards";
        else
            return "Unknown";

    }

    public override string GetItemDescription()
    {
        return _description;
    }

    public override int GetValue()
    {
        return _value;
    }

    public override string GetAttributes()
    {
        childAttributes = "Weapon Type: " + GetWeaponType() + "\n" + "Ammunition: " + GetAmmoType() + "\n" + "Damage: " + _damage + "\n" + "Cooldown: " + _cooldown;
        
        return "Name: " + GetName() + "\n" + stackableString() + "Classification: " + GetItemType() + "\n" + "Value: " + GetValue() + "\n\n"
            + childAttributes + "\n\n" 
            + GetItemDescription();
    }

    public override bool GetStackable()
    {
        return false;
    }
}

[System.Serializable]
public class Ammunition : GameItem
{
    public enum AmmoType
    {
        CANNONBALL,
        MANASHARD,
        NONE
    }

    public readonly string _name;
    public readonly string _description;
    public readonly int _value;
    public readonly AmmoType _ammoType;
    public readonly int _hullDamage;
    public readonly int _crewDamage;
    public readonly int _sailDamage;
    public readonly bool _stackable;

    public Ammunition(string name, string description, int value, AmmoType ammoType, int hullDamage, int crewDamage, int sailDamage, bool stackable = true)
    {
        _name = name;
        _description = description;
        _value = value;
        _ammoType = ammoType;
        _hullDamage = hullDamage;
        _crewDamage = crewDamage;
        _sailDamage = sailDamage;
        _stackable = stackable;
    }



    public override string GetName()
    {
        return _name;
    }

    public override string GetItemType()
    {
        return "Ammunition";

    }

    public string GetAmmoType()
    {
        if (_ammoType == AmmoType.CANNONBALL)
            return "Cannon Balls";
        else if (_ammoType == AmmoType.MANASHARD)
            return "Mana Shards";
        else
            return "Unknown";

    }

    public override string GetItemDescription()
    {
        return _description;
    }

    public override int GetValue()
    {
        return _value;
    }

    public override string GetAttributes()
    {
        childAttributes = "Ammunition Type: " + GetAmmoType() + "\n" + "Hull Damage: x" + _hullDamage + "\n" + "Crew Damage: x" + _crewDamage + "\n" + "Sail Damage: x" + _sailDamage;

        return "Name: " + GetName() + "\n" + stackableString() + "Classification: " + GetItemType() + "\n" + "Value: " + GetValue() + "\n\n"
            + childAttributes + "\n\n"
            + GetItemDescription();
    }

    public override bool GetStackable()
    {
        return _stackable;
    }
}