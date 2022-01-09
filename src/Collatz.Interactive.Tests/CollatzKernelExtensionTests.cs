using Microsoft.DotNet.Interactive;
using Microsoft.DotNet.Interactive.Commands;
using Microsoft.DotNet.Interactive.CSharp;
using Microsoft.DotNet.Interactive.Events;
using NUnit.Framework;
using System.Collections;
using System.Collections.Immutable;
using XPlot.Plotly;

namespace Collatz.Interactive.Tests;

// Lots of the code here was lifted from:
// https://github.com/dotnet/interactive/blob/main/src/Microsoft.DotNet.Interactive.Tests/Utility/TestUtility.cs
public static class CollatzKernelExtensionTests
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static KernelWrapper wrapper;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	[SetUp]
	public static async Task SetupAsync() =>
		CollatzKernelExtensionTests.wrapper = await KernelWrapper.CreateAsync().ConfigureAwait(false);

	[Test]
	public static async Task SubmitCommandWithSOptionAsync()
	{
		using var events = CollatzKernelExtensionTests.wrapper.Kernel.KernelEvents.ToSubscribedList();
		await CollatzKernelExtensionTests.wrapper.Kernel.SubmitCodeAsync("#!collatz -s 5").ConfigureAwait(false);

		Assert.Multiple(() =>
		{
			Assert.That(events, Has.Count.EqualTo(2));
			var displayedValueCommand = (DisplayedValueProduced)events[0];
			var succeededCommand = (CommandSucceeded)events[1];
			Assert.That(displayedValueCommand.Value, Is.TypeOf<PlotlyChart>());
			Assert.That(succeededCommand.Command, Is.TypeOf<SubmitCode>());
		});
	}

	[Test]
	public static async Task SubmitCommandWithStartOptionAsync()
	{
		using var events = CollatzKernelExtensionTests.wrapper.Kernel.KernelEvents.ToSubscribedList();
		await CollatzKernelExtensionTests.wrapper.Kernel.SubmitCodeAsync("#!collatz --start 5").ConfigureAwait(false);

		Assert.Multiple(() =>
		{
			Assert.That(events, Has.Count.EqualTo(2));
			var displayedValueCommand = (DisplayedValueProduced)events[0];
			var succeededCommand = (CommandSucceeded)events[1];
			Assert.That(displayedValueCommand.Value, Is.TypeOf<PlotlyChart>());
			Assert.That(succeededCommand.Command, Is.TypeOf<SubmitCode>());
		});
	}

	[Test]
	public static async Task SubmitCommandWithUnknownOptionAsync()
	{
		using var events = CollatzKernelExtensionTests.wrapper.Kernel.KernelEvents.ToSubscribedList();
		await CollatzKernelExtensionTests.wrapper.Kernel.SubmitCodeAsync("#!collatz --unknown 5").ConfigureAwait(false);

		Assert.Multiple(() =>
		{
			Assert.That(events, Has.Count.EqualTo(3));
			Assert.That(events[0], Is.TypeOf<CodeSubmissionReceived>());
			Assert.That(events[1], Is.TypeOf<CompleteCodeSubmissionReceived>());
			Assert.That(events[2], Is.TypeOf<CommandSucceeded>());
		});
	}

	[TearDown]
	public static void TearDown() =>
		CollatzKernelExtensionTests.wrapper.Dispose();
}

public sealed class KernelWrapper
	: IDisposable
{
	public static async Task<KernelWrapper> CreateAsync()
	{
		var wrapper = new KernelWrapper();
		await wrapper.InitializeAsync().ConfigureAwait(false);
		return wrapper;
	}

	public KernelWrapper()
	{
		this.Kernel = new CompositeKernel
		{
#pragma warning disable CA2000 // Dispose objects before losing scope
			new CSharpKernel()
#pragma warning restore CA2000 // Dispose objects before losing scope
		};
		this.KernelEvents = this.Kernel.KernelEvents.ToSubscribedList();
	}

	public async Task InitializeAsync() =>
		await new CollatzKernelExtension().OnLoadAsync(this.Kernel).ConfigureAwait(false);

	public void Dispose()
	{
		this.Kernel.Dispose();
		this.KernelEvents.Dispose();
	}

	public Kernel Kernel { get; }
	public SubscribedList<KernelEvent> KernelEvents { get; }
}

public sealed class SubscribedList<T>
	: IReadOnlyList<T>, IDisposable
{
	private ImmutableArray<T> list = ImmutableArray<T>.Empty;
	private readonly IDisposable subscription;

	public SubscribedList(IObservable<T> source) =>
		this.subscription = source.Subscribe(x => { this.list = this.list.Add(x); });

	public void Dispose() => this.subscription.Dispose();

	public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)this.list).GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

	public int Count => this.list.Length;

	public T this[int index] => this.list[index];
}

public static class ObservableExtensions
{
	public static SubscribedList<T> ToSubscribedList<T>(this IObservable<T> self) =>
		new(self);
}