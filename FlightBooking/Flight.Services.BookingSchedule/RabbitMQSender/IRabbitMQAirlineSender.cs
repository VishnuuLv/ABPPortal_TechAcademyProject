using Flight.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.Services.BookingSchedule.RabbitMQSender
{
    public interface IRabbitMQAirlineSender
    {
        void SendData(BaseMessage baseMessage, String queueName);
    }
}
