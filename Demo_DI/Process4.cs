﻿namespace Demo_DI;

interface IStepActivity
{
    void Step1();
    void Step2();
    void Step3();
}

internal class Process4
{
    private readonly IStepActivity _activity;

    // The Dependency is injected during the construction of the object.
    internal Process4(IStepActivity activity)
    {
        _activity = activity;
    }

    internal void DoSomething()
    {
        _activity?.Step1();
        _activity?.Step2();
        _activity?.Step3();
    }

    internal void DoSomethingElse()
    {
        _activity?.Step1();
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
