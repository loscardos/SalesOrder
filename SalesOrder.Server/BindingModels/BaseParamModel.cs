﻿namespace SalesOrder.Server.BindingModels;

public class BaseParamModel
{
    public string? Search { get; set; }

    public int? PageSize { get; set; }

    public int? CurrentPage { get; set; }
    
}