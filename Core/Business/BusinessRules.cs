﻿using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public class BusinessRules
    {
        //params verdiğimde run içerisinde istedğimiz kadar kkuralları veriyorum.
        public static IResult Run(params IResult[] logics) {

            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }
                
            }
            return null;
        } 
        
    }
}
