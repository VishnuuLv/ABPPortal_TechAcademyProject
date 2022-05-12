﻿using Flight.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight.Services.ManageAPI.Models.Dto
{
    public class LogsDto:BaseMessage
    {
        public string log { get; set; }
        public string task { get; set; }
        public string senderAPI { get; set; }
    }
}
