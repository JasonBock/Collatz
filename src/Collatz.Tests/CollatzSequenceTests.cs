using NUnit.Framework;
using System;
using System.Collections.Immutable;
using System.Numerics;

namespace Collatz.Tests;

public static class CollatzSequenceTests
{
	[Test]
	public static void Generate()
	{
		var start = new BigInteger(5);
		var sequence = CollatzSequenceGenerator.Generate(start);

		Assert.That(sequence, Is.EqualTo(ImmutableArray.Create<BigInteger>(5, 8, 4, 2, 1)));
	}

	[Test]
	public static void GenerateWithIncorrectStartValue() =>
		Assert.That(() => CollatzSequenceGenerator.Generate(BigInteger.One),
			Throws.TypeOf<ArgumentException>());

	[Test]
	public static void GenerateStream()
	{
		var start = new BigInteger(5);
		var sequence = CollatzSequenceGenerator.GenerateStream(start).ToImmutableArray();

		Assert.That(sequence, Is.EqualTo(ImmutableArray.Create<BigInteger>(5, 8, 4, 2, 1)));
	}

	[Test]
	public static void GenerateStreamWithIncorrectStartValue() =>
		Assert.That(() => CollatzSequenceGenerator.GenerateStream(BigInteger.One).ToImmutableArray(),
			Throws.TypeOf<ArgumentException>());
}