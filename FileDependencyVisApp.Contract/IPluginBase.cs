using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDependencyVisApp.Contract
{
    public interface IPluginBase
    {
        string PluginName { get; }
        string PluginDescription { get; }
        void Execute(object obj);
    }
}
