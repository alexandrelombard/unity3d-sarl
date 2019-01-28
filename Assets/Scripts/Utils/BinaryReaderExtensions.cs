using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using UnityEngine;

public static class BinaryReaderExtensions
{
    public static byte[] Reverse(this byte[] b)
    {
        Array.Reverse(b);
        return b;
    }

    public static string ReadSarlString(this BinaryReader reader)
    {
        var stringLength = reader.ReadInt32BE();
        var stringBytes = reader.ReadBytes(stringLength);
        var str = System.Text.Encoding.UTF8.GetString(stringBytes, 0, stringLength - 1);
        return str;
    }

    public static Vector3 ReadSarlVector(this BinaryReader reader)
    {
        return new Vector3(reader.ReadSingleBE(), reader.ReadSingleBE(), reader.ReadSingleBE());
    }

    public static int ReadInt32BE(this BinaryReader reader)
    {
        if(BitConverter.IsLittleEndian)
        {
            return BitConverter.ToInt32(reader.ReadBytesRequired(sizeof(int)).Reverse(), 0);
        }
        else
        {
            return reader.ReadInt32();
        }
    }

    public static float ReadSingleBE(this BinaryReader reader)
    {
        if(BitConverter.IsLittleEndian)
        {
            return BitConverter.ToSingle(reader.ReadBytesRequired(sizeof(float)).Reverse(), 0);
        }
        else
        {
            return reader.ReadSingle();
        }
    }

    public static byte[] ReadBytesRequired(this BinaryReader binaryReader, int byteCount)
    {
        var result = binaryReader.ReadBytes(byteCount);

        if (result.Length != byteCount)
            throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.", byteCount, result.Length));

        return result;
    }
}
