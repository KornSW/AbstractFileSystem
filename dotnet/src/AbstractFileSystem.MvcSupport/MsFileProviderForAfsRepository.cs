using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Utils;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.IO.Abstraction;

namespace Microsoft.AspNetCore.AfsSupport {

  /// <summary>
  /// An Wrapper to link an AFS-Repository inti the "ASP.NET Core FileProvider"-World
  /// </summary>
  public class MsFileProviderForAfsRepository : IFileProvider   {


    private class AfsFileInfo : IFileInfo {

      public bool Exists => throw new NotImplementedException();

      public long Length => throw new NotImplementedException();

      public string PhysicalPath => throw new NotImplementedException();

      public string Name => throw new NotImplementedException();

      public DateTimeOffset LastModified => throw new NotImplementedException();

      public bool IsDirectory => throw new NotImplementedException();

      public Stream CreateReadStream() {
        throw new NotImplementedException();
      }
    }



    private IAfsRepository _InnerAfsRepo;

    public MsFileProviderForAfsRepository(IAfsRepository afsRepoToWrap, string baseUrl) {
      _InnerAfsRepo = afsRepoToWrap;
    }

    public IFileInfo GetFileInfo(string subpath) {
      if (string.IsNullOrEmpty(subpath)) {
        return new NotFoundFileInfo(subpath);
      }

      throw new NotImplementedException();
      //StringBuilder stringBuilder = new StringBuilder(_baseNamespace.Length + subpath.Length);
      //stringBuilder.Append(_baseNamespace);
      //if (subpath.StartsWith("/", StringComparison.Ordinal)) {
      //  subpath = subpath.Substring(1, subpath.Length - 1);
      //}

      //string value = MakeValidEverettIdentifier(Path.GetDirectoryName(subpath));
      //if (!string.IsNullOrEmpty(value)) {
      //  stringBuilder.Append(value);
      //  stringBuilder.Append('.');
      //}

      //stringBuilder.Append(Path.GetFileName(subpath));
      //string text = stringBuilder.ToString();
      //if (HasInvalidPathChars(text)) {
      //  return new NotFoundFileInfo(text);
      //}

      //string fileName = Path.GetFileName(subpath);
      //if (_assembly.GetManifestResourceInfo(text) == null) {
      //  return new NotFoundFileInfo(fileName);
      //}

      //return new EmbeddedResourceFileInfo(_assembly, text, fileName, _lastModified);
    }




    public IDirectoryContents GetDirectoryContents(string subpath) {
      if (subpath == null) {
        return NotFoundDirectoryContents.Singleton;
      }

      if (subpath.Length != 0 && !string.Equals(subpath, "/", StringComparison.Ordinal)) {
        return NotFoundDirectoryContents.Singleton;
      }


      throw new NotImplementedException();
      //List<IFileInfo> list = new List<IFileInfo>();
      //string[] manifestResourceNames = _assembly.GetManifestResourceNames();
      //foreach (string text in manifestResourceNames) {
      //  if (text.StartsWith(_baseNamespace, StringComparison.Ordinal)) {
      //    list.Add(new EmbeddedResourceFileInfo(_assembly, text, text.Substring(_baseNamespace.Length), _lastModified));
      //  }
      //}

      //return new EnumerableDirectoryContents(list);
    }


    public IChangeToken Watch(string pattern) {
      return NullChangeToken.Singleton;
    }

    private static bool IsValidEverettIdFirstChar(char c) {
      if (!char.IsLetter(c)) {
        return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.ConnectorPunctuation;
      }

      return true;
    }


    private static bool IsValidEverettIdChar(char c) {
      UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
      if (!char.IsLetterOrDigit(c) && unicodeCategory != UnicodeCategory.ConnectorPunctuation && unicodeCategory != UnicodeCategory.NonSpacingMark && unicodeCategory != UnicodeCategory.SpacingCombiningMark) {
        return unicodeCategory == UnicodeCategory.EnclosingMark;
      }

      return true;
    }

    //
    // Summary:
    //     Make a folder subname into an Everett-compatible identifier
    private static void MakeValidEverettSubFolderIdentifier(StringBuilder builder, string subName) {
      if (string.IsNullOrEmpty(subName)) {
        return;
      }

      if (IsValidEverettIdFirstChar(subName[0])) {
        builder.Append(subName[0]);
      }
      else {
        builder.Append('_');
        if (IsValidEverettIdChar(subName[0])) {
          builder.Append(subName[0]);
        }
      }

      for (int i = 1; i < subName.Length; i++) {
        if (!IsValidEverettIdChar(subName[i])) {
          builder.Append('_');
        }
        else {
          builder.Append(subName[i]);
        }
      }
    }

    //
    // Summary:
    //     Make a folder name into an Everett-compatible identifier
    internal static void MakeValidEverettFolderIdentifier(StringBuilder builder, string name) {
      if (!string.IsNullOrEmpty(name)) {
        int length = builder.Length;
        string[] array = name.Split(new char[1] { '.' });
        MakeValidEverettSubFolderIdentifier(builder, array[0]);
        for (int i = 1; i < array.Length; i++) {
          builder.Append('.');
          MakeValidEverettSubFolderIdentifier(builder, array[i]);
        }

        if (builder.Length - length == 1 && builder[length] == '_') {
          builder.Append('_');
        }
      }
    }

    //
    // Summary:
    //     This method is provided for compatibility with Everett which used to convert
    //     parts of resource names into valid identifiers
    private static string MakeValidEverettIdentifier(string name) {
      if (string.IsNullOrEmpty(name)) {
        return name;
      }

      StringBuilder stringBuilder = new StringBuilder(name.Length);
      string[] array = name.Split('/', '\\');
      MakeValidEverettFolderIdentifier(stringBuilder, array[0]);
      for (int i = 1; i < array.Length; i++) {
        stringBuilder.Append('.');
        MakeValidEverettFolderIdentifier(stringBuilder, array[i]);
      }

      return stringBuilder.ToString();
    }

  }

}
