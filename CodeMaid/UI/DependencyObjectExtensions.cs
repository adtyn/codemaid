#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SteveCadwallader.CodeMaid.UI
{
    /// <summary>
    /// A set of extension methods for <see cref="DependencyObject" />.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Attempts to find the closest visual ancestor of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the ancestor.</typeparam>
        /// <param name="obj">The object to search.</param>
        /// <returns>The closest matching visual ancestor, otherwise null.</returns>
        public static T FindVisualAncestor<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(obj.FindVisualTreeRoot());

            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        /// <summary>
        /// Attempts to find a visual child of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the child.</typeparam>
        /// <param name="obj">The object to search.</param>
        /// <returns>A matching visual child, otherwise null.</returns>
        public static T FindVisualChild<T>(this DependencyObject obj)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                {
                    return (T)child;
                }

                var descendant = FindVisualChild<T>(child);
                if (descendant != null)
                {
                    return descendant;
                }
            }

            return null;
        }

        /// <summary>
        /// Attempts to find the closest visual tree root, working with the logical tree hierarchy.
        /// </summary>
        /// <param name="obj">The object to search.</param>
        /// <returns>A matching visual ancestor, otherwise null.</returns>
        public static DependencyObject FindVisualTreeRoot(this DependencyObject obj)
        {
            var current = obj;
            var result = obj;

            while (current != null)
            {
                result = current;
                if (current is Visual || current is Visual3D)
                {
                    break;
                }

                // If the current item is not a visual, try to walk up the logical tree.
                current = LogicalTreeHelper.GetParent(current);
            }

            return result;
        }
    }
}