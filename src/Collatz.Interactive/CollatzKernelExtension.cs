using Microsoft.DotNet.Interactive;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Globalization;
using System.Numerics;
using XPlot.Plotly;

namespace Collatz.Interactive
{
	public sealed class CollatzKernelExtension
		: IKernelExtension
	{
		public Task OnLoadAsync(Kernel kernel)
		{
			if (kernel is null)
			{
				throw new ArgumentNullException(nameof(kernel));
			}

			var command = new Command("#!collatz", "Displays a chart of the Collatz sequence.")
			{
				new Option<BigInteger>(new[] {"-s", "--start"}, 
					(ArgumentResult result) =>
					{
						return BigInteger.Parse(result.Tokens[0].Value, CultureInfo.CurrentCulture);
					},
					description: "The starting value of the sequence")
			};

			command.Handler = CommandHandler.Create(
				(BigInteger start, KernelInvocationContext invocationContext) =>
				{
					var position = 0;
					var sequence = CollatzSequenceGenerator.Generate(start)
						.Select(_ => Tuple.Create(position++, (long)_));

					var chart = Chart.Line(sequence);
					chart.Display();
				});

			kernel.AddDirective(command);
			return Task.CompletedTask;
		}
	}
}