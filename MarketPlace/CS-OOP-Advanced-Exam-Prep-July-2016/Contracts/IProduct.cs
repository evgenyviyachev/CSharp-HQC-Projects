﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_OOP_Advanced_Exam_Prep_July_2016.Contracts
{
    public interface IProduct
    {
        string Name { get; set; }
        int Size { get; set; }
        int ID { get; set; }
    }
}