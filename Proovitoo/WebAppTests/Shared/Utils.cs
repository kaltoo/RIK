using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;

namespace WebAppTests;

public class Utils
{
    public enum RandomMode
    {
        LettersAndNumbers,
        LettersOnly,
        NumbersOnly
    }

    private static readonly Random random = new();

    public static string RandomString(int length, RandomMode mode)
    {
        string chars;

        switch (mode)
        {
            case RandomMode.LettersAndNumbers:
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                break;
            case RandomMode.LettersOnly:
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                break;
            case RandomMode.NumbersOnly:
                chars = "0123456789";
                break;
            default:
                throw new InvalidDataException();
        }

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GetInputForDateBox(DateTime input)
    {
        var _temp = input.ToString("dd.MM.yyyy.HH.mm").Split(".");
        return $"{_temp[0]}{_temp[1]}{_temp[2]}{Keys.Tab}{_temp[3]}{_temp[4]}";
    }
}