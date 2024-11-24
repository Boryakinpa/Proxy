using System;
using System.Collections.Generic;
using ProxyPattern;

internal class Program
{
    private static void Main(string[] args)
    {
        Subject subject = new Proxy();

        subject.Request("admin request");
        subject.Request("user request");
        subject.Request("admin request");
    }
}
