using UnityEngine;

public class CachedTextCombiner
{
    private static readonly char[] resultChars = new char[20];
    
    public static char[] PushBackChar(string stringValue, char c, out int length)
    {
        length = 0;
        if(stringValue.Length + 1 > resultChars.Length)
        {
            Debug.LogWarning("cachedChar의 크기 부족");
            return null;
        }
        
        int resultCount = 0;
        for(int i = 0; i < stringValue.Length; i++)
        {
            resultChars[resultCount++] = stringValue[i];
        }
        resultChars[resultCount] = c;
        length = resultCount + 1;
        return resultChars;
    }
    
    public static char[] CombineTexts(string value1, string value2, char seperateChar = ' ')
    {
        if(value1.Length + value2.Length + 1 > resultChars.Length)
        {
            Debug.LogWarning("cachedChar의 크기 부족");
            return null;
        }
        
        int resultCount = 0;
        for(int i = 0; i < value1.Length; i++)
        {
            resultChars[resultCount++] = value1[i];
        }
        resultChars[resultCount++] = seperateChar;
        for(int i = 0; i < value2.Length; i++)
        {
            resultChars[resultCount++] = value2[i];   
        }
        return resultChars;
    }
    
    public static char[] CombineTexts(string stringValue, char[] charValue, int startIndex, int length, char seperateChar = ' ')
    {
        if(stringValue.Length + length + 1 > resultChars.Length)
        {
            Debug.LogWarning("cachedChar의 크기 부족");
            return null;
        }
        
        int resultCount = 0;
        for(int i = 0; i < stringValue.Length; i++)
        {
            resultChars[resultCount++] = stringValue[i];
        }
        resultChars[resultCount++] = seperateChar;
        for(int i = 0; i < length; i++)
        {
            resultChars[resultCount++] = charValue[i + startIndex];
        }
        return resultChars;
    }
    
    public static char[] CombineTexts(char[] charValue, int startIndex, int length, string stringValue, char seperateChar = ' ')
    {
        if(stringValue.Length + length + 1 > resultChars.Length)
        {
            Debug.LogWarning("cachedChar의 크기 부족");
            return null;
        }
        
        int resultCount = 0;
        for(int i = 0; i < length; i++)
        {
            resultChars[resultCount++] = charValue[i + startIndex];
        }
        resultChars[resultCount++] = seperateChar;
        for(int i = 0; i < stringValue.Length; i++)
        {
            resultChars[resultCount++] = stringValue[i];
        }                
        return resultChars;
    }
    
    public static char[] CombineTexts(char[] charValue1, int startIndex1, int length1, char[] charValue2, int startIndex2, int length2, char seperateChar = ' ')
    {
        if(length1 + length2 + 1 > resultChars.Length)
        {
            Debug.LogWarning("cachedChar의 크기 부족");
            return null;
        }
        
        int resultCount = 0;
        for(int i = 0; i < length1; i++)
        {
            resultChars[resultCount++] = charValue1[i + startIndex1];
        }
        resultChars[resultCount++] = seperateChar;
        for(int i = 0; i < length2; i++)
        {
            resultChars[resultCount++] = charValue2[i + startIndex2];
        }                
        return resultChars;
    }
}
