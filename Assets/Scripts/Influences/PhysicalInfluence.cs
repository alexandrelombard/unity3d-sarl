using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Represents a physical influence
/// </summary>
public class PhysicalInfluence : Influence
{
    public Vector3 Force
    {
        get;
        private set;
    }
    public Vector3 ForceLocation
    {
        get;
        private set;
    }
    public Vector3 AngularForce
    {
        get;
        private set;
    }

    public override void Parse(Stream stream)
    {
        using (BinaryReader reader = new BinaryReader(stream))
        {
            this.Id = reader.ReadSarlString();
            this.Force = reader.ReadSarlVector();
            this.ForceLocation = reader.ReadSarlVector();
            this.AngularForce = reader.ReadSarlVector();
        }
    }
}
