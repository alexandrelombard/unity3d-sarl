using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Represents a perception
/// </summary>
public class Perception
{
    private Vector3 position;
    /// <summary>
    /// The position of the perception (relative to the body)
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            this.position = value;
        }
    }

    private Collider attachedCollider;
    /// <summary>
    /// The collider attached to this perception (may be null)
    /// </summary>
    public Collider AttachedCollider
    {
        get
        {
            return this.attachedCollider;
        }
        private set
        {
            this.attachedCollider = value;
        }
    }

    public Perception(Vector3 position, Collider attachedCollider = null)
    {
        this.attachedCollider = attachedCollider;
        this.position = position;
    }
}
