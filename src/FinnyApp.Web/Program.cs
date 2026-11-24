
using FinnyApp.BLL.Services;
using FinnyApp.DAL.Database;
using FinnyApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ParentService>();

var app = builder.Build();

// Seed Data
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<DContext>();

    if (!await context.ParentTable.AnyAsync(p => p.Email == "demo@finny.com"))
    {
        var parent = new Parent
        {
            Name = "Demo Parent",
            Email = "demo@finny.com",
            Password = "123456"
        };

        context.ParentTable.Add(parent);
        await context.SaveChangesAsync();

        var child = new Child
        {
            Name = "Demo Child",
            PIN = "1234",
            Finnies = 100,
            Parent = parent
        };

        var goal = new Goal
        {
            Title = "Complete 1 Task",
            TaskAmountToFinish = 1,
            Parent = parent
        };

        var reward = new Reward
        {
            Title = "Extra Play Time",
            FinniesCost = 50,
            Parent = parent
        };

        var task = new Tasks
        {
            Title = "Clean Your Room",
            Description = "Keep your room clean and organized",
            RewardAmount = 20,
            Parent = parent
        };

        context.ChildTable.Add(child);
        context.GoalTable.Add(goal);
        context.RewardTable.Add(reward);
        context.TaskTable.Add(task);

        await context.SaveChangesAsync();

        context.ChildGoalProgressTable.Add(new ChildGoalProgress
        {
            Child = child,
            Goal = goal,
            TasksProgress = 0
        });

        context.ChildRewardSelectionTable.Add(new ChildRewardSelection
        {
            Child = child,
            Reward = reward,
            Status = false
        });

        context.ChildTaskProgressTable.Add(new ChildTaskProgress
        {
            Child = child,
            Task = task,
            Status = false
        });

        await context.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Global}/{action=Home}/{id?}")
    .WithStaticAssets();

app.Run();
