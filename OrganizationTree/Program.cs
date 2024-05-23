using System;
using System.Collections.Generic;
using System.Linq;

public class OrganizationNode
{
    public string Name { get; set; }
    public List<OrganizationNode> Children { get; set; } = new List<OrganizationNode>();

    public OrganizationNode(string name)
    {
        Name = name;
    }

    public void AddChild(OrganizationNode child)
    {
        Children.Add(child);
    }

    public void PrintTree(string indent = "")
    {
        Console.WriteLine(indent + Name);
        foreach (var child in Children)
        {
            child.PrintTree(indent + "  ");
        }
    }
}

public class OrganizationData
{
    public string Name { get; set; }
    public string ParentName { get; set; } // Empty or null for root
}

public class Program
{
    public static void Main()
    {
        // 示例组织信息
        List<OrganizationData> organizationData = new List<OrganizationData>
        {
            new OrganizationData { Name = "总公司", ParentName = "" },
            new OrganizationData { Name = "分公司 A", ParentName = "总公司" },
            new OrganizationData { Name = "部门 A1", ParentName = "分公司 A" },
            new OrganizationData { Name = "部门 A2", ParentName = "分公司 A" },
            new OrganizationData { Name = "分公司 B", ParentName = "总公司" },
            new OrganizationData { Name = "部门 B1", ParentName = "分公司 B" },
            new OrganizationData { Name = "小组 B1.1", ParentName = "部门 B1" },
            new OrganizationData { Name = "小组 B1.2", ParentName = "部门 B1" },
            new OrganizationData { Name = "部门 B2", ParentName = "分公司 B" },
            new OrganizationData { Name = "分公司 C", ParentName = "总公司" }
        };

        // 构建树形结构
        OrganizationNode root = BuildTree(organizationData, "");

        // 打印树形结构
        root.PrintTree();
    }

    public static OrganizationNode BuildTree(List<OrganizationData> data, string parentName)
    {
        var rootData = data.FirstOrDefault(d => d.ParentName == parentName);
        if (rootData == null)
        {
            return null;
        }

        var root = new OrganizationNode(rootData.Name);

        var childrenData = data.Where(d => d.ParentName == rootData.Name).ToList();
        foreach (var childData in childrenData)
        {
            var childNode = BuildTree(data, childData.Name);
            if (childNode != null)
            {
                root.AddChild(childNode);
            }
        }

        return root;
    }
}
