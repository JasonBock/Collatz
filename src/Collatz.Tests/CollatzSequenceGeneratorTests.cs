using NUnit.Framework;
using System.Collections.Immutable;
using System.Numerics;

namespace Collatz.Tests;

public static class CollatzSequenceGeneratorTests
{
   [Test]
#pragma warning disable CS0618 // Type or member is obsolete
   public static void Generate() => 
		Assert.That(CollatzSequenceGenerator.Generate(new BigInteger(5)),
		   Is.EqualTo(ImmutableArray.Create<BigInteger>(5, 8, 4, 2, 1)));

   [Test]
	public static void GenerateWithIncorrectStartValue() =>
		Assert.That(() => CollatzSequenceGenerator.Generate(BigInteger.One),
			Throws.TypeOf<ArgumentException>());

   [Test]
   public static void GenerateStream() => 
		Assert.That(CollatzSequenceGenerator.GenerateStream(new BigInteger(5)).ToImmutableArray(),
		   Is.EqualTo(ImmutableArray.Create<BigInteger>(5, 8, 4, 2, 1)));

   [Test]
	public static void GenerateStreamWithIncorrectStartValue() =>
		Assert.That(() => CollatzSequenceGenerator.GenerateStream(BigInteger.One).ToImmutableArray(),
			Throws.TypeOf<ArgumentException>());
#pragma warning restore CS0618 // Type or member is obsolete

#if NET7_0_OR_GREATER
	[Test]
   public static void GenerateForGeneric() => 
		Assert.That(CollatzSequenceGenerator.Generate(5),
		   Is.EqualTo(ImmutableArray.Create(5, 8, 4, 2, 1)));

   [Test]
	public static void GenerateWithIncorrectStartValueForGeneric() =>
		Assert.That(() => CollatzSequenceGenerator.Generate(1),
			Throws.TypeOf<ArgumentException>());

	[Test]
	public static void GenerateWithOverflowForGeneric() =>
		Assert.That(() => CollatzSequenceGenerator.Generate(int.MaxValue),
			Throws.TypeOf<OverflowException>());

   [Test]
   public static void GenerateStreamForGeneric() => 
		Assert.That(CollatzSequenceGenerator.GenerateStream(5).ToImmutableArray(),
		   Is.EqualTo(ImmutableArray.Create(5, 8, 4, 2, 1)));

   [Test]
	public static void GenerateStreamWithIncorrectStartValueForGeneric() =>
		Assert.That(() => CollatzSequenceGenerator.GenerateStream(1).ToImmutableArray(),
			Throws.TypeOf<ArgumentException>());

	[Test]
	public static void GenerateStreamWithOverflowForGeneric() =>
		Assert.That(() => CollatzSequenceGenerator.GenerateStream(int.MaxValue).ToImmutableArray(),
			Throws.TypeOf<OverflowException>());
#endif
}