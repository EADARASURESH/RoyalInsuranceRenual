﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Royal.Insurance.Renual.DTO
{
    public class InputData
    {
        public string Name { get; set; }
        public byte[] CsvFile { get; set; }
    }
}
