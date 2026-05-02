namespace VisualSorting;

public enum SortingType
{
    // --- Standard Comparison Sorts ---
    Bubble = 0,             // aka Sinking
    Selection = 1,
    Insertion = 2,
    Merge = 3,
    Quick = 4,             // aka Partition-Exchange
    Shell = 5,
    CocktailShaker = 6,    // aka Bidirectional Bubble sort / Cocktail sort / Shaker sort / Ripple sort / Shuffle sort / Shuttle sort 
    Gnome = 7,             // aka Stupid
    OddEven = 8,           // aka Brick sort / Parity sort / Odd-Even Transposition   
    Comb = 9,
    Cycle = 10,

    // --- Advanced Comparison (Hybrid/Efficient) ---
    Heap = 11,
    Intro = 12,            // aka Introspective Sort
    InPlaceMerge = 13,
    Tournament = 14,
    Tree = 15,
    Block = 16,            // aka Block Merge
    Smooth = 17,
    Tim = 18,
    Patience = 19,
    Cube = 20,
    Flux = 21,
    Crum = 22,
    Library = 23,
    Strand = 24,
    MergeInsertion = 25,    // aka Ford-Johnson

    // --- Distribution / Non-Comparison Sorts ---
    Pigeonhole = 26,
    BucketUniform = 27,
    BucketInteger = 28,
    Counting = 29,
    LSDRadix = 30,
    MSDRadix = 31,
    MSDRadixInPlace = 32,
    Spread = 33,
    Burst = 34,
    Flash = 35,
    Sample = 36,

    // --- Concurrent / Hardware Sorts ---
    Bitonic = 37,
    SortingNetwork = 38,
    Gravity = 39,           // a.k.a. Bead
    Spaghetti = 40,         // a.k.a. Poll

    // --- The "Esoteric" & Meme Sorts ---
    Bogo = 41,
    Stalin = 42,
    Sleep = 43,
    Stooge = 44,
    Slow = 45,
    Thanos = 46,
    Miracle = 47,
    QuantumBogo = 48
}