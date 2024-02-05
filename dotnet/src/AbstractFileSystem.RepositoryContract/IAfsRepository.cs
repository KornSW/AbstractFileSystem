using System.Collections.Generic;

namespace System.IO.Abstraction {

  public interface IAfsRepository {

     AfsRepositoryCapabilities GetCapabilities();

    /// <summary>
    /// gets the list of supported attributes.
    /// The FIRST ITEM is that attribute, which represents the KEY of a file
    /// and is used for every request arguments named 'fileKey' 
    /// </summary>
    AfsAttributeDescriptor[] GetAvailableAttributes();

    #region " pre-checks for certain file operations "

    /// <summary>
    /// returns true, when it is possible to delete ALL of the given files
    /// </summary>
    /// <param name="fileKeys"></param>
    /// <returns></returns>
    bool CanDelete(string[] fileKeys);

    /// <summary>
    /// returns true, when it is possible to overwrite ALL of the given files
    /// </summary>
    /// <param name="fileKeys"></param>
    /// <returns></returns>
    bool CanOverwrite(string[] fileKeys);

    /// <summary>
    /// returns true, when it is possible to download the content of ALL of the given files
    /// </summary>
    /// <param name="fileKeys"></param>
    /// <returns></returns>
    bool CanDownloadContent(string[] fileKeys);

    /// <summary>
    /// returns true, when it is possible to update attributes of ALL of the given files
    /// </summary>
    /// <param name="fileKeys"></param>
    /// <returns></returns>
    bool CanUpdateAttributes(string[] fileKeys);

    #endregion

    /// <summary>
    /// Lists all files,
    /// processes sorting and paging and returns the keys of that files.
    /// </summary>
    /// <param name="sortingAttributeName"></param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    string[] ListAllFiles(string sortingAttributeName, int limit, int skip);

    /// <summary>
    /// Searches files by given attribute values to be used as filter,
    /// processes sorting and paging and returns the keys of that files.
    /// </summary>
    /// <param name="attributesToFilter">
    /// AND-linked (each given attribute-entry needs to match).
    /// In general, the usage of the '*' is allowed.
    /// For attributes of type 'AreaPath' the MINIMATCH/GLOB pattern is used (expressions like '**/abc*').
    /// This alles non-recurcive filtering for 1 level: 'my/area/*' will only return direct childs
    /// </param>
    /// <param name="sortingAttributeName"></param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    string[] SearchFilesByAttribute(Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip);

    /// <summary>
    /// Searches files by a given text/word tthat should be present within the content,
    /// applies also the given attribute values to be used as filter,
    /// processes sorting and paging and returns the keys of that files.
    /// </summary>
    /// <param name="textWithinContent">any text that should be present within the content of a file</param>
    /// <param name="attributesToFilter">
    /// AND-linked (each given attribute-entry needs to match).
    /// In general, the usage of the '*' is allowed.
    /// For attributes of type 'AreaPath' the MINIMATCH/GLOB pattern is used (expressions like '**/abc*').
    /// This alles non-recurcive filtering for 1 level: 'my/area/*' will only return direct childs
    /// </param>
    /// <param name="sortingAttributeName"></param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    string[] SearchFilesByContent(string textWithinContent, Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit, int skip);

    /// <summary>
    /// returns a list of entries (one per file), containing a given set of attributes.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, string>[] LoadFileAttributes(string[] fileKeys, string[] includedAttributeNames);

    bool CheckFileExists(string fileKey);

    /// <summary>
    /// Updates the attribute which represents the 'fileKey'.
    /// </summary>
    /// <param name="oldFileKey"></param>
    /// <param name="newFileKey"></param>
    /// <returns></returns>
    bool UpdateKey(string oldFileKey, string newFileKey);

    /// <summary>
    /// Updates the attributes for exaclty one file.
    /// All present attributes that are 'updatable' will be updated,
    /// the others will be ignored silently. Returns true on success.
    /// Important: the attribute which represents the 'fileKey' must always be present
    /// within the given set of attributes and will be used to addess the target file
    /// (if you want to update the key, youll need to use the 'UpdateKey' method).
    /// </summary>
    /// <param name="attributes"></param>
    /// <returns>null on failure, otherwise an dictionary containing new values for all
    /// attributes that ARE DIFFERENT to that set of attributes which were sended. So the caller will
    /// need to merge the changes into his local snapshot.
    /// This happens when:
    ///   1) a new value was revoked (because the attribute is not updatable / does not match the value range).
    ///   2) a new value was modified (because of re-formatting)
    ///   3) another attribute value, which was not expl. ment to be updated, was affected implicitely
    ///   (the 'filesize', the 'modificationDate' OR the KEY has changed)
    /// </returns>
    Dictionary<string, string> UpdateAttributes(Dictionary<string, string> attributes);

    /// <summary>
    /// returns an one-time-password (OTP) that is required for using the 'CreateNewFile' method.
    /// or returns null if the given attributes are incomplete / creation is not possible
    /// </summary>
    /// <param name="attributeValues">All attributes that are 'RequiredOnCreation' (can be enumerated using GetAvailableAttributes)</param>
    /// <returns></returns>
    string RequestOtpForNewFileCreation(Dictionary<string, string> attributeValues);

    /// <summary>
    /// returns an one-time-password (OTP) that is required for using the 'TryOverwriteFile' method
    /// or returns null if the given fileKey does not exist / overwrite is not possible
    /// </summary>
    /// <param name="fileKey"></param>
    /// <returns></returns>
    string RequestOtpForFileOverwrite(string fileKey);

    /// <summary>
    /// returns an one-time-password (OTP) that is required for using the 'DownloadFileContent' method
    /// or returns null if content download is not possible
    /// </summary>
    /// <param name="fileKey"></param>
    /// <returns></returns>
    string RequestOtpForDownloadContent(string fileKey);

    /// <summary>
    /// downloads the content of a file
    /// </summary>
    /// <param name="otp"></param>
    /// <returns></returns>
    byte[] DownloadFileContent(string otp);

    /// <summary>
    /// Uploads content for a new file and returns the key of the file on success.
    /// If the file could not be created the return value will be null.
    /// </summary>
    /// <param name="otp"></param>
    /// <param name="content"></param>
    /// <param name="newMimeType"></param>
    /// <returns></returns>
    string CreateNewFile(string otp, byte[] content, string newMimeType);

    /// <summary>
    /// Uploads new content for an exisiting file and returns true on success.
    /// If the file could not be updated/overwritten the return value will be false.
    /// </summary>
    /// <param name="otp"></param>
    /// <param name="content"></param>
    /// <param name="newMimeType"></param>
    /// <returns></returns>
    bool TryOverwriteFile(string otp, byte[] content, string newMimeType);

    /// <summary>
    /// deletes a file by its key and returns true on success
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool TryDeleteFile(string key);

    /// <summary>
    /// returns the bytes of an PNG image as thumbnail for the given fileKey.
    /// If thumbnails are not supported for a file-type (or in general - see 'CanLoadThumnails'), then null
    /// will be returned
    /// </summary>
    /// <param name="fileKey"></param>
    /// <param name="sizePx">
    /// Defines the preffered size for the longer side of the png image to return.
    /// </param>
    /// <param name="square">
    /// If set to ture, the given size will be met exactly (not just as max-size), so the reuslt will always be a square.
    /// Any free edges when having an recancle source will be part of the PNG, having an transparent aplha channel.
    /// </param>
    /// <returns></returns>
    byte[] LoadThumnail(string fileKey, int sizePx, bool square);

    #region " ManagedValueRange "

    /// <summary>
    /// returns an array of possible/allowed values of the given attribute or null,
    /// when the given attribute is not an 'ManagedValueRange' (see GetAvailableAttributes)
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="filter">
    /// In general, the usage of the '*' is allowed.
    /// For attributes of type 'AreaPath' the MINIMATCH/GLOB pattern is used (expressions like '**/abc*').
    /// This alles non-recurcive filtering for 1 level: 'my/area/*' will only return direct childs
    /// </param>
    /// <param name="isReadOnly"></param>
    /// <returns></returns>
    string[] GetValueRange(string attributeName, string filter, out bool isReadOnly);

    /// <summary>
    /// add an possible/allowed value to the 'ManagedValueRange' of the given attribute
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="valueToAdd"></param>
    /// <returns></returns>
    bool TryAddToValueRange(string attributeName, string valueToAdd);

    /// <summary>
    /// removes an possible/allowed value from the 'ManagedValueRange' of the given attribute
    /// </summary>
    /// <param name="attributeName"></param>
    /// <param name="valueToRemove"></param>
    /// <returns></returns>
    bool TryRemoveFromValueRange(string attributeName, string valueToRemove);

    #endregion

  }

}
