﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Quoting.Domain.Seedworking
{
    public interface IModelInconsistency
    {
        bool Error { get; }
        string Notification { get; }
    }
}