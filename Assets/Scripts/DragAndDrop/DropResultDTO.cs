using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropResultDTO
{
    private bool _success;
    private ErrorDTO _error;

    public bool Success { get => _success; set => _success = value; }
    public ErrorDTO Error { get => _error; set => _error = value; }

    public DropResultDTO(bool success, ErrorDTO error)
    {
        _success = success;
        _error = error;
    }

    public DropResultDTO(ErrorDTO error)
    {
        _error = error;
    }

    public DropResultDTO(bool success)
    {
        _success = success;
    }

    public DropResultDTO()
    {

    }
}
