

using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MyBlazor.Client.Helper
{
    public class MyConsole
    {

        public IJSRuntime JSRuntime;

        public MyConsole(IJSRuntime JSRuntime)
        {
            this.JSRuntime = JSRuntime;
        }
        public ValueTask log(params object[] o)
        {
            return JSRuntime.InvokeVoidAsync("console.log", o);
        }
    }

    // public static class console {

    //     public static IJSRuntime JSRuntime  = new JSRuntime();
    //     public static async Task log() {

    //     }
    // }
}