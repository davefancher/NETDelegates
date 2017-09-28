<Query Kind="Program" />

[DebuggerNonUserCode]
public static class FunctionalExtensions
{
    public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> map) =>
        map(@this);

    public static T Tee<T>(this T @this, Action<T> act)
    {
        act(@this);
        return @this;
    }

    public static void Apply<T>(this T @this, Action<T> act) => act(@this);

    public static TResult ConvertTo<TResult>(this object @this)
    {
        var convertible = @this as IConvertible;

        return
            convertible != null
                ? (TResult)convertible.ToType(typeof(TResult), null)
                : (TResult)@this;
    }

    public static T SetProperty<T, U>(
        this T @this,
        Expression<Func<T, U>> propertyExpression,
        U value)
    {
        propertyExpression
            .Body
            .ConvertTo<MemberExpression>()
            .Member
            .Name
            .Map(@this.GetType().GetProperty)
            .SetValue(@this, value);

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
    private static Delegate __CombineMulticastDelegate(IEnumerable<MulticastDelegate> delegates) =>
        delegates
            .ToArray()
            .Map(Delegate.Combine);

    public static Action<T> Combine<T>(IEnumerable<Action<T>> delegates) =>
        (Action<T>)__CombineMulticastDelegate(delegates);

    public static Func<T, TResult> Combine<T, TResult>(IEnumerable<Func<T, TResult>> delegates) =>
        (Func<T, TResult>)__CombineMulticastDelegate(delegates);
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
