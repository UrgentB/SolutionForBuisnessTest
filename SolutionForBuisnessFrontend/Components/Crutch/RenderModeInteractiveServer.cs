using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

//У blazor есть баг при котором страницы с typeparam и rendermode компилируются некорректно
//поэтому был сипользован костыль отсуда: https://github.com/dotnet/razor/issues/9683

namespace SolutionForBuisnessFrontend.Components.Crutch
{
    class RenderModeInteractiveServer : RenderModeAttribute
    {
        public override IComponentRenderMode Mode => RenderMode.InteractiveServer;
    }
}
