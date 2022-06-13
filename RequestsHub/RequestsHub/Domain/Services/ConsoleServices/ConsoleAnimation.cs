namespace RequestsHub.Domain.Services.ConsoleServices
{
    internal class ConsoleAnimation
    {
        private const int LastFileCount = 128;
        private int counter;

        public void Spin(string DirectoryName)
        {
            counter++;

            if (counter == LastFileCount)
            {
                System.Console.WriteLine($"{DirectoryName} - done!");
                return;
            }

            switch (counter % 4)
            {
                case 0: Console.Write("/"); break;
                case 1: Console.Write("-"); break;
                case 2: Console.Write("\\"); break;
                case 3: Console.Write("|"); break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}