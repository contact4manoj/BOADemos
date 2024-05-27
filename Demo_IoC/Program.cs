namespace Demo_IoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DEMO OF CONTROLLED CODE");
            Process1.DoSomething();
            Console.WriteLine();

            Console.WriteLine("DEMO OF IoC using Delegate (callback code)");
            Process2.DoSomething(X.MyCustomStep2);
            Console.WriteLine();

            Console.WriteLine("DEMO OF IoC using Event");
            Process2 p2 = new Process2();
            // p2.OnStep2 += new StepHandler(X.MyCustomStep2);
            p2.OnStep2 += X.MyCustomStep2;
            p2.OnStep2 += X.MyCustomStep3;
            p2.DoSomethingByEvent();
            Console.WriteLine();

            Console.WriteLine("DEMO OF IoC using Interface");
            IStepActivity objSilver = new SilverProcess();
            IStepActivity objGold = new GoldProcess();
            Process3.DoSomething(objSilver);
            Console.WriteLine();
        }
    }

    internal class X
    {
        internal static void MyCustomStep1()
        {
            Console.WriteLine("---- customized step 1");
        }

        internal static void MyCustomStep2()
        {
            Console.WriteLine("---- customized step 2");
        }

        internal static void MyCustomStep3()
        {
            Console.WriteLine("---- customized step 3");
        }
    }
}
