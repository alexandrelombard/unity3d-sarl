using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script in charge of applying the influences on the bodies
/// </summary>
public class InfluenceApplier : MonoBehaviour
{
    private Dictionary<string, PhysicalInfluence> influences = new Dictionary<string, PhysicalInfluence>();

    // Start is called before the first frame update
    void Start()
    {
        var udpSarlInterface = FindObjectsOfType(typeof(UdpSarlInterface)) as UdpSarlInterface[];

        if (udpSarlInterface.Length != 0)
        {
            udpSarlInterface[0].PhysicalInfluenceReceived += physicalInfluence =>
            {
                var id = physicalInfluence.Id;

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    // Instantiate the body if it doesn't exist (later we will remove this part)
                    if (!BodiesRepository.Instance().Bodies.ContainsKey(id))
                    {
                        var bodyGameObject = BodiesRepository.Instance().Spawn(id);

                        // TODO Position is randomized (the concept of spawners must be introduced)
                        bodyGameObject.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
                    }

                    // TODO In a first time we just apply the force of the influence (PoC)
                    BodiesRepository.Instance().Bodies[id].GetComponent<Rigidbody>().AddRelativeForce(physicalInfluence.Force, ForceMode.Force);
                    //bodies[id].GetComponent<Rigidbody>().AddForceAtPosition(physicalInfluence.Force, physicalInfluence.ForceLocation);
                    //bodies[id].GetComponent<Rigidbody>().AddTorque(physicalInfluence.AngularForce);
                });

                //influences.Add(id, physicalInfluence);
            };

            udpSarlInterface[0].SimulationControlReceived += simulationControl =>
            {
                if (simulationControl.Type == SimulationControl.ActionType.CreateBody)
                {
                    UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    {
                        var id = simulationControl.Data;

                        Debug.Log("Creating body: " + id);

                        if (!BodiesRepository.Instance().Bodies.ContainsKey(id))
                        {
                            var bodyGameObject = BodiesRepository.Instance().Spawn(id);
                            // TODO Position is randomized (the concept of spawners must be introduced)
                            bodyGameObject.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
                        }
                    });
                }
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
