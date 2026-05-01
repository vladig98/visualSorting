namespace VisualSorting;

public static class WindowManager
{
    private static IWindow? _window;
    private static GL? _gl;

    // OpenGL Handles
    private static uint _vao;
    private static uint _vbo;
    private static uint _ebo;
    private static uint _program;

    // State
    private static int[] _numbers = [];
    private const int BaseWidth = 1800;
    private const int BaseHeight = 600;

    private static IEnumerator<int>? _activeSort;

    private const string VertexShaderSource = @"#version 330 core
        layout (location = 0) in vec3 aPosition;
        void main() { gl_Position = vec4(aPosition, 1.0); }";

    private const string FragmentShaderSource = @"#version 330 core
        out vec4 out_color;
        void main() { out_color = vec4(1.0, 0.5, 0.2, 1.0); }";

    public static void Initialize(SortingType type, int numbersLength = 100)
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(BaseWidth, BaseHeight),
            Title = "Sorting Visualizer",
            FramesPerSecond = 10
        };

        _window = Window.Create(options);
        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
        _window.FramebufferResize += (size) => _gl?.Viewport(0, 0, (uint)size.X, (uint)size.Y);

        _numbers = new int[numbersLength];
        for (int i = 0; i < numbersLength; i++)
        {
            _numbers[i] = Random.Shared.Next(10, BaseHeight);
        }

        _activeSort = type switch
        {
            // --- Standard Comparison Sorts ---
            SortingType.Bubble => Sorter.BubbleSort(_numbers).GetEnumerator(),
            SortingType.Selection => Sorter.SelectionSort(_numbers).GetEnumerator(),
            SortingType.Insertion => Sorter.InsertionSort(_numbers).GetEnumerator(),
            SortingType.Merge => Sorter.MergeSort(_numbers).GetEnumerator(),
            SortingType.Quick => Sorter.QuickSort(_numbers).GetEnumerator(),
            SortingType.Shell => Sorter.ShellSort(_numbers).GetEnumerator(),
            SortingType.CocktailShaker => Sorter.CocktailShakerSort(_numbers).GetEnumerator(),
            SortingType.Gnome => Sorter.GnomeSort(_numbers).GetEnumerator(),
            SortingType.OddEven => Sorter.OddEvenSort(_numbers).GetEnumerator(),
            SortingType.Comb => Sorter.CombSort(_numbers).GetEnumerator(),
            SortingType.Cycle => Sorter.CycleSort(_numbers).GetEnumerator(),

            // --- Advanced Comparison (Hybrid/Efficient) ---
            SortingType.Heap => Sorter.HeapSort(_numbers).GetEnumerator(),
            SortingType.Intro => Sorter.IntroSort(_numbers).GetEnumerator(),
            SortingType.InPlaceMerge => Sorter.InPlaceMergeSort(_numbers).GetEnumerator(),
            SortingType.Tournament => Sorter.TournamentSort(_numbers).GetEnumerator(),
            SortingType.Tree => Sorter.TreeSort(_numbers).GetEnumerator(),
            SortingType.Block => Sorter.BlockSort(_numbers).GetEnumerator(),
            SortingType.Smooth => Sorter.SmoothSort(_numbers).GetEnumerator(),
            SortingType.Tim => Sorter.TimSort(_numbers).GetEnumerator(),
            SortingType.Patience => Sorter.PatienceSort(_numbers).GetEnumerator(),
            SortingType.Cube => Sorter.CubeSort(_numbers).GetEnumerator(),
            SortingType.Flux => Sorter.FluxSort(_numbers).GetEnumerator(),
            SortingType.Crum => Sorter.CrumSort(_numbers).GetEnumerator(),
            SortingType.Library => Sorter.LibrarySort(_numbers).GetEnumerator(),
            SortingType.Strand => Sorter.StrandSort(_numbers).GetEnumerator(),
            SortingType.MergeInsertion => Sorter.MergeInsertionSort(_numbers).GetEnumerator(),

            // --- Distribution / Non-Comparison Sorts ---
            SortingType.Pigeonhole => Sorter.PigeonholeSort(_numbers).GetEnumerator(),
            SortingType.BucketUniform => Sorter.BucketUniformSort(_numbers).GetEnumerator(),
            SortingType.BucketInteger => Sorter.BucketIntegerSort(_numbers).GetEnumerator(),
            SortingType.Counting => Sorter.CountingSort(_numbers).GetEnumerator(),
            SortingType.LSDRadix => Sorter.LSDRadixSort(_numbers).GetEnumerator(),
            SortingType.MSDRadix => Sorter.MSDRadixSort(_numbers).GetEnumerator(),
            SortingType.MSDRadixInPlace => Sorter.MSDRadixInPlaceSort(_numbers).GetEnumerator(),
            SortingType.Spread => Sorter.SpreadSort(_numbers).GetEnumerator(),
            SortingType.Burst => Sorter.BurstSort(_numbers).GetEnumerator(),
            SortingType.Flash => Sorter.FlashSort(_numbers).GetEnumerator(),
            SortingType.Sample => Sorter.SampleSort(_numbers).GetEnumerator(),

            // --- Concurrent / Hardware Sorts ---
            SortingType.Bitonic => Sorter.BitonicSort(_numbers).GetEnumerator(),
            SortingType.SortingNetwork => Sorter.SortingNetworkSort(_numbers).GetEnumerator(),
            SortingType.Gravity => Sorter.GravitySort(_numbers).GetEnumerator(),
            SortingType.Spaghetti => Sorter.SpaghettiSort(_numbers).GetEnumerator(),

            // --- The "Esoteric" & Meme Sorts ---
            SortingType.Bogo => Sorter.BogoSort(_numbers).GetEnumerator(),
            SortingType.Stalin => Sorter.StalinSort(_numbers).GetEnumerator(),
            SortingType.Sleep => Sorter.SleepSort(_numbers).GetEnumerator(),
            SortingType.Stooge => Sorter.StoogeSort(_numbers).GetEnumerator(),
            SortingType.Slow => Sorter.SlowSort(_numbers).GetEnumerator(),
            SortingType.Thanos => Sorter.ThanosSort(_numbers).GetEnumerator(),
            SortingType.Miracle => Sorter.MiracleSort(_numbers).GetEnumerator(),
            SortingType.QuantumBogo => Sorter.QuantumBogoSort(_numbers).GetEnumerator(),

            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Sort {type} not handled.")
        };

        _window.Run();
    }

    private static unsafe void OnLoad()
    {
        _gl = _window!.CreateOpenGL();
        SetupInput();

        // 1. Shaders
        _program = CreateProgram(VertexShaderSource, FragmentShaderSource);

        // 2. Buffer Initialization
        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);

        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        _ebo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

        // 3. Vertex Attributes (Telling OpenGL how to read the VBO)
        _gl.EnableVertexAttribArray(0);
        _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

        _gl.ClearColor(Color.CornflowerBlue);
    }

    private static unsafe void OnRender(double deltaTime)
    {
        if (_gl is null || _window is null)
        {
            return;
        }

        _gl.Clear(ClearBufferMask.ColorBufferBit);

        DrawingInfo drawingInfo = GenerateGeometry();
        UpdateBuffers(drawingInfo);

        _gl.UseProgram(_program);
        _gl.BindVertexArray(_vao);
        _gl.DrawElements(PrimitiveType.Triangles, (uint)drawingInfo.Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
    }

    private static DrawingInfo GenerateGeometry()
    {
        List<float> vertices = [];
        List<uint> indices = [];

        float windowWidth = _window!.Size.X;
        float windowHeight = _window!.Size.Y;
        float count = _numbers.Length * 2 + 1;
        float widthPerRect = windowWidth / count;

        for (int i = 0; i < _numbers.Length; i++)
        {
            float pixelLeft = widthPerRect + (i * widthPerRect * 2);
            float pixelRight = pixelLeft + widthPerRect;

            float xLeft = (pixelLeft / windowWidth) * 2 - 1;
            float xRight = (pixelRight / windowWidth) * 2 - 1;
            float yTop = (_numbers[i] / windowHeight) * 2 - 1;
            float yBottom = -1.0f;

            uint offset = (uint)(i * 4);
            vertices.AddRange([xRight, yTop, 0, xRight, yBottom, 0, xLeft, yBottom, 0, xLeft, yTop, 0]);
            indices.AddRange([offset, offset + 1, offset + 3, offset + 1, offset + 2, offset + 3]);
        }

        return new DrawingInfo([.. vertices], [.. indices]);
    }

    private static unsafe void UpdateBuffers(DrawingInfo drawingInfo)
    {
        _gl!.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        fixed (float* vPtr = drawingInfo.Vertices)
        {
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(drawingInfo.Vertices.Length * sizeof(float)), vPtr, BufferUsageARB.StreamDraw);
        }

        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
        fixed (uint* iPtr = drawingInfo.Indices)
        {
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(drawingInfo.Indices.Length * sizeof(uint)), iPtr, BufferUsageARB.StreamDraw);
        }
    }

    private static uint CreateProgram(string vCode, string fCode)
    {
        uint vShader = CompileShader(ShaderType.VertexShader, vCode);
        uint fShader = CompileShader(ShaderType.FragmentShader, fCode);

        uint prog = _gl!.CreateProgram();
        _gl.AttachShader(prog, vShader);
        _gl.AttachShader(prog, fShader);
        _gl.LinkProgram(prog);

        _gl.GetProgram(prog, ProgramPropertyARB.LinkStatus, out int status);
        if (status != (int)GLEnum.True)
        {
            throw new Exception($"Program Link Error: {_gl.GetProgramInfoLog(prog)}");
        }

        _gl.DeleteShader(vShader);
        _gl.DeleteShader(fShader);
        return prog;
    }

    private static uint CompileShader(ShaderType type, string code)
    {
        uint shader = _gl!.CreateShader(type);
        _gl.ShaderSource(shader, code);
        _gl.CompileShader(shader);

        _gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
        if (status != (int)GLEnum.True)
        {
            throw new Exception($"{type} Error: {_gl.GetShaderInfoLog(shader)}");
        }

        return shader;
    }

    private static void SetupInput()
    {
        using IInputContext input = _window!.CreateInput();
        foreach (IKeyboard keyboard in input.Keyboards)
        {
            keyboard.KeyDown += (k, key, _) => 
            { 
                if (key == Key.Escape) 
                { 
                    _window!.Close(); 
                } 
            };
        }
    }

    private static void OnUpdate(double deltaTime) 
    {
        if (_activeSort?.MoveNext() == false)
        {
            // Sort is finished!
            _activeSort = null;
        }
    }
}