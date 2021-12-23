using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSV_Reader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        string s_data = null;

        TextAsset data = Resources.Load(file) as TextAsset;
        s_data = data.text;

        var lines = Regex.Split(s_data, LINE_SPLIT_RE);
        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }

    public static string XORdecrypt(string text)
    {
        var decoded = System.Convert.FromBase64String(text);

        byte[] result = new byte[decoded.Length];

        for (int c = 0; c < decoded.Length; c++)
        {
            result[c] = (byte)((uint)decoded[c] ^ (uint)Manager_GAME.instance.Get_XOR_Key()[c % Manager_GAME.instance.Get_XOR_Key().Length]);
        }

        string dexored = Encoding.UTF8.GetString(result);

        return dexored;
    }
}
