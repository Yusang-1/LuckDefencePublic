using System.Text;
using UnityEngine;

public class CachedTextNumber
{
    private char[] cachedTexts = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
    private char[] resultChars = new char[10];
    private int resultLength;
    
    public char[] GetCachedText(float number, out int length)
    {
        resultLength = 0;
        int index;
        
        if(number < 0)
        {
            resultChars[resultLength++] = '-';
            number = -number;
        }
        
        if(number >= 0 && number < 1)
        {
            resultChars[resultLength++] = cachedTexts[0];
        }

        float max = 1;
        while (max < number)
        {
            max *= 10;
        }
        if(max > number)
        {
            max /= 10;
        }
        
        while (number != 0)
        {
            if(max == 0.1f)
            {
                resultChars[resultLength++] = cachedTexts[10];
            }
            
            index = (int)(number / max);
            resultChars[resultLength++] = cachedTexts[index];;
            number %= max;
            max /= 10;
            
            if(max < 0.01f) break;
        }
        
        while (max >= 1)
        {
            resultChars[resultLength++] = cachedTexts[0];
            max /= 10;
        }
        
        length = resultLength;
        return resultChars;
    }
}
