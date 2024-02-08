using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Utils;

namespace System.IO.Abstraction {

  public static class AfsExtensions {

    public static void CopyFile(this IAfsRepository sourceRepo, string fileKey, IAfsRepository targetRepo) {

      //string otp = sourceRepo.RequestOtpForDownloadContent(fileKey);
      //byte[] fileContent = sourceRepo.DownloadFileContent(otp);

      //var creationAttribs = targetRepo.CreateAttributesTemplate();
      //var sourceAttribs = sourceRepo.LoadFileAttributes(
      //  new string[] { fileKey }, creationAttribs.Keys.ToArray()
      //).Single();

      //foreach (string attribName in creationAttribs.Keys) {
      //  if(sourceAttribs.TryGetValue( attribName, out var value)) {
      //    creationAttribs[attribName] = value;
      //  }
      //}

      //string creationOtp = targetRepo.RequestOtpForNewFileCreation(creationAttribs);
      //string newlyCreatedKey = targetRepo.CreateNewFile(
      //  creationOtp, fileContent, sourceAttribs[AfsWellknownAttributeNames.MimeType]
      //);

    }

    public static Dictionary<string, string> CreateAttributesTemplate(this IAfsRepository repo) {
      var dict = new Dictionary<string, string>();
      AfsAttributeDescriptor[] attribs = repo.GetAvailableAttributes();
      foreach (var a in attribs.Where((a) => a.RequiredOnCreation).Select(
        (kvp) => new KeyValuePair<string, string>(kvp.AttributeName, kvp.AttributeType.GetDefaultValue())
      )) {
        dict.Add(a.Key, a.Value);
      }
      return dict;
    }

    private static DateTime _UnixEpoch = new DateTime(1970, 01, 01,0,0,0,DateTimeKind.Utc);
    public static string GetDefaultValue(this AfsAttributeType attribType) {
 
      if (attribType == AfsAttributeType.String) return "";
      if (attribType == AfsAttributeType.Number) return "0";
      if (attribType == AfsAttributeType.ISODateTime) return DateTime.Now.ToString("O");
      if (attribType == AfsAttributeType.UnixTimestamp) return (Math.Round(_UnixEpoch.Subtract(DateTime.UtcNow).TotalSeconds,0)).ToString();
      if (attribType == AfsAttributeType.AreaPath) return "/";
      if (attribType == AfsAttributeType.UserIdenity) return "";

      if (attribType == AfsAttributeType.Flag) return "0";
      if (attribType == AfsAttributeType.HiddenFlag) return "0";
      if (attribType == AfsAttributeType.AchiveFlag) return "0";
      if (attribType == AfsAttributeType.WriteProtectionFlag) return "0";

      return string.Empty;
    }

  }

}
