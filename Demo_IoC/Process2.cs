using System.Runtime.CompilerServices;

namespace Demo_IoC;

delegate void StepHandler();

internal class Process2
{

    internal event StepHandler? OnStep2;

    internal void DoSomethingByEvent()
    {
        Process2.Step1();

        OnStep2?.Invoke();

        Process2.Step3();
    }


    // The method controls what to call, and how to call.
    internal static void DoSomething(StepHandler objD)
    {
        Process2.Step1();

        //if (objD is not null)
        //{
        //    objD.Invoke();
        //}

        objD?.Invoke();
        
        Process2.Step3();
    }

    private static void Step1()
    {
        Console.WriteLine("-- step 1");
    }

    private static void Step2()
    {
        Console.WriteLine("-- step 2");
    }

    private static void Step3()
    {
        Console.WriteLine("-- step 3");
    }
}
