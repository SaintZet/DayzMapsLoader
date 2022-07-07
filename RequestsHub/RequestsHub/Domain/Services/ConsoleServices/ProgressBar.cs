namespace RequestsHub.Domain.Services.ConsoleServices
{
    internal class ProgressBar
    {
        private static int fullCount;
        private static int currentCount;

        public ProgressBar(int count)
        {
            fullCount = count;
            currentCount = 0;
        }

        public static ProgressBar operator ++(ProgressBar progressBar)
        {
            currentCount += 1;
            Console.WriteLine($"Process: {fullCount}/{currentCount}");
            //Console.SetCursorPosition(0, Console.CursorTop - 1);
            //ClearCurrentConsoleLine();

            return progressBar;
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
