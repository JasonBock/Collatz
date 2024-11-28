using NUnit.Framework;
using System.Collections.Immutable;

namespace Collatz.Tests;

internal static class CollatzSequenceGeneratorTests
{
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
}