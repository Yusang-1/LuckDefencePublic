using System.Text;
using UnityEngine;

public class TimerCachedText
{
    private char[] cachedTexts = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
    private char[] resultChars = new char[10];
    private int resultLength;
    
    public char[] GetCachedText(float time, out int length)
    {
        resultLength = 0;
        int index;

        float max = 1;
        while (max < time)
        {
            max *= 10;
        }
        if(max > time)
        {
            max /= 10;
        }
        
        while (time != 0)
        {
            if(max == 0.1f)
            {
                resultChars[resultLength++] = cachedTexts[10];
            }
            
            index = (int)(time / max);
            resultChars[resultLength++] = cachedTexts[index];;
            time %= max;
            max /= 10;
            
            if(max < 0.01f) break;
        }
        
        length = resultLength;
        return resultChars;
    }
}
