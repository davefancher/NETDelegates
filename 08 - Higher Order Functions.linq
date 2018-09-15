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

IEnumerable<Name> FilterByFirstName(IEnumerable<Name> names, string firstName)
{
    var filtered = new List<Name>();

    foreach (var name in names)
    {
        if (name.First == firstName)
        {
            filtered.Add(name);
        }
    }
    
    return filtered;
}

IEnumerable<Name> FilterByLastName(IEnumerable<Name> names, string lastName)
{
    var filtered = new List<Name>();

    foreach (var name in names)
    {
        if (name.Last == lastName)
        {
            filtered.Add(name);
        }
    }
    
    return filtered;
}

IEnumerable<T> Filter<T>(IEnumerable<T> seq, Func<T, bool> filter)
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
}

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
        
    FilterByFirstName(names, "Peter").Dump();
    FilterByLastName(names, "Baker").Dump();
    
    Filter(names, n => n.First == "Peter").Dump();
    Filter(names, n => n.Last == "Baker").Dump();
    Filter(names, n => n.First == "Peter" && n.Last == "Capaldi").Dump();
}
