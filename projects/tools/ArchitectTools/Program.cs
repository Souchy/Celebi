namespace ArchitectTools
{
    internal class Program : App
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            apps[0].run();
        }


        private static App[] apps = { new Program(), new HierarchyTracer() };
        public string Keyword => "a";

        private Program()
        {

        }

        public void run()
        {
            Console.WriteLine("Welcome. Choose an app to run: ");
            foreach(var app in apps) {
                Console.WriteLine(app.Keyword + ": " + app.GetType().Name);
            }
            string? input = Console.ReadLine();
            foreach(var app in apps) { 
                if(input != null && input == app.Keyword) {
                    app.run();
                    //return;
                }
            }
            run();
        }

    }
}