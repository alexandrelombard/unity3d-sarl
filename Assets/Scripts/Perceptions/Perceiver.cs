using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A body with this behaviour attached is able to perceive objects within a frustum collider
/// </summary>
public class Perceiver : MonoBehaviour
{
    [SerializeField]
    private Collider viewFrustum;
    public Collider ViewFrustum
    {
        get
        {
            return this.viewFrustum;
        }
        set
        {
            this.viewFrustum = value;
        }
    }

    private Dictionary<Collider, Perception> perceivedObjets = new Dictionary<Collider, Perception>();
    public ICollection<Perception> PerceivedObjets
    {
        get
        {
            return perceivedObjets.Values;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        UpdatePerceptions(other);
    }

    void OnTriggerStay(Collider other)
    {
        UpdatePerceptions(other);
    }

    void OnTriggerExit(Collider other)
    {
        this.perceivedObjets.Remove(other);
    }

    void UpdatePerceptions(Collider other)
    {
        // IsTrigger is used for view frustum and not for bodies, as we only want to perceive bodies, we are filtering the collider with the IsTrigger property
        if (!other.isTrigger)
        {
            if (CheckVisibility(other))
            {
                var relativePosition = transform.InverseTransformPoint(other.gameObject.transform.position);
                var perception = new Perception(relativePosition, other);
                if (this.perceivedObjets.ContainsKey(other))
                {
                    this.perceivedObjets[other] = perception;
                }
                else
                {
                    this.perceivedObjets.Add(other, perception);
                }
            }
        }
    }

    /// <summary>
    /// Checks if a collider is visible from this object
    /// </summary>
    /// <param name="collider">The collider of the other object</param>
    /// <returns>True if the object is visible, false if it is occluded</returns>
    bool CheckVisibility(Collider collider)
    {
        var selfPosition = gameObject.transform.position;
        var otherPosition = collider.gameObject.transform.position;

        RaycastHit hitInfo;
        var hit = Physics.Raycast(selfPosition, otherPosition - selfPosition, out hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
        if(hit)
        {
            if(hitInfo.collider == collider)
            {
                return true;
            }
        }

        return false;
    }
}
