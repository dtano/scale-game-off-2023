using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ErrorSourceEnum
{
    ELEVATOR_FULL,
    RESERVES_FULL
}

public class ErrorDTO
{
    private ErrorSourceEnum _errorSource;
    private string _errorMessage;

    public ErrorSourceEnum Source => _errorSource;
    public string Message => _errorMessage;

    public ErrorDTO(ErrorSourceEnum errorSource, string errorMessage)
    {
        _errorSource = errorSource;
        _errorMessage = errorMessage;
    }
}
