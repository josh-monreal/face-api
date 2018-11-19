using FA.CompositionRoot;

namespace FA.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var unity = new DependencyInjection<Initializer>();
            var program = unity.Resolve();
            program.Start();
        }
    }
}