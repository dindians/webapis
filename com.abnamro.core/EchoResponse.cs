using System;

namespace com.abnamro.core
{
    public class EchoResponse
    {
        public string Echo { get; }
        public string AssemblyLocation { get; }
        public string OriginalAssemblyLocation { get; }
        public string ProcessFileName { get; }
        public DateTime ResponseDateTime { get; }

        public EchoResponse(string echo, DateTime responseDateTime, string processFileName, string assemblyLocation, string originalAssemblyLocation)
        {
            Echo = echo;
            ResponseDateTime = responseDateTime;
            ProcessFileName = processFileName;
            AssemblyLocation = assemblyLocation;
            OriginalAssemblyLocation = originalAssemblyLocation;
        }
    }
}
