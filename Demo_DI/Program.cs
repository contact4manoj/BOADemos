using Demo_DI;

IStepActivity objSilver = new SilverProcess();

Console.WriteLine("DEMO OF IoC using Dependency Injection");
Process4 p = new Process4(objSilver);
p.DoSomething();

