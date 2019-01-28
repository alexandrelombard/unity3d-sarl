using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Text;
using UnityEngine;

public static class BinaryWriterExtensions
{
    public static void WriteSarlString(this BinaryWriter writer, string value)
    {
        writer.WriteInt32BE(value.Length + 1);
        writer.Write(Encoding.ASCII.GetBytes(value));
        writer.Write(false);
    }

    public static void Write(this BinaryWriter writer, Vector3 value)
    {
        writer.WriteFloatBE(value.x);
        writer.WriteFloatBE(value.y);
        writer.WriteFloatBE(value.z);
    }

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

    public static void Write(this BinaryWriter writer, Perception perception)
    {
        writer.Write(perception.Position);
    }

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
