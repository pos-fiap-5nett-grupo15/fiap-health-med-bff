﻿namespace Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse
{
    public class BaseServiceResponse
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;
    }
}
