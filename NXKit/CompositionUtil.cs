﻿using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.Contracts;

namespace NXKit
{

    /// <summary>
    /// Composition utilities.
    /// </summary>
    public static class CompositionUtil
    {

        readonly static object sync = new object();
        static ComposablePartCatalog defaultCatalog;

        /// <summary>
        /// Gets the default catalog.
        /// </summary>
        public static ComposablePartCatalog DefaultCatalog
        {
            get { return GetDefaultCatalog(); }
        }

        /// <summary>
        /// Implements the getter for DefaultCatalog
        /// </summary>
        /// <returns></returns>
        static ComposablePartCatalog GetDefaultCatalog()
        {
            if (defaultCatalog == null)
                lock (sync)
                    if (defaultCatalog == null)
                        defaultCatalog = new ApplicationCatalog();

            return defaultCatalog;
        }

        /// <summary>
        /// Creates a new <see cref="CompositionContainer"/>.
        /// </summary>
        /// <returns></returns>
        public static CompositionContainer CreateContainer()
        {
            return new CompositionContainer(DefaultCatalog);
        }

        /// <summary>
        /// Adds the specified value to the container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="export"></param>
        public static CompositionContainer WithExport<T>(this CompositionContainer container, T export)
            where T : class
        {
            Contract.Requires<ArgumentNullException>(container != null);
            Contract.Requires<ArgumentNullException>(export != null);

            container.ComposeExportedValue(export);

            return container;
        }

    }

}