using System.Threading.Tasks;

namespace LaConcordia.Helpers
{
     interface IDisplayMessage
    {
        ValueTask DisplayErrorMessage(string message);
        ValueTask DisplaySuccessMessage(string message);
    }
}
