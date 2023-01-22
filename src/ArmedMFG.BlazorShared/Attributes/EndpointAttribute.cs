using System;

namespace ArmedMFG.BlazorShared.Attributes;

public class EndpointAttribute : Attribute
{
    public string Name { get; set; }
}
