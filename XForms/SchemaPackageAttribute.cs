﻿using System.ComponentModel.Composition;

namespace ISIS.Forms
{

    /// <summary>
    /// Marks a <see cref="SchemaPackage"/>.
    /// </summary>
    public class SchemaPackageAttribute : ExportAttribute
    {

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public SchemaPackageAttribute()
            : base(typeof(SchemaPackage))
        {

        }

    }

}