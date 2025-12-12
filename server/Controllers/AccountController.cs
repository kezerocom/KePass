using KePass.Server.Services.Definitions;
using KePass.Server.ValueObjects;
using KePass.Server.Commons;
using KePass.Server.Models;
using KePass.Server.ValueObjects.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KePass.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/account")]
public class AccountController(IAccountService service) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest(new ValidationProblemDetails { Detail = "Invalid ID." });

        var result = await service.GetByIdAsync(id);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("username/{username}")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByUsername([FromRoute] string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return BadRequest(new ValidationProblemDetails { Detail = "Username is required." });

        var result = await service.GetByUsernameAsync(username);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("email/{email}")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromRoute] string email)
    {
        var emailObj = new Email(email);
        if (!emailObj.IsValid()) return BadRequest(new ValidationProblemDetails { Detail = "Invalid email format." });

        var result = await service.GetByEmailAsync(emailObj);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
            return BadRequest(new ValidationProblemDetails { Detail = "Username is required." });

        var email = new Email(request.Email);
        var password = request.Password.ToPassword();
        if (!email.IsValid()) return BadRequest(new ValidationProblemDetails { Detail = "Invalid email format." });

        if (!password.IsValid())
            return BadRequest(new ValidationProblemDetails { Detail = "Invalid password." });

        var result = await service.CreateAsync(request.Username, email, password, AccountRole.User);
        if (!result.Success)
            return BadRequest(new ErrorProblemDetails("Account Creation Failed",
                result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);

        return CreatedAtAction(nameof(GetById), new { id = result.Result.Id }, response);
    }

    [HttpPut("{id:guid}/username")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUsername([FromRoute] Guid id, [FromBody] string newUsername)
    {
        if (id == Guid.Empty || string.IsNullOrWhiteSpace(newUsername))
            return BadRequest(new ValidationProblemDetails { Detail = "Invalid input." });

        var result = await service.UpdateUsernameAsync(id, newUsername);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found",
                result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);
        return Ok(response);
    }

    [HttpPut("{id:guid}/email")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmail([FromRoute] Guid id, [FromBody] string newEmail)
    {
        var emailObj = new Email(newEmail);
        if (id == Guid.Empty || !emailObj.IsValid())
            return BadRequest(new ValidationProblemDetails { Detail = "Invalid input." });

        var result = await service.UpdateEmailAsync(id, emailObj);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found",
                result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);
        return Ok(response);
    }

    [HttpPut("{id:guid}/password")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePassword([FromRoute] Guid id, [FromBody] UpdatePasswordRequest request)
    {
        var newPassword = request.ToPassword();

        if (id == Guid.Empty || !newPassword.IsValid())
            return BadRequest(new ValidationProblemDetails { Detail = "Invalid password." });

        var result = await service.UpdatePasswordAsync(id, newPassword);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);
        return Ok(response);
    }

    [HttpPut("{id:guid}/activate")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate([FromRoute] Guid id)
    {
        if (id == Guid.Empty) return BadRequest(new ValidationProblemDetails { Detail = "Invalid ID." });

        var result = await service.ActivateAsync(id);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);
        return Ok(response);
    }

    [HttpPut("{id:guid}/role")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRole([FromRoute] Guid id, [FromBody] UpdateRoleRequest request)
    {
        if (id == Guid.Empty) return BadRequest(new ValidationProblemDetails { Detail = "Invalid ID." });

        var result = await service.UpdateRoleAsync(id, request.Role);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);
        return Ok(response);
    }

    [Authorize]
    [HttpPut("{id:guid}/deactivate")]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate([FromRoute] Guid id)
    {
        if (id == Guid.Empty) return BadRequest(new ValidationProblemDetails { Detail = "Invalid ID." });

        var result = await service.DeactivateAsync(id);
        if (!result.Success)
            return NotFound(new ErrorProblemDetails("Resource Not Found", result.Error));

        var response = AccountDetails.CreateFromAccount(result.Result);
        return Ok(response);
    }

    [Serializable]
    public class CreateRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required PasswordDetails Password { get; set; }
    }

    [Serializable]
    public class UpdateRoleRequest
    {
        public AccountRole Role { get; set; }
    }

    [Serializable]
    public class UpdatePasswordRequest : PasswordDetails
    {
    }


    [Serializable]
    public class PasswordDetails
    {
        public required PasswordAlgorithm Algorithm { get; set; }
        public required string Hash { get; set; }
        public required string Salt { get; set; }
        public required uint Memory { get; set; }
        public required uint Iterations { get; set; }
        public required uint Parallelism { get; set; }
        public required double Version { get; set; }

        public Password ToPassword()
        {
            return new Password
            {
                Algorithm = Algorithm,
                Memory = Memory,
                Hash = Convert.FromHexString(Hash),
                Salt = Convert.FromHexString(Salt),
                Iterations = Iterations,
                Parallelism = Parallelism,
                Version = Version
            };
        }
    }

    [Serializable]
    public class AccountDetails
    {
        public required Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required bool IsActive { get; set; }
        public required AccountRole Role { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }

        public static AccountDetails CreateFromAccount(Account account)
        {
            return new AccountDetails
            {
                Id = account.Id,
                Username = account.Username,
                Email = account.Email.ToString(),
                IsActive = account.IsActive,
                Role = account.Role,
                CreatedAt = account.CreatedAt,
                UpdatedAt = account.UpdatedAt
            };
        }
    }
}