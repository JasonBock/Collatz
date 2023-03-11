using System.Collections.Immutable;
using System.Numerics;

namespace Collatz;

public static class CollatzSequenceGenerator
{
	public static ImmutableArray<BigInteger> Generate(BigInteger start)
	{
		if (start <= BigInteger.One)
		{
			throw new ArgumentException("Must provide a starting value greater than one.", nameof(start));
		}

		var builder = ImmutableArray.CreateBuilder<BigInteger>();
		builder.Add(start);

		while (start > 1)
		{
			start = start % 2 == 0 ?
				start / 2 : ((3 * start) + 1) / 2;
			builder.Add(start);
		}

		return builder.ToImmutable();
	}

#if NET7_0_OR_GREATER
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

		while (start > T.One)
		{
			start = start % two == T.Zero ?
				start / two : ((three * start) + T.One) / two;
			builder.Add(start);
		}

		return builder.ToImmutable();
	}
#endif

	public static IEnumerable<BigInteger> GenerateStream(BigInteger start)
	{
		if (start <= BigInteger.One)
		{
			throw new ArgumentException("Must provide a starting value greater than one.", nameof(start));
		}

		yield return start;

		while (start > 1)
		{
			start = start % 2 == 0 ?
				start / 2 : ((3 * start) + 1) / 2;
			yield return start;
		}
	}

#if NET7_0_OR_GREATER
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

		while (start > T.One)
		{
			start = start % two == T.Zero ?
				start / two : ((three * start) + T.One) / two;
			yield return start;
		}
	}
#endif
}