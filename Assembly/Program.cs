using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace AssemblyProject
{
    class Program
    {   static void Main(string[] args)
         {  
            GC.Collect();
            GC.WaitForPendingFinalizers();

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine($"Name of assemblies: {asm.GetName().Name}");
            }
            Square(5);
        }
  
      
    static void Square(int number)
        {
            var context = new AssemblyLoadContext(name: "Square", isCollectible: true);

            context.Unloading += Context_Unloading;

            var assemplyPath = Path.Combine(@"C:\Users\assai\source\repos\ExampleAssemlyLoad\ExampleAssemlyLoad\bin\Debug\netcoreapp3.1", "ExampleAssemlyLoad.dll");

            Assembly assembly = context.LoadFromAssemblyPath(assemplyPath);

            var type = assembly.GetType("ExampleAssemlyLoad.Program");

            if(type != null)
            {
                var squareMethod = type.GetMethod("Square", BindingFlags.Static | BindingFlags.NonPublic);
                var result = squareMethod?.Invoke(null, new object[] { number });
                if(result is int)
                {
                    Console.WriteLine($"Square of digit {number} equal to {result}");
                }
            }
            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine($"Name of assemblies: {asm.GetName().Name}");
            }

            context.Unload();
        }

        static void Context_Unloading(AssemblyLoadContext obj)
        {
            Console.WriteLine("Library downloaded");
        }


    }
}
