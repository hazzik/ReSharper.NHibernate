using JetBrains.ReSharper.Psi.Tree;

namespace NHibernatePlugin
{
    public static class TreeNodeExtensions
    {
        public static T ToTreeNode<T>(this T node) where T : ITreeNode {
            return node;
        }
    }
}