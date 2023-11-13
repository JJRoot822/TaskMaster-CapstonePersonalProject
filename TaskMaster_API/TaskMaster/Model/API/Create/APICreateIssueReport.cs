﻿namespace TaskMaster.Model.API.Create;

public class APICreateIssueReport
{
    public string Title { get; set; }

    public int Priority { get; set; }

    public int Severity { get; set; }

    public int IssueType { get; set; }

    public string Details { get; set; }

    public bool Fixed { get; set; }

    public DateTime? DateResolved { get; set; }

    public string Notes { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }
}
