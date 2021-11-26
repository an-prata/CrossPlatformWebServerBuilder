// Copyright (c) Evan Overman (https://github.com/an-prata)
// CrossPlatformWebServerBuilder (https://github.com/an-prata/CrossPlatformWebServerBuilder)

using System;
using System.IO;
using System.Text;

namespace CrossPlatformWebServerBuilder.Models
{
	class ScriptBuilder
	{
		public static readonly string[] Requires = new string[] 
		{
			"express = require('express');",
			"path = require('path');\n",
			"app = express();\n"
		};

		public static string MakeListen(int port) => 
			$"app.listen({Convert.ToString(port)}, console.log('Server listening on port {Convert.ToString(port)}'));\n";

		public static string[] MakeGet(string filePath, string url) => new string[] 
		{
			$"app.get('\"{url}\"" + "', (req, res) => {",
			$"    res.sendFile(path.join(__dirname + '\"{filePath}\"'));",
			$"    console.log('Got request for {url} ({filePath}) ... ');",
			"});\n"
		};

		public static string MakeSingleString(string[] sArray)
		{
			string singleString = "";
			foreach (string s in sArray) singleString += s + '\n';
			return singleString;
		}

		public static string[] GetLines(FileStream fileStream) 
		{
			byte[] bytes = new byte[fileStream.Length];
			fileStream.Read(bytes, 0, (int)fileStream.Length);
			return Encoding.Default.GetString(bytes).Split('\n');
		}

		public static void SaveFile(FileStream fileStream, string lines)
		{
			byte[] bytes = new byte[lines.Length];
			for (int i = 0; i < lines.Length; i++) bytes[i] = Convert.ToByte(lines[i]);
			fileStream = new FileStream(fileStream.Name, FileMode.Create);
			fileStream.Write(bytes, 0, bytes.Length);
		}
	}
}
