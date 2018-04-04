using System;

namespace com.abnamro.webapi.core
{
    public class ConsoleApiHost : IConsoleApiHost
    {
        private readonly string[] _args;

        public ConsoleApiHost(string[] args)
        {
            _args = args;
        }

        int IConsoleApiHost.BufferHeight { set => Console.BufferHeight = value; }
        string IConsoleApiHost.Title { set => Console.Title = value; }

        string[] IConsoleApiHost.Args => _args;

        string IConsoleApiHost.ReadLine() => Console.ReadLine();

        void IConsoleApiHost.WriteLine(string value) => Console.WriteLine(value);
    }
}
