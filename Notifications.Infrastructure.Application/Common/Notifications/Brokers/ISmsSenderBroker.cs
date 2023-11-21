using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications.Infrastructure.Application.Common.Notifications.Models;

namespace Notifications.Infrastructure.Application.Common.Notifications.Brokers
{
    public interface ISmsSenderBroker
    {
        ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
    }
}
