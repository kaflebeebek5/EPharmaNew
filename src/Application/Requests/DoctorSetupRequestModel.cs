﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPharma.Application.Requests
{
    public class DoctorSetupRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialist { get; set; }
        public  string ImagePath { get; set; }
        public string Address { get; set; }
        public int GenderId { get; set; }
        public UploadReceipt uploadReceipt { get; set; }
    }
    public class UploadReceipt : UploadRequest
    {

    }
}
