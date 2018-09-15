<Query Kind="Program" />

// Ultimately all delegates derive from System.Delegate
// but we can't define them directly

// Illegal definition
// Cannot derive from special class 'Delegate'
// Only the compiler can do this through the delegate keyword

//public class MyDelegate : Delegate {}

// In reality all delegates derive from MulticastDelegate
// We can't define MulticastDelegate directly, either

// Also illegal
// Cannot derive from special class 'MulticastDelegate'
// Only the compiler can do this through the delegate keyword

//public class MyDelegate : MulticastDelegate {}

// We've already seen that we don't typically have to define
// our own delegate types anymore thanks to the generic
// delegate types
// Action<...>
// Func<..., U>
// Predicate<T>

void Main()
{
	// We can initialize them like variables
	Func<int, int, int> add =
		(a, b) =>
			a + b;
			
	Func<int, int, int> subtract =
		(a, b) =>
			a - b;

	// We can invoke them like functions
	//add(41, 1).Dump("Add result");
	//subtract(43, 1).Dump("Subtract result");

	// ...and pass them to other functions, too!
	//Enumerable.Range(1, 10).Where(x => x % 2 == 0).Dump("Even number filter");

	// We can even combine them to execute together...kind of
	// (note: the combined delegates are invoked in order)
	//(add + subtract)(41, 1).Dump("Combined result (only the last is returned)");
		
	// Or we can invoke them via the underlying delegate type:
	//add.Invoke(1, 2).Dump("Invoke");
	
	// But how? 
	var d = (MulticastDelegate)add;

	// There is no Invoke method on Delegate or MulticastDelegate
	// That's part of the code generated for the custom delegate types
	// This is illegal
	//d.Invoke(1, 2).Dump();

	// ...but this isn't
	//d.DynamicInvoke(1, 2).Dump("DynamicInvoke result");

	// Each delegate type has an invocation list which represents all
	// of the functions represented by the delegate instance
	add.GetInvocationList().Dump("Func<int, int, int> Invocation List");
	d.GetInvocationList().Dump("MulticastDelegate Invocation List");
	
	// Knowing how MulticastDelegate works is key to getting the most from them
}
