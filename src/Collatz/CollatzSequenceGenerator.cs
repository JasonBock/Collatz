using System.Collections.Immutable;
using System.Numerics;

namespace Collatz;

/// <summary>
/// Contains methods to generate Collatz sequences.
/// </summary>
public static class CollatzSequenceGenerator
{
	/// <summary>
	/// Generates an array of <typeparamref name="T"/> values based on the starting value.
	/// </summary>
	/// <typeparam name="T">A value that derives from <see cref="IBinaryInteger{T}"/>.</typeparam>
	/// <param name="start">The starting value.</param>
	/// <returns>The Collatz sequence based on the starting value.</returns>
	/// <exception cref="ArgumentException">Thrown if <paramref name="start"/> is less than or equal to 1.</exception>
	public static ImmutableArray<T> Generate<T>(T start)
		where T : IBinaryInteger<T>
	{
		if (start <= T.One)
		{
			throw new ArgumentException("Must provide a starting value greater than one.", nameof(start));
		}

		var two = T.CreateChecked(2);
		var three = T.CreateChecked(3);

		var builder = ImmutableArray.CreateBuilder<T>();
		builder.Add(start);

		checked
		{
			while (start > T.One)
			{
				start = start % two == T.Zero ?
					start / two : ((three * start) + T.One) / two;
				builder.Add(start);
			}
		}

		return builder.ToImmutable();
	}

	/// <summary>
	/// Generates a stream of <typeparamref name="T"/> values based on the starting value.
	/// </summary>
	/// <typeparam name="T">A value that derives from <see cref="IBinaryInteger{T}"/>.</typeparam>
	/// <param name="start">The starting value.</param>
	/// <returns>The Collatz sequence as a stream based on the starting value.</returns>
	/// <exception cref="ArgumentException">Thrown if <paramref name="start"/> is less than or equal to 1.</exception>
	public static IEnumerable<T> GenerateStream<T>(T start)
		where T : IBinaryInteger<T>
	{
		if (start <= T.One)
		{
			throw new ArgumentException("Must provide a starting value greater than one.", nameof(start));
		}

		yield return start;

		var two = T.CreateChecked(2);
		var three = T.CreateChecked(3);

		checked
		{
			while (start > T.One)
			{
				start = start % two == T.Zero ?
					start / two : ((three * start) + T.One) / two;
				yield return start;
			}
		}
	}
}