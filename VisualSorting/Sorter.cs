namespace VisualSorting;

public static class Sorter
{
    public static IEnumerable<int> BubbleSort(int[] numbers)
    {
        bool swapped;

        do
        {
            swapped = false;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] <= numbers[i + 1])
                {
                    continue;
                }

                (numbers[i], numbers[i + 1]) = (numbers[i + 1], numbers[i]);
                swapped = true;

                yield return numbers[i];
            }
        } while (swapped);
    }

    public static IEnumerable<int> SelectionSort(int[] numbers)
    {
        int index = 0;

        while (index < numbers.Length - 1)
        {
            int currentIndex = -1;

            for (int i = index; i < numbers.Length; i++)
            {
                if (currentIndex >= 0 && numbers[i] >= numbers[currentIndex])
                {
                    continue;
                }

                currentIndex = i;
            }

            (numbers[index], numbers[currentIndex]) = (numbers[currentIndex], numbers[index]);
            index++;

            yield return numbers[currentIndex];
        }
    }

    public static IEnumerable<int> InsertionSort(int[] numbers)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int index = -1;
            int current = numbers[i];

            for (int j = i - 1; j >= 0; j--)
            {
                if (current >= numbers[j])
                {
                    break;
                }

                index = j;
                numbers[j + 1] = numbers[j];

                yield return numbers[j];
            }

            if (index == i || index < 0)
            {
                continue;
            }

            numbers[index] = current;
            yield return numbers[index];
        }
    }

    public static IEnumerable<int> MergeSort(int[] numbers)
    {
        List<List<int>> subLists = [];
        for (int i = 0; i < numbers.Length; i++)
        {
            subLists.Add([numbers[i]]);
        }

        while (subLists.Count > 1)
        {
            for (int i = 0; i < subLists.Count - 1; i++)
            {
                List<int> subListA = subLists[i];
                List<int> subListB = subLists[i + 1];
                List<int> merged = [];

                int startOffset = subLists.Take(i).Sum(list => list.Count);
                int totalToMerge = subListA.Count + subListB.Count;

                int aPtr = 0, bPtr = 0;
                while (aPtr < subListA.Count || bPtr < subListB.Count)
                {
                    int valA = aPtr < subListA.Count ? subListA[aPtr] : int.MaxValue;
                    int valB = bPtr < subListB.Count ? subListB[bPtr] : int.MaxValue;

                    if (valA <= valB)
                    {
                        merged.Add(valA);
                        aPtr++;
                    }
                    else
                    {
                        merged.Add(valB);
                        bPtr++;
                    }

                    for (int j = 0; j < merged.Count; j++)
                    {
                        numbers[startOffset + j] = merged[j];
                    }

                    yield return startOffset + merged.Count - 1;
                }

                subLists[i] = merged;
                subLists.RemoveAt(i + 1);
            }
        }
    }

    public static IEnumerable<int> QuickSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> ShellSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> CocktailShakerSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BogoSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> StalinSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> SleepSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> StoogeSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> ThanosSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> MiracleSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> GravitySort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> QuantumBogoSort(int[] numbers)
    {
        throw new NotImplementedException();
    }
}
