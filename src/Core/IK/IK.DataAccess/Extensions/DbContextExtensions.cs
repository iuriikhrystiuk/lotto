// <copyright file="DbContextExtensions.cs">
// This is a property of a Iurii Khrystiuk.
// All of the code comes as is and no license required.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace IK.DataAccess.Extensions
{
    /// <summary>
    ///     The extension methods for <see cref="DbContext"/> class.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Gets the name of the table from the context.
        /// </summary>
        /// <param name="context">The target context.</param>
        /// <param name="type">The target type.</param>
        /// <returns>The name of the table.</returns>
        public static string GetTableName(this DbContext context, Type type)
        {
            MetadataWorkspace metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            // Get the part of the model that contains info about the actual CLR types
            ObjectItemCollection objectItemCollection = (ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace);

            // Get the entity type from the model that maps to the CLR type
            EntityType entityType = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == type);

            // Get the entity set that uses this entity type
            EntitySet entitySet = metadata
                .GetItems<EntityContainer>(DataSpace.CSpace)
                .Single()
                .EntitySets
                .Single(s => s.ElementType.Name == entityType.Name);

            // Find the mapping between conceptual and storage model for this entity set
            EntitySetMapping mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                    .Single()
                    .EntitySetMappings
                    .Single(s => s.EntitySet == entitySet);

            // Find the storage entity set (table) that the entity is mapped
            EntitySet table = mapping
                .EntityTypeMappings.Single()
                .Fragments.Single()
                .StoreEntitySet;

            // Return the table name from the storage entity set
            string tableName = (string)table.MetadataProperties["Table"].Value ?? table.Name;
            string schemaName = (string)table.MetadataProperties["Schema"].Value ?? table.Schema;
            return string.Format("{0}.{1}", schemaName, tableName);
        }

        /// <summary>
        /// Gets the table definition for the type from the context.
        /// </summary>
        /// <param name="context">The target context.</param>
        /// <param name="type">The target type.</param>
        /// <returns>The table definition.</returns>
        public static DataTable GetTableDefinition(this DbContext context, Type type)
        {
            MetadataWorkspace metadata = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            IEnumerable<EdmProperty> props = metadata.GetItems(DataSpace.CSpace)
                .Where(m => m.BuiltInTypeKind == BuiltInTypeKind.EntityType)
                .Select(m => m as EntityType)
                .Where(t => t.Name == type.Name)
                .SelectMany(t => t.Properties);

            DataTable table = new DataTable();
            foreach (EdmProperty edmProperty in props)
            {
                table.Columns.Add(edmProperty.Name, GetType(edmProperty.UnderlyingPrimitiveType.ClrEquivalentType.FullName));
            }

            return table;
        }

        /// <summary>
        /// Gets the type using its name.
        /// </summary>
        /// <param name="fullName">The full name of the type.</param>
        /// <returns>The instance of type.</returns>
        public static Type GetType(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                return null;
            }

            Type type = Type.GetType(fullName);
            if (type == null)
            {
                string targetAssembly = fullName;
                while (type == null && targetAssembly.Length > 0)
                {
                    try
                    {
                        int dotInd = targetAssembly.LastIndexOf('.');
                        targetAssembly = dotInd >= 0 ? targetAssembly.Substring(0, dotInd) : string.Empty;
                        if (targetAssembly.Length > 0)
                        {
                            type = Type.GetType(fullName + ", " + targetAssembly);
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return type;
        }
    }
}
