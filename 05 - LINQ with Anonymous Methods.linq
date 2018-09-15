<Query Kind="Statements" />

"The quick brown fox jumps over the lazy dog"
    .Split(' ')
    .Select(delegate (string s) { return s.ToUpper(); })
    .OrderBy(delegate (string s) { return s; })
    .Where(delegate (string s) { return s.Length >= 4; })
    .Dump();
