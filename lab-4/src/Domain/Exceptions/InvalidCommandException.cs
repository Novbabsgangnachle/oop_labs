using System;

namespace lab_4.Domain.Exceptions;

public class InvalidCommandException(string message) : Exception(message);