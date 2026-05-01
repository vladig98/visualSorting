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
        Stack<(int left, int right)> stack = new();
        stack.Push((0, numbers.Length - 1));

        while (stack.Count > 0)
        {
            (int left, int right) = stack.Pop();
            if (left >= right)
            {
                continue;
            }

            int pivot = numbers[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;
                    (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
                    yield return i;
                }
            }

            (numbers[i + 1], numbers[right]) = (numbers[right], numbers[i + 1]);
            int pivotIndex = i + 1;
            yield return pivotIndex;

            stack.Push((pivotIndex + 1, right));
            stack.Push((left, pivotIndex - 1));
        }
    }

    public static IEnumerable<int> ShellSort(int[] numbers)
    {
        int n = numbers.Length;
        int[] gaps = [701, 301, 132, 57, 23, 10, 4, 1];

        foreach (int gap in gaps)
        {
            if (gap >= n)
            {
                continue;
            }

            for (int i = gap; i < n; i++)
            {
                int temp = numbers[i];
                int j = i;

                while (j >= gap && numbers[j - gap] > temp)
                {
                    numbers[j] = numbers[j - gap];
                    j -= gap;

                    yield return j;
                }

                numbers[j] = temp;
                yield return j;
            }
        }
    }

    public static IEnumerable<int> CocktailShakerSort(int[] numbers)
    {
        bool swapped;

        do
        {
            swapped = false;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] > numbers[i + 1])
                {
                    (numbers[i], numbers[i + 1]) = (numbers[i + 1], numbers[i]);
                    swapped = true;
                    yield return i;
                }
            }

            if (!swapped)
            {
                break;
            }

            swapped = false;
            for (int i = numbers.Length - 2; i >= 0; i--)
            {
                if (numbers[i] > numbers[i + 1])
                {
                    (numbers[i], numbers[i + 1]) = (numbers[i + 1], numbers[i]);
                    swapped = true;
                    yield return i;
                }
            }
        } while (swapped);
    }

    public static IEnumerable<int> GnomeSort(int[] numbers)
    {
        int index = 1;
        while (index < numbers.Length)
        {
            if (index == 0 || numbers[index] >= numbers[index - 1])
            {
                index++;
            }
            else
            {
                (numbers[index], numbers[index - 1]) = (numbers[index - 1], numbers[index]);
                index--;

                yield return index;
            }
        }
    }

    public static IEnumerable<int> OddEvenSort(int[] numbers)
    {
        bool isSorted = false;
        while (!isSorted)
        {
            isSorted = true;
            for (int i = 1; i < numbers.Length - 1; i += 2)
            {
                if (numbers[i] > numbers[i + 1])
                {
                    (numbers[i], numbers[i + 1]) = (numbers[i + 1], numbers[i]);
                    isSorted = false;

                    yield return i;
                }
            }
            for (int i = 0; i < numbers.Length - 1; i += 2)
            {
                if (numbers[i] > numbers[i + 1])
                {
                    (numbers[i], numbers[i + 1]) = (numbers[i + 1], numbers[i]);
                    isSorted = false;

                    yield return i;
                }
            }
        }
    }

    public static IEnumerable<int> CombSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> CycleSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> HeapSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> IntroSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> InPlaceMergeSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> TournamentSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> TreeSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BlockSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> SmoothSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> TimSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> PatienceSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> CubeSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> FluxSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> CrumSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> LibrarySort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> StrandSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> MergeInsertionSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> PigeonholeSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BucketUniformSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BucketIntegerSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> CountingSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> LSDRadixSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> MSDRadixSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> MSDRadixInPlaceSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> SpreadSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BurstSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> FlashSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> SampleSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BitonicSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> SortingNetworkSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> GravitySort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> SpaghettiSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<int> BogoSort(int[] numbers)
    {
        while (!IsSorted(numbers))
        {
            int n = numbers.Length;
            while (n > 1)
            {
                n--;
                int k = Random.Shared.Next(n + 1);
                (numbers[n], numbers[k]) = (numbers[k], numbers[n]);
            }

            yield return 0;
        }
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

    public static IEnumerable<int> SlowSort(int[] numbers)
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

    public static IEnumerable<int> QuantumBogoSort(int[] numbers)
    {
        throw new NotImplementedException();
    }

    private static bool IsSorted(int[] numbers)
    {
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            if (numbers[i] > numbers[i + 1])
            {
                return false;
            }
        }

        return true;
    }
}
