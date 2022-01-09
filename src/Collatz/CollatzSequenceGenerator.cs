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
}