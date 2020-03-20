using System.Collections.Generic;
using UnityEngine;

// Ask Liam for help.
public class AbilityController : MonoBehaviour
{
    // The currently equipped abilities that you can use.
    //public Dictionary<string, Ability> Abilities { get; private set; } = new Dictionary<string, Ability>();
    [SerializeField] private List<string> _abilitiesIds;
    public List<Ability> Abilities { get; private set; } = new List<Ability>();
    
    private Transform abilitiesTransform;

    private void Awake()
    {
        // Groups the abilities in the hierarchy.
        var go = new GameObject("Abilities");
        go.transform.parent = transform;
        abilitiesTransform = go.transform;

        for (var i = 0; i < _abilitiesIds.Count; i++)
            AddAbility(_abilitiesIds[i], i);

        _abilitiesIds = null;
    }

    // Adds the ability so you can use it later.
    // Players can only use index 0 and 1. But AI can use whatever.
    public void AddAbility(string abilityId, int index)
    {
        if (ItemDatabase.Instance == null)
        {
            Debug.LogError("No Item Database was found.");
            return;
        }
        
        // Creates a copy of the ability from the Item Database.
        if (ItemDatabase.Instance.Abilities.ContainsKey(abilityId))
        {
            // Gets a copy of the ability from the item database.
            var newAbility = Instantiate(ItemDatabase.Instance.Abilities[abilityId], abilitiesTransform)
                .GetComponent<Ability>();

            // Prevents duplicates.
            if (Abilities.Contains(newAbility))
                return;
            
            // Adds the ability at the given index.
            Abilities.Insert(index, newAbility);
            //Debug.Log($"Ability {abilityId} successfully created. {Abilities[index].Name}");
        }
        else
            Debug.LogError($"Item Database does not contain a PerkData with the ID: {abilityId}");
    }

    // Removes the ability so you can no longer use it.
    // RefreshList = true: resizes the list.
    // RefreshList = false: sets the index to an empty ability so you can still use the same indexes.
    // Not tested.
    public void RemoveAbility(int index, bool refreshList)
    {
        if (Abilities.Count <= index)
            return;
        
        if (refreshList)
            Abilities.RemoveAt(index);
        else
            AddAbility("nullAbility", index);
    }
    
    // If an ability exists at the given index, use it.
    public bool UseAbility(int index = 0)
    {
        if (Abilities.Count > index)
        {
            Abilities[index].Use();
            return true;
        }

        Debug.LogWarning($"Ability '{index}' could not be found in the EquippedAbilities");
        
        return false;
    }
}
