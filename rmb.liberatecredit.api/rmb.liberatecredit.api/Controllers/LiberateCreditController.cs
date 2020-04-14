using Microsoft.AspNetCore.Mvc;
using RMB.LiberateCredit.Domain.BindingModel;
using RMB.LiberateCredit.Domain.Interfaces.Handlers;
using RMB.LiberateCredit.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMB.LiberateCredit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiberateCreditController : ControllerBase
    {
        private readonly ILiberateCreditService _liberateCreditService;
        private readonly INotificationHandler _notificationHandler;
        public LiberateCreditController(INotificationHandler notificationHandler, ILiberateCreditService liberateCreditService)
        {
            _liberateCreditService = liberateCreditService;
            _notificationHandler = notificationHandler;
            _liberateCreditService.SetNotificationHandler(_notificationHandler);
        }
        [HttpPost]
        public IActionResult LiberateCredit(LiberateCreditBindingModel model)
        {
            var result = _liberateCreditService.Liberate(model);

            var obj = new
            {
                Notifications = _notificationHandler.Notifications,
                Result = result
            };

            return Ok(obj);
        }
    }
}
