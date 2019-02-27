using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Represents a simulation control action
/// </summary>
public class SimulationControl
{
    public const string SIMULATION_CONTROL = "0001";

    public ActionType Type
    {
        get;
        private set;
    }
    public string Data
    {
        get;
        private set;
    }

    public void Parse(Stream stream)
    {
        using (BinaryReader reader = new BinaryReader(stream))
        {
            var actionOrdinal = reader.ReadInt32BE();
            switch(actionOrdinal)
            {
                case 0:
                    Type = ActionType.CreateBody;
                    break;
                case 1:
                    Type = ActionType.SetSimulationParameter;
                    break;
                case 2:
                    Type = ActionType.SimulationStart;
                    break;
                case 3:
                    Type = ActionType.SimulationStep;
                    break;
                case 4:
                    Type = ActionType.SimulationEnd;
                    break;
            }
            Data = reader.ReadSarlString();
        }
    }

    public enum ActionType
    {
        CreateBody,
        SetSimulationParameter,
        SimulationStart,
        SimulationStep,
        SimulationEnd
    }
}
