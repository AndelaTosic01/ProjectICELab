using ShellProgressBar;

namespace MIntermediateRepresentation
{
    public class Program
    {
        private const int Major = 1;
        private const int Minor = 1;
        private const int Build = 1;
        private const int Revision = 0;

        public static void Main(string[] args)
        {
            Console.WriteLine("-----------------MIntermediateRepresentation v{0}.{1}.{2}.{3}-----------------", Major, Minor, Build, Revision);

            //-----------------TODO-----------------
            //  1) Define more DataType
            //      - Real, String, Boolean
            //  2) Change IntegerType 
            //      - Add compatibility with long
            //  3) Implement Constant
            //  4) Define more expression 
            //      - Arithmetic Operations (e.g. Sub, Mul, Div, Max, Min) 
            //          |-> Sum of two string defined as concatenation 
            //      - Logic Operation (and, or, not, xor)
            //  *) Remember always to test and comment your code.
            //-----------------TODO-----------------

            const int totalTicks = 10;
            var options = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Yellow,
                BackgroundColor = ConsoleColor.DarkYellow,
                ProgressCharacter = '─'
            };
            var childOptions = new ProgressBarOptions
            {
                ForegroundColor = ConsoleColor.Green,
                BackgroundColor = ConsoleColor.DarkGreen,
                ProgressCharacter = '─',
                //CollapseWhenFinished = true
            };
            using (var pbar = new ProgressBar(totalTicks, "main progressbar", options))
            {
                
                TickToCompletion(pbar, totalTicks, sleep: 10, childAction: i =>
                {
                    using (var child = pbar.Spawn(totalTicks, "child actions", childOptions))
                    {
                        TickToCompletion(child, totalTicks, sleep: 100);
                    }
                });
            }

            Thread.Sleep(100);
        }

        public static void TickToCompletion(IProgressBar pbar, int ticks, int sleep = 1750, Action<int> childAction = null)
        {
            var initialMessage = pbar.Message;
            for (var i = 0; i < ticks; i++)
            {
                pbar.Message = $"Start {i + 1} of {ticks} {Console.CursorTop}/{Console.WindowHeight}: {initialMessage}";
                childAction?.Invoke(i);
                Thread.Sleep(sleep);
                pbar.Tick($"End {i + 1} of {ticks} {Console.CursorTop}/{Console.WindowHeight}: {initialMessage}");
            }
        }
    }
}
