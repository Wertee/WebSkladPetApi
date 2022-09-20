﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Category.Validation
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string message) : base(message) { }
    }
}
