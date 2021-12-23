using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

public class CSV_Writer
{
    // CSV
    static DirectoryInfo Folder_Info;
    static FileInfo File_Info;
    static StreamWriter Stream_Writer;

    // item UI Setting
    static string s_strFolder;

    // Set DataPath
    public static void DataPath(string value)
    {
        if (Application.platform == RuntimePlatform.Android) s_strFolder = string.Format("{0}/CSV/", Application.persistentDataPath);
        else s_strFolder = string.Format("{0}/Resources/CSV/", Application.dataPath);

        Folder_Info = new DirectoryInfo(s_strFolder);
        if (!Folder_Info.Exists)
            Folder_Info.Create();

        s_strFolder = s_strFolder + value + ".csv";

        Stream_Writer = new StreamWriter(s_strFolder);
    }

    // WriteLine contain XORencrypt
    public static void W_XORencrypt(string text)
    {
        byte[] decrypted = Encoding.UTF8.GetBytes(text);
        byte[] encrypted = new byte[decrypted.Length];

        for (int i = 0; i < decrypted.Length; i++)
        {
            encrypted[i] = (byte)(decrypted[i] ^ Manager_GAME.instance.Get_XOR_Key()[i % Manager_GAME.instance.Get_XOR_Key().Length]);
        }

        string xored = System.Convert.ToBase64String(encrypted);

        Stream_Writer.WriteLine(xored);
    }

    // WriteLine not contain XORencrypt
    public static string R_XORencrypt(string text)
    {
        byte[] decrypted = Encoding.UTF8.GetBytes(text);
        byte[] encrypted = new byte[decrypted.Length];

        for (int i = 0; i < decrypted.Length; i++)
        {
            encrypted[i] = (byte)(decrypted[i] ^ Manager_GAME.instance.Get_XOR_Key()[i % Manager_GAME.instance.Get_XOR_Key().Length]);
        }

        string xored = System.Convert.ToBase64String(encrypted);

        return xored;
    }
}