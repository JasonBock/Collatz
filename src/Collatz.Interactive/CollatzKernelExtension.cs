using Microsoft.DotNet.Interactive;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;
using System.Numerics;
using XPlot.Plotly;

namespace Collatz.Interactive;

public sealed class CollatzKernelExtension
	: IKernelExtension
{
	public Task OnLoadAsync(Kernel kernel)
	{
		if (kernel is null)
		{
			throw new ArgumentNullException(nameof(kernel));
		}

		var startOption = new Option<BigInteger>(new[] { "-s", "--start" },
			(ArgumentResult result) =>
			{
				return BigInteger.Parse(result.Tokens[0].Value, CultureInfo.CurrentCulture);
			},
			description: "The starting value of the sequence");

		var command = new Command("#!collatz", "Displays a chart of the Collatz sequence.")
			{
				startOption
			};

		command.SetHandler(
			(BigInteger start) =>
			{
				var position = 0;
				var sequence = CollatzSequenceGenerator.Generate<BigInteger>(start)
					.Select(_ => Tuple.Create(position++, (long)_));

				var chart = Chart.Line(sequence);
				chart.Display();
			}, startOption);

		kernel.AddDirective(command);
		return Task.CompletedTask;
	}
}