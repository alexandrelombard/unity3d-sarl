using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Represents an action influence
/// </summary>
public class ActionInfluence : Influence
{
    public string ActionType
    {
        get;
        private set;
    }

    public string Target
    {
        get;
        private set;
    }

    public Vector3 Position
    {
        get;
        private set;
    }

    public override void Parse(Stream stream)
    {
        using (BinaryReader reader = new BinaryReader(stream))
        {
            this.Id = reader.ReadSarlString();
            this.ActionType = reader.ReadSarlString();
            this.Target = reader.ReadSarlString();
            this.Position = reader.ReadSarlVector();
        }
    }
    
}
