using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is in charge of controlling the simulation (initializing physics settings, triggering steps, etc.)
/// </summary>
public class SimulationExecutor : MonoBehaviour
{
    /// <summary>
    /// The delta time of simulation steps (in seconds)
    /// </summary>
    [SerializeField]
    private float deltaTime = 0.01f;
    public float DeltaTime
    {
        get;
        set;
    }

    private UdpSarlInterface sarlInterface;

    /// <summary>
    /// Adds a listener to the simulation control events triggered by the SARL interface
    /// </summary>
    void Start()
    {
        Physics.autoSimulation = false;

        var udpSarlInterface = FindObjectsOfType(typeof(UdpSarlInterface)) as UdpSarlInterface[];
        if (udpSarlInterface.Length != 0)
        {
            sarlInterface = udpSarlInterface[0];

            sarlInterface.SimulationControlReceived += simulationControl =>
            {
                switch (simulationControl.Type)
                {
                    case SimulationControl.ActionType.SimulationStep:
                        Step();
                        break;
                }
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Trigger a simulation step
    /// </summary>
    void Step()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            // Trigger the phyics simulation
            Physics.Simulate(deltaTime);

            // Compute the perceptions for all bodies if they have a perceiver component
            foreach (var agentBody in BodiesRepository.Instance().Bodies)
            {
                var perceiver = agentBody.Value.GetComponent(typeof(Perceiver)) as Perceiver;
                if(perceiver != null)
                {
                    var perceptions = perceiver.PerceivedObjets;
                    this.sarlInterface.EmitPerceptions(new PerceptionList(agentBody.Key, perceptions));
                }
            }
        });
    }
}
