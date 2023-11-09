﻿namespace TaskMaster.Model.API.Create;

public class APICreateProject
{
    public string Name { get; set; }

    public string Color { get; set; }

    public string? Description { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int UserId { get; set; }
}
