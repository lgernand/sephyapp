using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace sephyapp.Models;

public class RegisterRequestDTO
{
    public String RoleName { get; set; }
}