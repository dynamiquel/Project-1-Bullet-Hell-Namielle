using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public class AbilityController : MonoBehaviour
{
    // The currently equipped abilities that you can use.
    public Dictionary<string, Ability> EquippedAbilities { get; private set; } = new Dictionary<string, Ability>();
    
    private Transform abilitiesTransform;

    private void Awake()
    {
        // Groups the abilities in the hierarchy.
        GameObject go = new GameObject("Abilities");
        go.transform.parent = transform;
        abilitiesTransform = go.transform;
    }

    // Adds the ability so you can use it later.
    public void AddAbility(string abilityId)
    {
        if (ItemDatabase.Instance == null)
        {
            Debug.LogError("No Item Database was found.");
            return;
        }

        // Prevents duplicates.
        if (EquippedAbilities.ContainsKey(abilityId))
            return;

        // Creates a copy of the ability from the Item Database.
        if (ItemDatabase.Instance.Abilities.ContainsKey(abilityId))
        {
            var newAbility = Instantiate(ItemDatabase.Instance.Abilities[abilityId], abilitiesTransform);
            EquippedAbilities[abilityId] = newAbility.GetComponent<Ability>();
            Debug.Log($"Ability {abilityId} successfully created. {EquippedAbilities[abilityId].Name}");
        }
        else
            Debug.LogError($"Item Database does not contain a PerkData with the ID: {abilityId}");
    }

    // Removes the ability so you can no longer use it.
    public void RemoveAbility(string abilityId)
    {
        if (EquippedAbilities.ContainsKey(abilityId))
        {
            Destroy(EquippedAbilities[abilityId].gameObject);
            EquippedAbilities.Remove(abilityId);
        }
    }
    
    // If the ability exists, use it.
    public bool UseAbility(string abilityId)
    {
        if (EquippedAbilities.ContainsKey(abilityId))
        {
            EquippedAbilities[abilityId].Use();
            return true;
        }

        Debug.LogWarning($"Ability '{abilityId}' could not be found in the EquippedAbilities");
        
        return false;
    }
}
