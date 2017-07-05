using System;

namespace TrieRope
{
    class Launcher
    {
        static void Main(string[] args)
        {
            StringEditor stringEditor = new StringEditor();

            stringEditor.Login("pesho");
            stringEditor.Undo("pesho");


            stringEditor.Prepend("pesho", "stringexample");
            stringEditor.Delete("pesho", 3, 6);
            stringEditor.Undo("pesho");

            stringEditor.Substring("pesho", 0, 3);
            stringEditor.Undo("pesho");


            Console.WriteLine(stringEditor.Print("pesho"));
        }
    }
}
