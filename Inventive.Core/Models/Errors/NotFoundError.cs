using FluentResults;

namespace Inventive.Core.Models.Errors;

public class NotFoundError(string message) : Error(message);
