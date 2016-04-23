﻿//
//  PackageExtractionHandler.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2016 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using Warcraft.MPQ;
using System.IO;
using Warcraft.MPQ.FileInfo;
using System.Collections.Generic;

namespace Everlook.Package
{
	/// <summary>
	/// Package interaction handler. This class is responsible for loading a package and performing file operations
	/// on it.
	/// </summary>
	public class PackageInteractionHandler : IDisposable
	{
		/// <summary>
		/// Gets the package path.
		/// </summary>
		/// <value>The package path.</value>
		public string PackagePath
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the package.
		/// </summary>
		/// <value>The name of the package.</value>
		public string PackageName
		{
			get
			{
				return Path.GetFileName(PackagePath);
			}
		}

		private readonly MPQ Package;

		/// <summary>
		/// Initializes a new instance of the <see cref="Everlook.Package.PackageInteractionHandler"/> class.
		/// </summary>
		/// <param name="InPackagePath">In package path.</param>
		public PackageInteractionHandler(string InPackagePath)
		{
			if (File.Exists(InPackagePath))
			{
				this.Package = new MPQ(new FileStream(InPackagePath, FileMode.Open, FileAccess.Read, FileShare.Read));
			}
			else
			{
				throw new FileNotFoundException("No package could be found at the specified path.");
			}

			this.PackagePath = InPackagePath;
		}

		/// <summary>
		/// Gets the file list.
		/// </summary>
		/// <returns>The file list.</returns>
		public List<string> GetFileList()
		{
			return this.Package.GetFileList();
		}

		/// <summary>
		/// Checks if the package contains the specified file.
		/// </summary>
		/// <returns><c>true</c>, if the package contains the file, <c>false</c> otherwise.</returns>
		/// <param name="fileReference">File reference.</param>
		public bool ContainsFile(ItemReference fileReference)
		{
			if (!fileReference.IsFile)
			{
				return false;
			}

			return Package.DoesFileExist(fileReference.ItemPath);
		}

		/// <summary>
		/// Extracts the specified reference from its associated package.
		/// </summary>
		/// <param name="fileReference">File reference.</param>
		public byte[] ExtractReference(ItemReference fileReference)
		{
			if (!fileReference.IsFile)
			{
				return null;
			}

			return Package.ExtractFile(fileReference.ItemPath);		
		}

		/// <summary>
		/// Gets a set of information about the specified package file, such as stored size, disk size
		/// and storage flags.
		/// </summary>
		/// <returns>The reference info.</returns>
		/// <param name="fileReference">File reference.</param>
		public MPQFileInfo GetReferenceInfo(ItemReference fileReference)
		{
			if (!fileReference.IsFile)
			{
				return null;
			}

			return Package.GetFileInfo(fileReference.ItemPath);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Everlook.Package.PackageInteractionHandler"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="Everlook.Package.PackageInteractionHandler"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="Everlook.Package.PackageInteractionHandler"/> in an unusable state. After calling <see cref="Dispose"/>,
		/// you must release all references to the <see cref="Everlook.Package.PackageInteractionHandler"/> so the garbage
		/// collector can reclaim the memory that the <see cref="Everlook.Package.PackageInteractionHandler"/> was occupying.</remarks>
		public void Dispose()
		{
			this.Package.Dispose();
		}
	}
}

