using System;

namespace DescriptiveIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DescriptiveIndex ind = new DescriptiveIndex();
                ind.AddIndexByString(Console.ReadLine());
                ind.AddIndex("Sherlock", new[] { 1, 2, 3 });
                ind.AddIndex("Watson", new[] { 12, 14, 15 });
                ind.GetDescriptiveIndexByFile(Console.ReadLine());
                ind.PrintResultForSearch("Watson");
                ind.PrintDescriptiveIndex();
                ind.PushToFile();
                ind.DeleteElement("Sherlock");
                ind.PrintDescriptiveIndex();  
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();

        }
    }
}
