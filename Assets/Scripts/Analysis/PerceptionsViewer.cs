using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionsViewer : MonoBehaviour
{
    [SerializeField]
    public Color perceptionRayColor = Color.blue;

    private Perceiver perceiverComponent;

    // Start is called before the first frame update
    void Start()
    {
        this.perceiverComponent = gameObject.GetComponent<Perceiver>() as Perceiver;
    }

    // Update is called once per frame
    void Update()
    {
        var perceivedObjects = perceiverComponent.PerceivedObjets;
        foreach(var perception in perceivedObjects)
        {
            var absolutePerceptionPosition = transform.TransformPoint(perception.Position);
            var direction = absolutePerceptionPosition - gameObject.transform.position;
            Debug.DrawRay(gameObject.transform.position, direction, perceptionRayColor);
        }
    }
}
