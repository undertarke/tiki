using Microsoft.AspNetCore.SignalR;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoloDevApp.Service.SignalR
{
    public interface INotificationService
    {
        Task ConfirmAccount(string id, string fullname);
        Task GetThongBaoFake(string noiDung);

    }

    public class NotificationService : INotificationService
    {
        private IHubContext<AppHub> _hubContext;

        public NotificationService(IHubContext<AppHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ConfirmAccount(string id, string fullname)
        {
            await _hubContext.Clients.All.SendAsync("ConfirmAccount", id, fullname);
        }

   

        public async Task GetThongBaoFake(string noiDung)
        {
            await _hubContext.Clients.All.SendAsync("GetThongBaoFakeRT", noiDung);
        }
    }
}