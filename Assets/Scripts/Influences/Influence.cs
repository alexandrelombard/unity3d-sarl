using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Abstract class for influences (both physical and action)
/// </summary>
public abstract class Influence
{
    public const string PHYSICAL_INFLUENCE = "0101";
    public const string ACTION_INFLUENCE = "0201";

    public string Id
    {
        get;
        protected set;
    }

    public void Parse(byte[] byteArray)
    {
        using (MemoryStream stream = new MemoryStream(byteArray))
        {
            Parse(stream);
        }
    }

    public abstract void Parse(Stream stream);
}
