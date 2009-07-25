﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Assembler
{
    public class Parser : IDisposable
    {
        private StreamReader reader;

        public string currentTxtCommand;

        public Parser()
        {
            // useful for testing
        }

        public Parser(Stream inputStream)
        {
            reader = new StreamReader(inputStream);
        }

        public bool HasMoreCommands()
        {
            return !reader.EndOfStream;
        }

        public void Advance()
        {
            if (this.HasMoreCommands())
            {
                this.currentTxtCommand = reader.ReadLine();
            }
        }

        public Command CommandType()
        {
            // currently doesn't handle symbols (L commands see p110)
            Command commandType = Command.ERROR;

            if (this.currentTxtCommand.StartsWith("@")) // if it starts with @
            {
                commandType = Command.A_COMMAND;
            }
            else if (this.currentTxtCommand.StartsWith("("))
            {
                // TODO check this
                commandType = Command.L_COMMAND;
            }
            // this if must be last
            else if (this.currentTxtCommand.Contains('=')
                || this.currentTxtCommand.Contains(";"))
            {
                // TODO - should probably use regex - see P.109 for spec of c command
                commandType = Command.C_COMMAND;
            }

            return commandType;
        }

        /// <summary>
        /// Returns the destination mnemonic of a c instruction
        /// Should only be called if the current command is a c command.
        /// </summary>
        /// <returns></returns>
        public string Dest()
        {
            string theDestinationNnemonic;
            
            if (this.CommandType() != Command.C_COMMAND)
            {
                throw new Exception("Dest should only be called when CommandType is C_Command " + Environment.NewLine
                    + "Current Command Type: " + this.CommandType() + Environment.NewLine
                    + "Current current text command: " + this.currentTxtCommand);

            }

            // matches 3 uppercase letters before '=' sign
            Regex regex = new Regex(@"([A-Z]{1,3})(?==)");

            Match match = regex.Match(this.currentTxtCommand);

            theDestinationNnemonic = match.Groups[1].Value;

            return theDestinationNnemonic;
        }

        public string Symbol()
        {
            //string symbol;

            // "\([\w]{1,}\)";

            //"(@[\w]*)";

            return "";
        }

        /// <summary>
        /// Returns the comp part of a C instruction, see page 109.
        /// </summary>
        /// <returns></returns>
        public string Comp()
        {
            if (this.CommandType() != Command.C_COMMAND)
            {
                throw new Exception("Comp should only be called when CommandType is C_Command " + Environment.NewLine
                    + "Current Command Type: " + this.CommandType() + Environment.NewLine
                    + "Current current text command: " + this.currentTxtCommand);

            }
            
            string comp = String.Empty;

            //comp is either after the '=' sign (comp is part of desintation command)
            //or before the ';' (comp is part of jmp command)

            // matches after '='
            Regex regex = new Regex("(=)([^\r\n]*)");
            Match match = regex.Match(this.currentTxtCommand);
            if (match.Success)
            {
                comp = match.Groups[2].Value;
            }

            //matches before ';'
            regex = new Regex(@"([^\r\n]*)(;)");
            match = regex.Match(this.currentTxtCommand);
            if (match.Success)
            {
                comp = match.Groups[1].Value;
            }
            
            return comp;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (reader != null)
            {
                reader.Close();
            }
        }

        #endregion
    }
}
