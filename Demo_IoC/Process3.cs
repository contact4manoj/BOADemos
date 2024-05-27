namespace Demo_IoC;

interface IStepActivity
{
    void Step1();
    void Step2();
    void Step3();
}

internal class Process3
{
    internal static void DoSomething(IStepActivity activity)
    {
        activity?.Step1();
        activity?.Step2();
        activity?.Step3();
    }
}

internal class SilverProcess : IStepActivity
{ 
    public void Step1()
    {
        Console.WriteLine("-- Silver step 1");
    }

    public void Step2()
    {
        Console.WriteLine("-- Silver step 2");
    }

    public void Step3()
    {
        Console.WriteLine("-- Silver step 3");
    }
}


internal class GoldProcess : IStepActivity
{
    public void Step1()
    {
        Console.WriteLine("-- Gold step 1");
    }

    public void Step2()
    {
        Console.WriteLine("-- Gold step 2");
    }

    public void Step3()
    {
        Console.WriteLine("-- Gold step 3");
    }
}
