using UnityEngine;
using System.Collections;

public static class MiscExtensionMethods
{

    /// <summary>
    /// returns MD5 hex digest of this array.
    /// </summary>
    /// <returns>
    /// The md5.
    /// </returns>
    /// <param name='bytes'>
    /// Bytes.
    /// </param>
    public static string MD5(this byte[] bytes) {
        var md5 = System.Security.Cryptography.MD5.Create();
        var hash = md5.ComputeHash (bytes);
        var hex = "";
        for (var i = 0; i < hash.Length; i++) {
            hex += System.Convert.ToString (hash [i], 16).PadLeft (2, '0');
        }
        return hex.PadLeft (32, '0');
    }

    /// <summary>
    /// MD5 Hex Digest of this string.
    /// </summary>
    /// <returns>
    /// The md5.
    /// </returns>
    /// <param name='text'>
    /// Text.
    /// </param>
    public static string MD5 (this string text)
    {
        var bytes = System.Text.UTF8Encoding.UTF8.GetBytes (text);
        return MD5(bytes);
    }

    /// <summary>
    /// Encrypts and Decrypts a string.
    /// </summary>
    /// <returns>
    /// The c4.
    /// </returns>
    /// <param name='text'>
    /// Text.
    /// </param>
    /// <param name='skey'>
    /// Skey.
    /// </param>
    public static string RC4 (this string text, string skey)
    {
        var bytes = System.Text.ASCIIEncoding.ASCII.GetBytes (text);
        bytes.RC4 (skey);
        return System.Text.ASCIIEncoding.ASCII.GetString (bytes);
    }

    /// <summary>
    /// Encrypts and Decrypts a byte array.
    /// </summary>
    /// <param name='bytes'>
    /// Bytes.
    /// </param>
    /// <param name='skey'>
    /// Skey.
    /// </param>
    public static void RC4 (this byte[] bytes, string skey)
    {
        var key = System.Text.ASCIIEncoding.ASCII.GetBytes (skey);
        byte[] s = new byte[256];
        byte[] k = new byte[256];
        byte temp;
        int i, j;

        for (i = 0; i < 256; i++) {
            s [i] = (byte)i;
            k [i] = key [i % key.GetLength (0)];
        }

        j = 0;
        for (i = 0; i < 256; i++) {
            j = (j + s [i] + k [i]) % 256;
            temp = s [i];
            s [i] = s [j];
            s [j] = temp;
        }

        i = j = 0;
        for (int x = 0; x < bytes.GetLength (0); x++) {
            i = (i + 1) % 256;
            j = (j + s [i]) % 256;
            temp = s [i];
            s [i] = s [j];
            s [j] = temp;
            int t = (s [i] + s [j]) % 256;
            bytes [x] ^= s [t];
        }
    }


}

