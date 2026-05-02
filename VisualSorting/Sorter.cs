using static System.Runtime.InteropServices.JavaScript.JSType;

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
        int gap = numbers.Length;
        double shrink = 1.3;
        bool isSorted = false;

        while (!isSorted)
        {
            gap = (int)Math.Floor(gap / shrink);
            if (gap <= 1)
            {
                gap = 1;
                isSorted = true;
            }
            else if (gap == 9 || gap == 10)
            {
                gap = 11;
            }

            int i = 0;
            while (i + gap < numbers.Length)
            {
                if (numbers[i] > numbers[i + gap])
                {
                    (numbers[i], numbers[i + gap]) = (numbers[i + gap], numbers[i]);
                    isSorted = false;

                    yield return gap;
                }

                i++;
            }
        }
    }

    public static IEnumerable<int> CycleSort(int[] numbers)
    {
        for (int cycleStart = 0; cycleStart <= numbers.Length - 2; cycleStart++)
        {
            int item = numbers[cycleStart];
            int pos = cycleStart;

            for (int i = cycleStart + 1; i < numbers.Length; i++)
            {
                if (numbers[i] < item)
                {
                    pos++;
                }
            }

            if (pos == cycleStart)
            {
                continue;
            }

            while (item == numbers[pos])
            {
                pos++;
            }

            (numbers[pos], item) = (item, numbers[pos]);
            yield return pos;

            while (pos != cycleStart)
            {
                pos = cycleStart;

                for (int i = cycleStart + 1; i < numbers.Length; i++)
                {
                    if (numbers[i] < item)
                    {
                        pos++;
                    }
                }

                while (item == numbers[pos])
                {
                    pos++;
                }

                (numbers[pos], item) = (item, numbers[pos]);
                yield return pos;
            }
        }
    }

    public static IEnumerable<int> HeapSort(int[] numbers)
    {
        int count = numbers.Length;
        int start = count / 2;
        int end = count;

        while (end > 1)
        {
            if (start > 0)
            {
                start--;
            }
            else
            {
                end--;
                (numbers[end], numbers[0]) = (numbers[0], numbers[end]);
                yield return end;
            }

            int root = start;
            while ((root * 2 + 1) < end)
            {
                int child = root * 2 + 1;

                if (child + 1 < end && numbers[child] < numbers[child + 1])
                {
                    child++;
                }

                if (numbers[root] < numbers[child])
                {
                    (numbers[root], numbers[child]) = (numbers[child], numbers[root]);
                    root = child;
                    yield return root;
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static IEnumerable<int> IntroSort(int[] numbers)
    {
        int maxDepth = (int)Math.Floor(Math.Log2(numbers.Length)) * 2;

        return IntroSortInternal(numbers, 0, numbers.Length - 1, maxDepth);
    }

    public static IEnumerable<int> InPlaceMergeSort(int[] numbers)
    {
        return InPlaceMergeSortInternal(numbers, 0, numbers.Length - 1);
    }

    public static IEnumerable<int> TournamentSort(int[] numbers)
    {
        List<TournamentNode> forest = [];

        foreach (int n in numbers)
        {
            forest.Add(new TournamentNode { Value = n });
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            TournamentNode? winner = PlayTournament(forest);
            if (winner is null)
            {
                continue;
            }

            numbers[i] = winner.Value;

            yield return i;
            forest = winner.Defeated;
        }
    }

    public static IEnumerable<int> TreeSort(int[] numbers)
    {
        if (numbers.Length == 0)
        {
            yield break;
        }

        TreeNode root = new(numbers[0]);
        for (int i = 1; i < numbers.Length; i++)
        {
            Insert(root, numbers[i]);
            yield return i;
        }

        int index = 0;
        foreach (int val in TraverseInOrder(root))
        {
            numbers[index] = val;
            yield return index;
            index++;
        }
    }

    public static IEnumerable<int> BlockSort(int[] numbers)
    {
        int n = numbers.Length;
        if (n < 2)
        {
            yield break;
        }

        int blockSize = 16;

        for (int i = 0; i < n; i += blockSize)
        {
            int end = Math.Min(i + blockSize - 1, n - 1);
            foreach (int frame in InsertionSortRange(numbers, i, end))
            {
                yield return frame;
            }
        }

        for (int size = blockSize; size < n; size *= 2)
        {
            for (int left = 0; left < n; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + 2 * size - 1, n - 1);

                if (mid < right)
                {
                    foreach (int frame in InPlaceMerge(numbers, left, mid, right))
                    {
                        yield return frame;
                    }
                }
            }
        }
    }

    public static IEnumerable<int> SmoothSort(int[] numbers)
    {
        int n = numbers.Length;
        if (n < 2)
        {
            yield break;
        }

        int p = 1, b = 1, c = 1;

        // 1. BUILD PHASE
        for (int i = 0; i < n; i++)
        {
            if ((p & 3) == 3)
            {
                p >>= 2;
                Up(ref b, ref c);
                Up(ref b, ref c);
                p++;
            }
            else if (b > 1 && i + c + 1 >= n)
            {
                p <<= (b - 1);
            }
            else
            {
                if (b == 1)
                {
                    p <<= 1;
                }
                else
                {
                    p <<= b - 1;
                }

                Down(ref b, ref c);
                p |= 1;
            }
            foreach (int f in Trinkle(numbers, i, p, b, c))
            {
                yield return f;
            }
        }

        // 2. EXTRACTION PHASE
        for (int i = n - 1; i > 0; i--)
        {
            if (b == 1)
            {
                p--;
                while (p > 0 && (p & 1) == 0)
                {
                    p >>= 1;
                    Up(ref b, ref c);
                }
            }
            else if (b >= 3)
            {
                p--;
                int leftChildIdx = i - b + c;
                int rightChildIdx = i - 1;

                p <<= 1;
                p |= 1;
                int bLeft = b, cLeft = c;
                Down(ref bLeft, ref cLeft);

                foreach (int f in Trinkle(numbers, leftChildIdx, p, bLeft, cLeft))
                {
                    yield return f;
                }

                p <<= 1;
                p |= 1;
                Down(ref b, ref c);
                Down(ref b, ref c);

                foreach (int f in Trinkle(numbers, rightChildIdx, p, b, c))
                {
                    yield return f;
                }
            }

            yield return i;
        }
    }

    public static IEnumerable<int> TimSort(int[] numbers)
    {
        int n = numbers.Length;
        int minRun = GetMinRun(n);

        for (int i = 0; i < n; i += minRun)
        {
            int end = Math.Min(i + minRun - 1, n - 1);
            foreach (int frame in InsertionSortRange(numbers, i, end))
            {
                yield return frame;
            }
        }

        for (int size = minRun; size < n; size = 2 * size)
        {
            for (int left = 0; left < n; left += 2 * size)
            {
                int mid = left + size - 1;
                int right = Math.Min(left + 2 * size - 1, n - 1);

                if (mid < right)
                {
                    foreach (int frame in InPlaceMerge(numbers, left, mid, right))
                    {
                        yield return frame;
                    }
                }
            }
        }
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

    private static IEnumerable<int> IntroSortInternal(int[] numbers, int left, int right, int depth)
    {
        int n = right - left + 1;

        if (n < 16)
        {
            foreach (var frame in InsertionSortRange(numbers, left, right))
            {
                yield return frame;
            }
        }
        else if (depth == 0)
        {
            foreach (var frame in HeapSortRange(numbers, left, right))
            {
                yield return frame;
            }
        }
        else
        {
            int pivotIndex = Partition(numbers, left, right);
            yield return pivotIndex;

            foreach (int frame in IntroSortInternal(numbers, left, pivotIndex - 1, depth - 1))
            {
                yield return frame;
            }

            foreach (int frame in IntroSortInternal(numbers, pivotIndex + 1, right, depth - 1))
            {
                yield return frame;
            }
        }
    }

    private static IEnumerable<int> InsertionSortRange(int[] numbers, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int temp = numbers[i];
            int j = i;

            while (j > left && numbers[j - 1] > temp)
            {
                numbers[j] = numbers[j - 1];
                j--;
                yield return j;
            }
            numbers[j] = temp;
            yield return j;
        }
    }

    private static IEnumerable<int> HeapSortRange(int[] numbers, int left, int right)
    {
        int n = right - left + 1;

        for (int i = n / 2 - 1; i >= 0; i--)
        {
            foreach (int res in SiftDownRange(numbers, i, n, left))
            {
                yield return res;
            }
        }

        for (int i = n - 1; i > 0; i--)
        {
            (numbers[left], numbers[left + i]) = (numbers[left + i], numbers[left]);
            yield return left + i;

            foreach (int res in SiftDownRange(numbers, 0, i, left))
            {
                yield return res;
            }
        }
    }

    private static IEnumerable<int> SiftDownRange(int[] numbers, int root, int n, int offset)
    {
        while (root * 2 + 1 < n)
        {
            int child = root * 2 + 1;
            if (child + 1 < n && numbers[offset + child] < numbers[offset + child + 1])
            {
                child++;
            }

            if (numbers[offset + root] < numbers[offset + child])
            {
                (numbers[offset + root], numbers[offset + child]) = (numbers[offset + child], numbers[offset + root]);
                root = child;
                yield return offset + root;
            }
            else
            {
                break;
            }
        }
    }

    private static int Partition(int[] numbers, int left, int right)
    {
        int pivot = numbers[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (numbers[j] <= pivot)
            {
                i++;
                (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
            }
        }

        (numbers[i + 1], numbers[right]) = (numbers[right], numbers[i + 1]);
        return i + 1;
    }

    private static IEnumerable<int> InPlaceMergeSortInternal(int[] numbers, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            foreach (int res in InPlaceMergeSortInternal(numbers, left, mid))
            {
                yield return res;
            }

            foreach (int res in InPlaceMergeSortInternal(numbers, mid + 1, right))
            {
                yield return res;
            }

            foreach (int res in InPlaceMerge(numbers, left, mid, right))
            {
                yield return res;
            }
        }
    }

    private static IEnumerable<int> InPlaceMerge(int[] numbers, int start, int mid, int end)
    {
        int start2 = mid + 1;
        if (numbers[mid] <= numbers[start2])
        {
            yield break;
        }

        while (start <= mid && start2 <= end)
        {
            if (numbers[start] <= numbers[start2])
            {
                start++;
            }
            else
            {
                int value = numbers[start2];
                int index = start2;

                while (index != start)
                {
                    numbers[index] = numbers[index - 1];
                    index--;
                    yield return index;
                }

                numbers[start] = value;
                yield return start;

                start++;
                mid++;
                start2++;
            }
        }
    }

    private static TournamentNode? PlayTournament(List<TournamentNode> nodes)
    {
        if (nodes.Count == 0)
        {
            return null;
        }

        List<TournamentNode> currentRound = nodes;
        while (currentRound.Count > 1)
        {
            List<TournamentNode> nextRound = [];
            for (int i = 0; i < currentRound.Count; i += 2)
            {
                if (i + 1 < currentRound.Count)
                {
                    TournamentNode node1 = currentRound[i];
                    TournamentNode node2 = currentRound[i + 1];

                    if (node1.Value <= node2.Value)
                    {
                        node1.Defeated.Add(node2);
                        nextRound.Add(node1);
                    }
                    else
                    {
                        node2.Defeated.Add(node1);
                        nextRound.Add(node2);
                    }
                }
                else
                {
                    nextRound.Add(currentRound[i]);
                }
            }
            currentRound = nextRound;
        }

        return currentRound[0];
    }

    private static void Insert(TreeNode root, int value)
    {
        TreeNode current = root;
        while (true)
        {
            if (value <= current.Value)
            {
                if (current.Left == null)
                {
                    current.Left = new TreeNode(value);
                    break;
                }
                current = current.Left;
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = new TreeNode(value);
                    break;
                }
                current = current.Right;
            }
        }
    }

    private static IEnumerable<int> TraverseInOrder(TreeNode? node)
    {
        if (node == null)
        {
            yield break;
        }

        foreach (int val in TraverseInOrder(node.Left))
        {
            yield return val;
        }

        yield return node.Value;

        foreach (int val in TraverseInOrder(node.Right))
        {
            yield return val;
        }
    }

    private static IEnumerable<int> Trinkle(int[] numbers, int r, int p, int b, int c)
    {
        while (p > 0)
        {
            while ((p & 1) == 0)
            {
                p >>= 1;
                Up(ref b, ref c);
            }

            if (p == 1)
            {
                break;
            }

            int prevRoot = r - b;
            if (prevRoot < 0 || numbers[prevRoot] <= numbers[r])
            {
                break;
            }

            if (b >= 3)
            {
                int rLeft = r - b + c;
                int rRight = r - 1;
                if (numbers[prevRoot] <= numbers[rLeft] || numbers[prevRoot] <= numbers[rRight])
                {
                    break;
                }
            }

            (numbers[r], numbers[prevRoot]) = (numbers[prevRoot], numbers[r]);
            r = prevRoot;
            p--;
            yield return r;
        }

        foreach (int f in Sift(numbers, r, b, c))
        {
            yield return f;
        }
    }

    private static IEnumerable<int> Sift(int[] numbers, int r, int b, int c)
    {
        while (b >= 3)
        {
            int r2 = r - b + c;
            int r3 = r - 1;    

            if (r2 < 0 || r3 < 0)
            {
                break;
            }

            if (numbers[r2] < numbers[r3])
            {
                r2 = r3;
                Down(ref b, ref c);
            }
            else
            {
                Down(ref b, ref c);
                Down(ref b, ref c);
            }

            if (numbers[r] >= numbers[r2])
            {
                break;
            }

            (numbers[r], numbers[r2]) = (numbers[r2], numbers[r]);
            r = r2;
            yield return r;
        }
    }

    private static void Up(ref int b, ref int c) 
    { 
        int t = b; 
        b = b + c + 1; 
        c = t; 
    }

    private static void Down(ref int b, ref int c) 
    { 
        int t = c; 
        c = b - c - 1; 
        b = t; 
    }

    private static int GetMinRun(int n)
    {
        int r = 0;
        while (n >= 32)
        {
            r |= (n & 1);
            n >>= 1;
        }
        return n + r;
    }
}
