﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Assembler;

namespace UnitTests
{
    /// <summary>
    /// Summary description for CodeGeneratorTests
    /// </summary>
    [TestClass]
    public class CodeGeneratorTests
    {
        public CodeGeneratorTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CodeGenDest()
        {
            Assert.IsTrue(CodeGenerator.Dest("") == "000");
            Assert.IsTrue(CodeGenerator.Dest("M") == "001");
            Assert.IsTrue(CodeGenerator.Dest("D") == "010");
            Assert.IsTrue(CodeGenerator.Dest("MD") == "011");
            Assert.IsTrue(CodeGenerator.Dest("A") == "100");
            Assert.IsTrue(CodeGenerator.Dest("AM") == "101");
            Assert.IsTrue(CodeGenerator.Dest("AD") == "110");
            Assert.IsTrue(CodeGenerator.Dest("AMD") == "111");
        }

    }
}
