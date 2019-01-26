using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

using Utilities.Files.Saving;
using Utilities.Files.Erasing;

namespace Utilities.Files
{
	public class FilesManager : MonoBehaviour
	{
		private static string baseRoute = Application.persistentDataPath;

		// //////////////////////////// //
		// ////// PUBLIC METHODS ////// //
		// //////////////////////////// //

		// route need to have / at the beggining
		// fileName is just a string

		public static bool DoesFileExists (string route, string fileName)
		{
			return File.Exists (baseRoute + route + "/" + fileName);
		}

		public static void StoreFile (string route, string fileName, object data)
		{
			Save (route, fileName, data);
		}

		public static void StoreFileBytes (string route, string fileName, byte[] data)
		{
			Save (route, fileName, data);
		}

		public static object LoadFile (string route, string fileName)
		{
			return Load (route, fileName);
		}

		public static byte[] LoadFileBytes (string route, string fileName)
		{
			return ObjectToByteArray (Load (route, fileName));
		}

		public static void EraseFolder (string folder)
		{
			Erase (folder);
		}

		public static void EraseFile (string route, string fileName)
		{
			Erase (route + "/" + fileName);
		}

		public static long GetFolderSize (string route, FileSizeType fileSizeType = FileSizeType.Bytes)
		{
			if (!Directory.Exists (baseRoute + route)) 
			{
				return 0;
			}

			string[] fileNames = Directory.GetFiles (baseRoute + route, "*.*");

			long folderSize = 0;

			foreach (string fileName in fileNames)
			{
				FileInfo info = new FileInfo (fileName);
				folderSize += info.Length;
			}

			folderSize = fileSizeType == FileSizeType.MegaBytes ? folderSize / 1024 / 1024 : folderSize;

			return folderSize;
		}

		// ///////////////////////////////// //
		// ////// FILES BASIC METHODS ////// //
		// ///////////////////////////////// //

		private static void Save (string route, string fileName, object data)
		{
			VerifyAndCreateFolder (route);

			string fullRoute = baseRoute + "/" + route + "/" + fileName;

			SaveFileCoroutine savefile = new GameObject ("Saving "+ fileName).AddComponent <SaveFileCoroutine> ();

			savefile.SetDataAndStart (baseRoute + "/" + route + "/" + fileName, ObjectToByteArray (data));

			print ("Saved in " + fullRoute);
		}

		private static object Load (string route, string filename)
		{
			object obj = null;
			string fullRoute = baseRoute + "/" + route + "/" + filename;

			print ("Loading from " + fullRoute);

			if (File.Exists (fullRoute))
			{
				obj = ByteArrayToObject (File.ReadAllBytes (fullRoute));

				//print ("Loaded");
			}
			else
			{
				//print ("File not found");
			}

			return obj;
		}

		private static void Erase (string route)
		{
			string fullRoute = baseRoute + "/" + route;

			print ("Erasing: " + fullRoute);

			EraseFileCoroutine eraseFile = new GameObject ("Erasing" + fullRoute).AddComponent <EraseFileCoroutine> ();

			eraseFile.SetDataAndStart (fullRoute);
		}

		private static void VerifyAndCreateFolder (string route)
		{
			string fullRoute = baseRoute + "/" + route;

			if (!Directory.Exists (fullRoute)) 
			{
				Directory.CreateDirectory (fullRoute);
			}
		}

		// ////////////////////////////////// //
		// ////// BYTE HADLING METHODS ////// //
		// ////////////////////////////////// //

		private static object ByteArrayToObject (byte[] arrBytes)
		{
			try
			{
				BinaryFormatter bf = new BinaryFormatter ();
				MemoryStream ms = new MemoryStream (arrBytes);
				return bf.Deserialize (ms);
			}
			catch
			{
				return (object) arrBytes;
			}
		}

		private static byte[] ObjectToByteArray (object obj)
		{
			try
			{
				return (byte[]) obj;
			}
			catch
			{
				BinaryFormatter bf = new BinaryFormatter();
				MemoryStream ms = new MemoryStream ();
				bf.Serialize (ms, obj);
				return ms.ToArray ();
			}
		}
	}

	// ///////////////////////// //
	// ////// ENUMERATORS ////// //
	// ///////////////////////// //

	public enum FileSizeType
	{
		Bytes,
		MegaBytes
	}
}