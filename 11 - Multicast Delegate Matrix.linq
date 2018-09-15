<Query Kind="Program" />

public static class Extensions
{
    public static TResult[,] ToMatrix<TSource, TResult>(this IEnumerable<IEnumerable<TSource>> source, Func<TSource, TResult> selector)
    {
        var rows = source.Count();
        var cols = source.ElementAt(0).Count();

        var matrix = new TResult[rows, cols];

        for (var r = 0; r < rows; r++)
        {
            var row = source.ElementAt(r);
            for (var c = 0; c < cols; c++)
            {
                matrix[r, c] = c.Map(row.ElementAt).Map(selector);
            }
        }
        
        return matrix;
    }
}

void Main()
{
    var range = Enumerable.Range(1, 10);

    var multipliers =
        range
            .Select<int, Func<int, int>>(
                i =>
                    x =>
                    {
                        var result = x * i;
                        //$"{x} * {i} = {result}".Dump();
                        return result;
                    })
            .Map(DelegateHelper.Combine)
            //.Tee(m => m.Dump("Multicast Delegate").GetInvocationList().Dump("Invocation List"))
            //.Tee(d => d.Invoke(1).Dump("Invoke (Only last result is returned)"))
			//.Tee(d => d.InvokeAll(1).Dump("Invoke All (All results are returned!)"))
			;

    range
        .Select(i => multipliers.InvokeAll(i).ToArray())
        .ToArray()
        .ToMatrix(i => i)
        .Dump();
}