<Query Kind="Program" />

class Name
{
    public Name(string first, string last)
    {
        First = first;
        Last = last;
    }

    public string First { get; }
    public string Last { get; }
}

Func<Func<T, bool>, IEnumerable<T>> Filter<T>(IEnumerable<T> seq) =>
    filter =>
    {
        var filtered = new List<T>();

        foreach (var item in seq)
        {
            if (filter(item))
            {
                filtered.Add(item);
            }
        }

        return filtered;
    };

void Main()
{
    var names =
        new[]
        {
            new Name("William", "Hartnell"),
            new Name("Patrick", "Troughton"),
            new Name("Jon", "Pertwee"),
            new Name("Tom", "Baker"),
            new Name("Peter", "Davison"),
            new Name("Colin", "Baker"),
            new Name("Sylvester", "McCoy"),
            new Name("Paul", "McGann"),
            new Name("Christopher", "Eccleston"),
            new Name("David", "Tennant"),
            new Name("Matt", "Smith"),
            new Name("Peter", "Capaldi"),
            new Name("Jodie", "Whittaker")
        };

    var filterNames = FilterPartial(names);

    filterNames(n => n.First == "Peter" && n.Last == "Capaldi").Dump();
}
