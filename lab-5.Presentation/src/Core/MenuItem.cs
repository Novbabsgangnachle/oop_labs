﻿namespace DefaultNamespace;

public sealed class MenuItem
{
    public string Title { get; }
    public Action Action { get; }

    public MenuItem(string title, Action action)
    {
        Title = title;
        Action = action;
    }
}