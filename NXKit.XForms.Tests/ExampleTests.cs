﻿using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NXKit.XForms.Tests
{

    [TestClass]
    public class ExampleTests
    {

        [TestMethod]
        public void Test_form()
        {
            var host = NXDocumentHost.Load(new Uri("nx-example:///form.xml"));
        }

        [TestMethod]
        public void Test_include()
        {
            var host = NXDocumentHost.Load(new Uri("nx-example:///include.xml"));
        }

        [TestMethod]
        public void Test_script()
        {
            var host = NXDocumentHost.Load(new Uri("nx-example:///script.xml"));
        }

        [TestMethod]
        public void Test_select1()
        {
            var host = NXDocumentHost.Load(new Uri("nx-example:///select1.xml"));
        }

    }

}