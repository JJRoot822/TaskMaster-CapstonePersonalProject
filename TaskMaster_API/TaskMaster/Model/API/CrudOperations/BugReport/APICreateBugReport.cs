﻿using System.ComponentModel.DataAnnotations.Schema;

namespace TaskMaster.Model.API.CrudOperations.BugReport;

public class APICreateBugReport
{
    public string Title { get; set; }
    public string Details { get; set; }
    public string Notes { get; set; }
    public int Priority { get; set; }
    public int Severity { get; set; }
    public bool Fixed { get; set; }
    public DateTime DateFixed { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }
}
