using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CornerParser {

	public static bool IsGraphic(string _input)
    {
        int specialCharCount = 0;
        for(int i=0; i<_input.Length; i++)
        {
            char letter = _input[i];
            if (!isDigit(letter))
            {
                if(letter == '/')
                {
                    specialCharCount++;
                    if(specialCharCount > 1)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static void Parse(string _graphicString, ref int _edgeCost, ref int _edgeReturnCost)
    {
        int middleLetter = -1;

        for (int i = 0; i < _graphicString.Length; i++)
        {
            char letter = _graphicString[i];
            if (letter == '/')
            {
                middleLetter = i;
                break;
            }
        }
        int firstNumber = int.Parse(_graphicString.Substring(0, middleLetter));
        int secondNumber = int.Parse(_graphicString.Substring(middleLetter + 1));

        _edgeCost = secondNumber - firstNumber;
        _edgeReturnCost = firstNumber;
    }

    private static bool isDigit(char _input)
    {
        return ((_input >= '0') && (_input <= '9'));
    }
}
