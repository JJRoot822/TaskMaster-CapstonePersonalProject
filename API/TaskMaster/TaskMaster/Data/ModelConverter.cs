using Microsoft.EntityFrameworkCore;

using TaskMaster.Data.Context;
using TaskMaster.Model.API;
using TaskMaster.Model.Domain.ProjectData;
using TaskMaster.Model.Domain.ProjectData.TaskitemData;
using TaskMaster.Model.Domain.UserData;

namespace TaskMaster.Data;

public class ModelConverter
{
    public static APIUser ToAPIUser(TaskMasterDbContext context, User user)
    {
        APIUser apiUser = new APIUser();
        apiUser.UserId = user.UserId;
        apiUser.FirstName = user.FirstName;
        apiUser.LastName = user.LastName;
        apiUser.Username = user.Username;
        apiUser.Email = user.Email;

        context.Entry(user).Collection(u => u.Projects).Load();
        context.Entry(user).Reference(u => u.Role).Load();

        apiUser.Role = user.Role.RoleName;
        apiUser.Projects = ToListOfAPIProjects(context, user.Projects);

        return apiUser;
    }

    public static List<APIUser> ToListOfAPIUsers(TaskMasterDbContext context, List<User> users)
    {
        List<APIUser> apiUsers = new List<APIUser>();

        foreach (var user in users)
        {
            APIUser apiUser = ToAPIUser(context, user);

            apiUsers.Add(apiUser);
        }

        return apiUsers;
    }

    public static APIProject ToAPIProject(TaskMasterDbContext context, Project project)
    {
        APIProject apiProject = new APIProject();
        apiProject.ProjectId = project.ProjectId;
        apiProject.Name = project.Name;
        apiProject.Description = project.Description;
        apiProject.Color = project.Color;
        apiProject.ReleaseDate = project.ReleaseDate;
        apiProject.UserId = project.UserId;

        context.Entry(project).Collection(p => p.TaskItems).Load();
        context.Entry(project).Collection(p => p.BugReports).Load();
        context.Entry(project).Collection(p => p.TestCases).Load();
        context.Entry(project).Collection(p => p.IssueReports).Load();

        apiProject.Tasks = ToListOfAPITaskItems(project.TaskItems);
        apiProject.Bugs = ToListOfAPIBugReports(project.BugReports);
        apiProject.Issues = ToListOfAPIIssueReports(project.IssueReports);
        apiProject.TestCases = ToListOfAPITestCases(project.TestCases);

        return apiProject;
    }
    public static List<APIProject> ToListOfAPIProjects(TaskMasterDbContext context, List<Project> projects)
    {
        List<APIProject> apiProjects = new List<APIProject>();

        foreach (var project in projects)
        {
            APIProject apiProject = ToAPIProject(context, project);

            apiProjects.Add(apiProject);
        }

        return apiProjects;
    }

    public static APITaskItem ToAPITaskItem(TaskItem taskItem)
    {
        APITaskItem apiTaskItem = new APITaskItem();
        apiTaskItem.TaskItemId = taskItem.TaskItemId;
        apiTaskItem.Title = taskItem.Title;
        apiTaskItem.Priority = taskItem.Priority;
        apiTaskItem.Completed = taskItem.Completed;
        apiTaskItem.Notes = taskItem.Notes;
        apiTaskItem.Details = taskItem.Details;
        apiTaskItem.DueDate = taskItem.DueDate;
        apiTaskItem.CompletedDate = taskItem.CompletedDate;
        apiTaskItem.UserId = taskItem.UserId;
        apiTaskItem.ProjectId = taskItem.ProjectId;

        return apiTaskItem;
    }
    
    public static List<APITaskItem> ToListOfAPITaskItems(List<TaskItem> tasks)
    {
        List<APITaskItem> apiTaskItems = new List<APITaskItem>();
        
        foreach (var taskItem in tasks)
        {
            APITaskItem apiTaskItem = ToAPITaskItem(taskItem);
        
            apiTaskItems.Add(apiTaskItem);
        }

        return apiTaskItems;
    }

    public static APIBugReport ToAPIBugReport(BugReport bugReport)
    {
        APIBugReport apiBugReport = new APIBugReport();
        apiBugReport.BugReportId = bugReport.BugReportId;
        apiBugReport.Title = bugReport.Title;
        apiBugReport.Details = bugReport.Details;
        apiBugReport.Notes = bugReport.Notes;
        apiBugReport.Priority = bugReport.Priority;
        apiBugReport.Severity = bugReport.Severity;
        apiBugReport.Fixed = bugReport.Fixed;
        apiBugReport.DateFixed = bugReport.DateFixed;
        apiBugReport.UserId = bugReport.UserId;
        apiBugReport.ProjectId = bugReport.ProjectId;

        return apiBugReport;
    }

    public static List<APIBugReport> ToListOfAPIBugReports(List<BugReport> bugReports)
    {
        List<APIBugReport> apiBugReports = new List<APIBugReport>();

        foreach (var bugReport in bugReports)
        {
            APIBugReport apiBugReport = ToAPIBugReport(bugReport);

            apiBugReports.Add(apiBugReport);
        }

        return apiBugReports;
    }

    public static APIIssueReport ToAPIIssueReport(IssueReport issueReport)
    {
        APIIssueReport apiIssueReport = new APIIssueReport();
        apiIssueReport.IssueReportId = issueReport.IssueReportId;
        apiIssueReport.Title = issueReport.Title;
        apiIssueReport.Priority = issueReport.Priority;
        apiIssueReport.Severity = issueReport.Severity;
        apiIssueReport.IssueType = issueReport.IssueType;
        apiIssueReport.Details = issueReport.Details;
        apiIssueReport.Fixed = issueReport.Fixed;
        apiIssueReport.DateResolved = issueReport.DateResolved;
        apiIssueReport.Notes = issueReport.Notes;
        apiIssueReport.UserId = issueReport.UserId;
        apiIssueReport.ProjectId = issueReport.ProjectId;

        return apiIssueReport; ;
    }

    public static List<APIIssueReport> ToListOfAPIIssueReports(List<IssueReport> issues)
    {
        List<APIIssueReport> apiIssueReports = new List<APIIssueReport>();

        foreach (var issueReport in issues)
        {
            APIIssueReport apiIssueReport = ToAPIIssueReport(issueReport);

            apiIssueReports.Add(apiIssueReport);
        }

        return apiIssueReports;
    }

    public static APITestCase ToAPITestCase(TestCase testCase)
    {
        APITestCase apiTestCase = new APITestCase();
        apiTestCase.TestCaseId = testCase.TestCaseId;
        apiTestCase.Title = testCase.Title;
        apiTestCase.Details = testCase.Details;
        apiTestCase.UserId = testCase.UserId;
        apiTestCase.ProjectId = testCase.ProjectId;

        return apiTestCase;
    }
    
    public static List<APITestCase> ToListOfAPITestCases(List<TestCase> testCases)
    {
        List<APITestCase> apiTestCases = new List<APITestCase>();

        foreach (var testCase in testCases)
        {
            APITestCase apiTestCase = ToAPITestCase(testCase);

            apiTestCases.Add(apiTestCase);
        }

        return apiTestCases;
    }
}
