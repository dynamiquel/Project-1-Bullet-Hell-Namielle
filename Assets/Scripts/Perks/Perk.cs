using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Figured that perks don't actually do anything on their own. It's simply a condition check.
public class Perk
{
    public PerkData PerkData;

    /// <summary>
    /// Return true if the perk is on cooldown.
    /// </summary>
    public bool IsCooldown { get => cooldown != null; }

    /// <summary>
    /// Returns true if the perk is not on cooldown; starts the cooldown.
    /// </summary>
    public bool CanUse
    {
        get
        {
            if (IsCooldown)
            {
                //Debug.Log($"Perk '{PerkData.Name}' is on cooldown.");
                return false;
            }

            cooldown = ItemDatabase.Instance.StartCoroutine(StartCooldown(PerkData.Cooldown));

            //Debug.Log($"Perk '{PerkData.Name}' has executed.");
            return true;
        }
    }

    private Coroutine cooldown = null;

    public Perk(PerkData perkData)
    {
        PerkData = perkData;
    }

    /// <summary>
    /// Forces the cooldown to stop.
    /// </summary>
    public void ClearCooldown()
    {
        cooldown = null;
    }

    IEnumerator StartCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        //Debug.Log($"Perk '{PerkData.Name}' cooldown has finished.");
        this.cooldown = null;
    }

    public override string ToString()
    {
        var sb = new System.Text.StringBuilder("Perk");
        sb.AppendLine(PerkData.ToString());
        return sb.ToString();
    }
}
