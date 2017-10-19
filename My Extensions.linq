<Query Kind="Program" />

[DebuggerNonUserCode]
public static class FunctionalExtensions
{
    public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> map) => map(@this);

    public static T Tee<T>(this T @this, Action<T> act)
    {
        act(@this);
        return @this;
    }
}

[DebuggerNonUserCode]
public static class Operators
{
    public static T ID<T>(T value) => value;
}

[DebuggerNonUserCode]
public static class DelegateHelper
{
    private static Delegate CombineDelegates(IEnumerable<MulticastDelegate> delegates) =>
        delegates
            .Cast<Delegate>()
            .Aggregate(Delegate.Combine);

    public static Action<T> Combine<T>(IEnumerable<Action<T>> delegates) =>
        delegates
            .Map(CombineDelegates)
            .ConvertTo<Action<T>>();

    public static Func<T, TResult> Combine<T, TResult>(IEnumerable<Func<T, TResult>> delegates) =>
        delegates
            .Map(CombineDelegates)
            .ConvertTo<Func<T, TResult>>();
}

[DebuggerNonUserCode]
public static class DelegateExtensions
{
    public static IEnumerable<TResult> InvokeAll<T, TResult>(this Func<T, TResult> @this, T input) =>
        @this
            .GetInvocationList()
            .Cast<Func<T, TResult>>()
            .Select(d => d.Invoke(input));

    public static T InvokeChain<T>(this Func<T, T> @this, T input) =>
        @this
            .GetInvocationList()
            .Cast<Func<T, T>>()
            .Aggregate(input, (a, d) => d(a));
}

