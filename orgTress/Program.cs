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
            new OrganizationData { Name = "深圳总公司", ParentName = "" },
            new OrganizationData { Name = "龙华公司", ParentName = "深圳总公司" },
            new OrganizationData { Name = "分公司 A", ParentName = "总公司" },
            new OrganizationData { Name = "分公司 E", ParentName = "总公司" },
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
        var rootNodes = BuildTree(organizationData, "");

        // 打印树形结构
        foreach (var root in rootNodes)
        {
            root.PrintTree();
        }
    }

    public static List<OrganizationNode> BuildTree(List<OrganizationData> data, string parentName)
    {
        var nodes = new List<OrganizationNode>();

        var childrenData = data.Where(d => d.ParentName == parentName).ToList();
        foreach (var childData in childrenData)
        {
            var node = new OrganizationNode(childData.Name);
            node.Children = BuildTree(data, childData.Name);
            nodes.Add(node);
        }

        return nodes;
    }
}
/*
 * 
 * 
 * 
 * 解释
OrganizationNode 类：表示树中的一个节点，每个节点有一个名称和一个子节点列表。
OrganizationData 类：表示组织信息，每个对象包含节点名称及其父节点名称。
BuildTree 方法：
递归构建树形结构的方法。
根据父节点名称找到所有子节点，然后递归调用自己为每个子节点构建其子树。
方法返回一个节点列表，可以包含多个根节点（在某些情况下可能会有多个根节点，如多棵树）。
Main 方法：
示例组织信息列表，并调用 BuildTree 方法来构建树形结构，然后打印结果。
 
 总公司
  分公司 A
    部门 A1
    部门 A2
  分公司 B
    部门 B1
      小组 B1.1
      小组 B1.2
    部门 B2
  分公司 C

 
 */