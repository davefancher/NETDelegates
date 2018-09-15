<Query Kind="Statements" />

"The quick brown fox jumps over the lazy dog"
    .Split(' ')
    .Select(s => s.ToUpper())
    .OrderBy(s => s)
    .Where(s => s.Length >= 4)
    .Dump();