using System;

namespace lab_4.Domain.Exceptions;

public class FileSystemException(string message) : Exception(message);