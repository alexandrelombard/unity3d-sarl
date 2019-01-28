using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceViewer : MonoBehaviour
{
    [SerializeField]
    public Color velocityColor = Color.yellow;

    [SerializeField]
    public Color normalizedVelocityColor = new Color(1.0f, 0.5f, 0.0f);

    [SerializeField]
    public Color velocityVariationColor = Color.red;

    [SerializeField]
    public float velocityVariationScale = 10.0f;

    private Vector3 lastVelocity = new Vector3();
    
    void Start()
    {
        //
    }

    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
        if(rigidbody != null)
        {
            var position = this.transform.position;
            var velocity = rigidbody.velocity;
            var velocityVariation = velocity - this.lastVelocity;
            velocityVariation.Normalize();
            velocityVariation.Scale(new Vector3(velocityVariationScale, velocityVariationScale, velocityVariationScale));

            Debug.DrawRay(position, rigidbody.velocity, velocityColor);
            Debug.DrawRay(position, rigidbody.velocity.normalized, normalizedVelocityColor);
            //Debug.DrawRay(position, velocityVariation, velocityVariationColor);

            this.lastVelocity = velocity;
        }
    }
}
