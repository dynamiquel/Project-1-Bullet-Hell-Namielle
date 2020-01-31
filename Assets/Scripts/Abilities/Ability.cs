using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Just a general idea
public class Ability : MonoBehaviour
{
    public virtual string Id { get; set; }
    public virtual string name { get; set; }
    public virtual decimal coolDown { get; set; }
    decimal coolDownTimer;

    public virtual void Use()
    {
        if (coolDownTimer > 0)
            return;

        SetCooldown();
    }

    void SetCooldown()
    {
        coolDownTimer = coolDown;
    }

    private void Update()
    {
        if (coolDownTimer > 0)
            coolDownTimer -= (decimal)Time.deltaTime;
    }
}
