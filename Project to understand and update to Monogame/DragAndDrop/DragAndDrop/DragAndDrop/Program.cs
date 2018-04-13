using System;

namespace DragAndDrop
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (DragAndDropTutorial game = new DragAndDropTutorial())
            {
                game.Run();
            }
        }
    }
#endif
}

