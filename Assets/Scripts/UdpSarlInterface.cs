using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class UdpSarlInterface : MonoBehaviour
{
    [SerializeField]
    private int receptionPort = 4242;
    public int ReceptionPort
    {
        get { return receptionPort; }
        set { receptionPort = value; }
    }

    [SerializeField]
    private int emissionPort = 4243;
    public int EmissionPort
    {
        get { return emissionPort; }
        set { emissionPort = value; }
    }

    private UdpClient udpClient;
    private Thread thread;

    public delegate void ActionInfluenceHandler(ActionInfluence actionInfluence);
    public delegate void PhysicalInfluenceHandler(PhysicalInfluence physicalInfluence);
    public delegate void SimulationControlHandler(SimulationControl simulationControl);

    public event ActionInfluenceHandler ActionInfluenceReceived = delegate { };
    public event PhysicalInfluenceHandler PhysicalInfluenceReceived = delegate { };
    public event SimulationControlHandler SimulationControlReceived = delegate { };

    void Start()
    {
        udpClient = new UdpClient(receptionPort);
        thread = new Thread(new ThreadStart(ThreadLoop));
        thread.Start();
    }

    void Stop()
    {
        thread.Abort();
        udpClient.Close();
    }

    public void EmitPerceptions(PerceptionList perceptionList)
    {
        var ipEndPoint = new IPEndPoint(IPAddress.Loopback, this.emissionPort);
        using (MemoryStream stream = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(perceptionList);
            }
            stream.Flush();
            var buffer = stream.GetBuffer();
            udpClient.Send(buffer, buffer.Length, ipEndPoint);
        }
    }

    private void ThreadLoop()
    {
        while(Thread.CurrentThread.IsAlive)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, receptionPort);
            var receivedBytes = udpClient.Receive(ref endPoint);

            ProcessPacket(receivedBytes);
        }
    }

    private void ProcessPacket(byte[] packet)
    {
        using(MemoryStream stream = new MemoryStream(packet))
        {
            using(BinaryReader reader = new BinaryReader(stream))
            {
                var type = reader.ReadSarlString();

                switch(type)
                {
                    case SimulationControl.SIMULATION_CONTROL:
                        {
                            var simulationControl = new SimulationControl();
                            simulationControl.Parse(reader.BaseStream);
                            SimulationControlReceived(simulationControl);
                            Debug.Log("Simulation Control received");
                        }
                        break;
                    case Influence.PHYSICAL_INFLUENCE:
                        {
                            var influence = new PhysicalInfluence();
                            influence.Parse(reader.BaseStream);
                            PhysicalInfluenceReceived(influence);
                            Debug.Log("Physical Influence received");
                        }
                        break;
                    case Influence.ACTION_INFLUENCE:
                        {
                            var influence = new ActionInfluence();
                            influence.Parse(reader.BaseStream);
                            ActionInfluenceReceived(influence);
                            Debug.Log("Action Influence received");
                        }
                        break;
                }
            }
        }
    }
}
