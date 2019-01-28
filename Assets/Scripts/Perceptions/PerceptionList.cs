using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PerceptionList
{
    private string id;
    public string Id
    {
        get
        {
            return this.id;
        }
        private set
        {
            this.id = value;
        }
    }

    private ICollection<Perception> perceptions = new List<Perception>();
    public ICollection<Perception> Perceptions
    {
        get
        {
            return perceptions;
        }
        private set
        {
            this.perceptions = value;
        }
    }

    public PerceptionList(string id, ICollection<Perception> perceptions)
    {
        this.id = id;
        this.perceptions = perceptions;
    }

    public PerceptionList(string id, ICollection<Vector3> positions)
    {
        this.id = id;
        foreach(var position in positions)
        {
            perceptions.Add(new Perception(position));
        }
    }

    public PerceptionList(string id, ICollection<GameObject> objects)
    {
        this.id = id;
        foreach(var obj in objects)
        {
            perceptions.Add(new Perception(obj.transform.position));
        }
    }

    public PerceptionList(string id, ICollection<Collider> colliders)
    {
        this.id = id;
        foreach(var collider in colliders)
        {
            perceptions.Add(new Perception(collider.gameObject.transform.position));
        }
    }
}
