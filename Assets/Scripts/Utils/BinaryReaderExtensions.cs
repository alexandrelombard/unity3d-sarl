using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using UnityEngine;

/// <summary>
/// Extension methods for the BinaryReader
/// </summary>
public static class BinaryReaderExtensions
{
    /// <summary>
    /// Reverse a byte array, used for endianness conversion
    /// </summary>
    /// <param name="b">The array of bytes</param>
    /// <returns>The reversed array of bytes</returns>
    public static byte[] Reverse(this byte[] b)
    {
        Array.Reverse(b);
        return b;
    }

    /// <summary>
    /// Reads a string coming from the SARL environment (with length prefixed on 4 bytes - Big Endian)
    /// </summary>
    /// <param name="reader">The binary reader</param>
    /// <returns>The read string</returns>
    public static string ReadSarlString(this BinaryReader reader)
    {
        var stringLength = reader.ReadInt32BE();
        var stringBytes = reader.ReadBytes(stringLength);
        var str = System.Text.Encoding.UTF8.GetString(stringBytes, 0, stringLength - 1);
        return str;
    }

    /// <summary>
    /// Reads a vector coming from the SARL environment
    /// </summary>
    /// <param name="reader">The binary reader</param>
    /// <returns>The vector</returns>
    public static Vector3 ReadSarlVector(this BinaryReader reader)
    {
        return new Vector3(reader.ReadSingleBE(), reader.ReadSingleBE(), reader.ReadSingleBE());
    }

    /// <summary>
    /// Reads a big-endian 32 bits integer
    /// </summary>
    /// <param name="reader">The binary reader</param>
    /// <returns>The read int</returns>
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

    /// <summary>
    /// Reads a big-endian single precision float
    /// </summary>
    /// <param name="reader">The binary reader</param>
    /// <returns>The read single</returns>
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

    /// <summary>
    /// Reads a given amount of bytes
    /// </summary>
    /// <param name="binaryReader">The binary reader</param>
    /// <param name="byteCount">The number of bytes to read</param>
    /// <returns>The array of bytes</returns>
    public static byte[] ReadBytesRequired(this BinaryReader binaryReader, int byteCount)
    {
        var result = binaryReader.ReadBytes(byteCount);

        if (result.Length != byteCount)
            throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.", byteCount, result.Length));

        return result;
    }
}
