using System.Reflection;

namespace BLH.ApproveIQ.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}