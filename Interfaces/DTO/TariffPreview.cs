﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace DTO
{
    public class TariffPreview
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public bool isActive { get; set; }
        public DateTime? endDate { get; set; }
    }
}
