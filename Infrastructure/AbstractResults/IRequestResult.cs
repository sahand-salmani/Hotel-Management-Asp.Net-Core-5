﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.AbstractResults
{
    public interface IRequestResult<T> where T : class
    {
        public ResultTypes ResultType { get; set; }
        public ISuccessResult<T> Result { get; set; }
        public IFailureResult Failure { get; set; }
    }
}
