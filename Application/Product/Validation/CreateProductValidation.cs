﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Product.DTO;

namespace Application.Product.Validation
{
    public class CreateProductValidation : ProductValidation
    {
        public CreateProductValidation(ProductDTO productDto) : base(productDto) { }


        public override void Validate()
        {
            ValidateCount();
        }
    }
}
