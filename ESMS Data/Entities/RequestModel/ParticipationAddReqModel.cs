﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESMS_Data.Entities.RequestModel
{
    public class ParticipationAddReqModel
    {
        public int Idt {  get; set; }
        public string Subject { get; set; }
        public string Room { get; set; }
        public List<string> Students { get; set; }
    }
}
