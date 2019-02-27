using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text;
using UnityEngine;

/// <summary>
/// The binary writer extensions
/// </summary>
public static class BinaryWriterExtensions
{
    /// <summary>
    /// Writes a string for the SARL environment
    /// </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="value">The string to write</param>
    public static void WriteSarlString(this BinaryWriter writer, string value)
    {
        writer.WriteInt32BE(value.Length + 1);
        writer.Write(Encoding.ASCII.GetBytes(value));
        writer.Write(false);
    }

    /// <summary>
    /// Writes a vector for the SARL environment
    /// </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="value">The vector to write</param>
    public static void Write(this BinaryWriter writer, Vector3 value)
    {
        writer.WriteFloatBE(value.x);
        writer.WriteFloatBE(value.y);
        writer.WriteFloatBE(value.z);
    }

    /// <summary>
    /// Writes a big-endian 32 bits integer
    /// </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="value">The int to write</param>
    public static void WriteInt32BE(this BinaryWriter writer, int value)
    {
        if (BitConverter.IsLittleEndian)
        {
            var buffer = BitConverter.GetBytes(value).Reverse();
            writer.Write(buffer, 0, buffer.Length);
        }
        else
        {
            writer.Write(value);
        }
    }

    /// <summary>
    /// Writes a big-endian float
    /// </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="value">The float to write</param>
    public static void WriteFloatBE(this BinaryWriter writer, float value)
    {
        if(BitConverter.IsLittleEndian)
        {
            var buffer = BitConverter.GetBytes(value).Reverse();
            writer.Write(buffer, 0, buffer.Length);
        }
        else
        {
            writer.Write(value);
        }
    }

    /// <summary>
    /// Writes a perception (for SARL)
    /// </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="perception">The perception to write</param>
    public static void Write(this BinaryWriter writer, Perception perception)
    {
        writer.Write(perception.Position);
    }

    /// <summary>
    /// Writes a list of perception (for SARL)
    /// </summary>
    /// <param name="writer">The binary writer</param>
    /// <param name="perceptionList">The list of perceptions</param>
    public static void Write(this BinaryWriter writer, PerceptionList perceptionList)
    {
        writer.WriteSarlString(perceptionList.Id);
        writer.WriteInt32BE(perceptionList.Perceptions.Count);
        foreach (var perception in perceptionList.Perceptions)
        {
            writer.Write(perception.Position);
        }
    }
}
