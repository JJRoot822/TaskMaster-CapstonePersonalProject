﻿using TaskMaster.Model.API.ProjectData.TaskItemData;
using TaskMaster.Model.API.UserData;
using TaskMaster.Model.Domain.ProjectData;

namespace TaskMaster.Model.API.ProjectData;

public class APIProject
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int UserId { get; set; }
}
