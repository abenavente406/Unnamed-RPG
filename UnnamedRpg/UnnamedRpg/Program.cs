using System;

namespace UnnamedRpg
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Create a new instance of our game and run it
            using (TestGame game = new TestGame())
            {
                game.Run();
            }
        }
    }
#endif
}

