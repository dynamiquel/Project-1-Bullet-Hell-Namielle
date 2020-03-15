using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public class PerkController : MonoBehaviour
{
    public Dictionary<string, Perk> ActivePerks { get; private set; } = new Dictionary<string, Perk>();

    public void AddPerk(string id)
    {
        if (ItemDatabase.Instance == null)
        {
            Debug.LogError("No Item Database was found.");
            return;
        }

        if (ItemDatabase.Instance.PerkDatas.ContainsKey(id))
        {
            Perk newPerk = new Perk(ItemDatabase.Instance.PerkDatas[id]);
            ActivePerks[id] = newPerk;
            Debug.Log($"Perk {id} successfully created. {ActivePerks[id].PerkData.Description}");
        }
        else
            Debug.LogError($"Item Database does not contain a PerkData with the ID: {id}");
        
    }

    /// <summary>
    /// Returns true if the perk is active and not on cooldown; starts the cooldown (if it has one).
    /// </summary>
    public bool CanUse(string perkId)
    {
        if (ActivePerks.ContainsKey(perkId))
            if (ActivePerks[perkId].CanUse)
                return true;

        return false;
    }
}