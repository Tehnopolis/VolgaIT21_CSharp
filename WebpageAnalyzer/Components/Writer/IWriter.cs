using System.Collections.Generic;

namespace WebpageAnalyzer.Components
{
    public interface IWriter
    {
        void Write(Dictionary<string, int> data); 
    }
}
