﻿namespace Web8_SecuredWebApi02.Models;

public class Person
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public Gender Gender { get; set; } = Gender.Female;

}
