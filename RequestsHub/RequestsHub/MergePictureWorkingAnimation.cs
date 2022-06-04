namespace RequestsHub
{
    internal interface IMergePictureWorkingAnimation
    {
        void Spin(string DirectoryName);
    }

    internal class MergePictureWorkingAnimation : IMergePictureWorkingAnimation
    {
        private const int LastFileCount = 128;
        private int counter;

        public MergePictureWorkingAnimation()
        {
            counter = 0;
        }

        public void Spin(string DirectoryName)
        {
            counter++;

            if (counter == LastFileCount)
            {
                Console.WriteLine($"{DirectoryName} - done!");
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