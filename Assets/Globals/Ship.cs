using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ship {

    public enum ShipClass
    {
        SLOOP,
        FRIGATE,
        KRAKEN
    }

    public readonly string _name; // name of the ship
    public readonly string _description; // description of the ship
    public readonly int _value; // value of the ship
    public readonly ShipClass _shipClass; // class of the ship

    private readonly int _hullHealthMaxBase; // hull health of the ship without any upgrades
    public int _hullHealth; // current hull health of the ship

    private readonly int _crewHealthMaxBase; // crew health of the ship without any upgrades
    public int _crewHealth; // current crew health of the ship

    private readonly int _sailHealthMaxBase; // sail health of the ship without any upgrades
    public int _sailHealth; // current sail health of the ship

    public int slotCount;
    public WeaponSlot[] weaponSlots;
    private bool initialized = false;


    public Ship (string name, string description, int value, ShipClass shipClass, int hullHealthMaxBase, int crewHealthMaxBase, int sailHealthMaxBase)
    {
        _name = name;
        _description = description;
        _value = value;
        _shipClass = shipClass;

        _hullHealthMaxBase = hullHealthMaxBase;
        _crewHealthMaxBase = crewHealthMaxBase;
        _sailHealthMaxBase = sailHealthMaxBase;

        _hullHealth = HullStrengthMax();
        _crewHealth = CrewStrengthMax();
        _sailHealth = SailStrengthMax();


        InitializeWeaponSlots();
    }

    public int HullStrengthMax() // returns maximum hull strength after all upgrades
    {
        return _hullHealthMaxBase;
    }

    public int CrewStrengthMax() // returns maximum crew strength after all upgrades
    {
        return _crewHealthMaxBase;
    }

    public int SailStrengthMax() // returns maximum sail strength after all upgrades
    {
        return _sailHealthMaxBase;
    }

    private void InitializeWeaponSlots() // initailizes the ship's weapons based on its class. Probably will be replaced.
    {
        if (initialized)
        {
            Debug.LogError("The ship " + _name + " has already been initialized!");
            return;
        }

        switch (_shipClass)
        {
            case ShipClass.SLOOP: // Sloops have 4 slots for cannons
                slotCount = 4;
                weaponSlots = new WeaponSlot[slotCount];
                for (int i = 0; i < 4; i++)
                {
                    weaponSlots[i] = new WeaponSlot();
                    weaponSlots[i].addType(ShipWeapon.WeaponType.CANNON);
                    //weaponSlots[i].equippedWeapon = (ShipWeapon)new ShipWeapon(
                    //                        "Basic Cannon",   // name
                    //                        "The old and reliable 20mm naval gun, a common weapon used by merchant, pirate, and naval vessels alike.", // description
                    //                        50,                             // value
                    //                        ShipWeapon.WeaponType.CANNON,   // weapon type
                    //                        Ammunition.AmmoType.CANNONBALL, // ammo type
                    //                        2,                              // cooldown
                    //                        10).CreateInstance();                           // damage
                }
                break;
            case ShipClass.FRIGATE: // Frigates have 8 slots for cannons, slots 6 and 7 can also be used for spells
                slotCount = 8;
                weaponSlots = new WeaponSlot[slotCount];
                for (int i = 0; i < 8; i++)
                {
                    weaponSlots[i] = new WeaponSlot();
                    weaponSlots[i].addType(ShipWeapon.WeaponType.CANNON);
                }
                for (int i = 6; i < 8; i++)
                {
                    weaponSlots[i] = new WeaponSlot();
                    weaponSlots[i].addType(ShipWeapon.WeaponType.SPELL);
                }
                break;
            case ShipClass.KRAKEN: // Kraken has 8 slots for melee, 1 for each tentacle it'll have
                slotCount = 8;
                weaponSlots = new WeaponSlot[slotCount];
                for (int i = 0; i < 8; i++)
                {
                    weaponSlots[i] = new WeaponSlot();
                    weaponSlots[i].addType(ShipWeapon.WeaponType.MELEE);
                }
                break;
            default:
                break;
        }
    }

    public ShipWeapon.WeaponType[] getValidWeapons(int slot) // Returns an array of all WeaponTypes the slot accepts
    {
        if (slot >= slotCount)
            return null;
        return weaponSlots[slot].validTypes();
    }

    public bool isValidWeapon (ShipWeapon weapon, int slot) // See if a weapon is valid. should be called before equipWeapon to avoid error
    {
        if (slot >= slotCount)
            return false;
        if (!weaponSlots[slot].validWeapon(weapon))
            return false;
        return true;
    }

    public bool equipWeapon (ShipWeapon weapon, int slot) // Attempt to equip weapon to this slot, returns true if equipped, false otherwise.
    {
        if (slot >= slotCount)
        {
            Debug.LogError("ERR: slot " + slot + " is not a valid slot on ship " + _name);
            return false;
        }
        if (!weaponSlots[slot].validWeapon(weapon))
        {
            Debug.LogError("ERR: weapon type " + weapon._weaponType + " is not a valid type for slot " + slot + " on ship " + _name + ". valid types are: " + weaponSlots[slot].validTypesString());
            return false;
        }

        weaponSlots[slot].equippedWeapon = weapon;
        return true;
    }
    
    public ShipWeapon getWeapon(int slot) // WARNING: CAN BE NULL!
    {
        if (slot > slotCount)
        {
            Debug.LogError("ERR: slot " + slot + " is not a valid slot on ship " + _name);
            return null;
        }

        return weaponSlots[slot].equippedWeapon;
    }

    public WeaponSlot[] GetWeaponSlots() { return weaponSlots;}

    public int GetSlotNumber() { return slotCount; }

}

[System.Serializable]
public class WeaponSlot
{
    private List<ShipWeapon.WeaponType> acceptedTypes;
    private int numTypes;
    public ShipWeapon equippedWeapon;

    public WeaponSlot()
    {
        acceptedTypes = new List<ShipWeapon.WeaponType>();
        numTypes = 0;
    }

    //By Jon G.; puts the current weapon in this slot back in player inventory, then sets a new weapon to the slot
    public void Replace(ShipWeapon newWeapon) 
    {
        if(equippedWeapon != null) PlayerStatus.Inventory.AddItem(equippedWeapon.CreateInstance());
        equippedWeapon = (ShipWeapon)newWeapon.CreateInstance();
        newWeapon.Drop();
    }

    public ShipWeapon.WeaponType[] validTypes()
    {
        ShipWeapon.WeaponType[] a = new ShipWeapon.WeaponType[numTypes];
        acceptedTypes.CopyTo(a);
        return a;
    }

    public string validTypesString()
    {
        string s = "";
        foreach (ShipWeapon.WeaponType type in acceptedTypes)
            s += ", " + type;
        return s.Substring(2);
    }

    public void addType(ShipWeapon.WeaponType weaponType)
    {
        if (!acceptedTypes.Contains(weaponType))
        {
            acceptedTypes.Add(weaponType);
            numTypes++;
        }
        
    }

    public bool validWeapon(ShipWeapon weapon)
    {
        return acceptedTypes.Contains(weapon._weaponType);
    }
}

/*
public class CrewSlot
{

}
*/
