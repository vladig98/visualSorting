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
    private static readonly int[] _numbers = new int[100];
    private const int BaseWidth = 1800;
    private const int BaseHeight = 600;

    private const string VertexShaderSource = @"#version 330 core
        layout (location = 0) in vec3 aPosition;
        void main() { gl_Position = vec4(aPosition, 1.0); }";

    private const string FragmentShaderSource = @"#version 330 core
        out vec4 out_color;
        void main() { out_color = vec4(1.0, 0.5, 0.2, 1.0); }";

    public static void Initialize()
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

        for (int i = 0; i < _numbers.Length; i++)
        {
            _numbers[i] = Random.Shared.Next(10, BaseHeight);
        }

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
    }
}