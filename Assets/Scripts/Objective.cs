using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective
{
    public ObjectiveState state;
    public string description;

    public Objective(ObjectiveState state, string description)
    {
        this.state = state;
        this.description = description;
    }
}

public enum ObjectiveState
{
    New,
    Updated,
    Completed
}
