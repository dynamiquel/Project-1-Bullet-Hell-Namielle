using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class PerkData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; } = 1;
    public float Cooldown { get; set; } = 0;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Description: {Description}");
        sb.AppendLine($"Cost: {Cost.ToString()}");
        sb.AppendLine($"Cooldown: {Cooldown.ToString()}");
        return sb.ToString();
    }
}
