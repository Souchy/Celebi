using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.impl.stats;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace EeveeUnitTests
{
    public class UnitTest1
    {

        private readonly ITestOutputHelper output;
        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async Task TestCSharpScriptPerformance()
        {
            IStats stats = new Stats();
            stats.set(StatType.Life, new StatResource());
            stats.get<IStatResource>(StatType.Life).current = 7;

            var options = ScriptOptions.Default;
            options = options.AddReferences(typeof(IStats).Assembly);
            options = options.AddImports(
                "souchy.celebi.eevee.enums",
                "souchy.celebi.eevee.face.stats",
                "souchy.celebi.eevee.impl.stats"
            );
            var str = "get<StatResource>(StatType.Life).current";
            var script = CSharpScript.Create<int>(str, options, globalsType: typeof(IStats));


            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 1000; i++)
            {
                ScriptState<int> result = await script.RunAsync(stats);
                Assert.Equal(7, result.ReturnValue);
            }
            watch.Stop();

            TimeSpan timeSpan = watch.Elapsed;
            output.WriteLine(string.Format("Time: {0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes,
                timeSpan.Seconds, timeSpan.Milliseconds));

        }
    }
}