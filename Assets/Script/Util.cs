using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static List<int> GetRandomNums(int count, int maxNum)
    {
        List<int> result = new List<int>();

        while (result.Count < count)
        {
            bool isFail = false;
            int randNum = Random.Range(0, maxNum);

            foreach (int num in result)
            {
                if (num == randNum)
                {
                    isFail = true;
                    break;
                }
            }

            if (!isFail)
            {
                result.Add(randNum);
            }
        }

        return result;
    }
}
