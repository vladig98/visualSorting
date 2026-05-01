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
        throw new NotImplementedException();
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
