﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResponseModel
    {
        public long TotalItems {  get; set; }

        public int PageIndex {  get; set; }

        public int PageSize {  get; set; }

        public dynamic data { get; set; }
    }
}
