# Collatz

This is a library that generates a sequence of integer values, based on the [Collatz Conjecture](https://en.wikipedia.org/wiki/Collatz_conjecture). I'm primarly creating this package because I use this algorithm to demonstrate a number of development concepts in presentations, so having it as a NuGet package will make it easier to reuse it.

## Getting started

Simply reference the `Collatz` NuGet package - that's it.

### Prerequisites

This package works for both .NET 6 and .NET 7. If you're using .NET 7, the `Generate()` and `GenerateStream()` values use the `IBinaryInteger<T>` interface to allow you to use any type that implements that interface.

## Usage

You can either get the entire sequence at once:

```csharp
var sequence = CollatzSequenceGenerator.Generate(5);

// sequence will be [ 5, 8, 4, 2, 1 ]
```

Or get it as an enumerable:

```csharp
foreach(var value in CollatzSequenceGenerator.GenerateStream(5))
{
  Console.WriteLine(value);
}

/*
The following sequence will print to the console: 

5
8
4
2
1
*/
```

## Additional documentation

* [Changelog](https://github.com/JasonBock/Collatz/blob/main/changelog.md)
* [Collatz conjecture on Wikipedia](https://en.wikipedia.org/wiki/Collatz_conjecture)
* [The Ultimate Challenge: The 3x+1 Problem](https://www.maa.org/press/maa-reviews/the-ultimate-challenge-the-3x1-problem)

## .NET Polygot Version in VSCode

The .ipynb files have been confirmed to work with Polyglot Notebooks v1.0.4102020. Later versions have compatiability issues with XPloty.

## Feedback

If you run into any issues, please add them [here](https://github.com/JasonBock/Collatz/issues).