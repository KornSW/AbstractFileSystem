using System.Collections.Generic;

namespace System.IO.Abstraction {

  public interface IAfsRepository {

    /// <summary>
    /// Returns an string, representing the "Identity" of the current origin.
    /// This can be used to discriminate multiple source repos (for usecases like caching)
    /// </summary>
    string GetOriginIdentity();

    /// <summary>
    /// Returns an property bag which holds information about the implemented/supported capabilities of this IAfsRepository.
    /// </summary>
    /// <returns></returns>
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
    /// <param name="sortingAttributeName">can contain the prefix ^ for desc-order</param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    string[] ListAllFiles(string sortingAttributeName, int limit = 100, int skip = 0);

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
    /// <param name="sortingAttributeName">can contain the prefix ^ for desc-order</param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    string[] SearchFilesByAttribute(Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit = 100, int skip = 0);

    /// <summary>
    /// Searches files by a given text/word that should be present within the content,
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
    /// <param name="sortingAttributeName">can contain the prefix ^ for desc-order</param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    string[] SearchFilesByContent(string textWithinContent, Dictionary<string, string> attributesToFilter, string sortingAttributeName, int limit = 100, int skip = 0);

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
    /// Initiates the Content-Access-Operation to create a new File.
    /// Therefore an one-time-password ('contentPushOtp') will be returned, which is required for using the 'TryPushFileContent' method.
    /// An empty file will be created immediately when calling this method.
    /// As long as the 'TryPushFileContent' hasnt been called with willContinuePush=false (or 'ContentOperationComplete'),
    /// the Content-Access-Operation will represent an 'lock' which prohibits any write or read access to that file for other parties.
    /// </summary>
    /// <param name="attributeValues">All attributes that are 'RequiredOnCreation' (can be enumerated using GetAvailableAttributes)</param>
    /// <param name="contentType">The Content-Type (also known as MIME-Type) of the content that will be pushed.</param>
    /// <param name="contentPushOtp">Returns a one-time-password for calling 'TryPushFileContent'</param>
    /// <param name="intendedFileKeyOnSuccess">Returns the fileKey under which the file will be accessable when it has been written.</param>
    /// <returns>Returns true on success, or false if the operation is not permitted.</returns>
    bool TryBeginCreateNewFile(Dictionary<string, string> attributeValues, string contentType, out string contentPushOtp, out string intendedFileKeyOnSuccess);

    /// <summary>
    /// Initiates the Content-Access-Operation to overwrite an exisiting File.
    /// Therefore an one-time-password ('contentPushOtp') will be returned, which is required for using the 'TryPushFileContent' method.
    /// The target file will be truncated immediately when calling this method.
    /// As long as the 'TryPushFileContent' hasnt been called with willContinuePush=false (or 'ContentOperationComplete'),
    /// the Content-Access-Operation will represent an 'lock' which prohibits any write or read access to that file for other parties.
    /// </summary>
    /// <param name="fileKey">The key of the target file for which the content should be overwritten</param>
    /// <param name="contentType">The Content-Type (also known as MIME-Type) of the content that will be pushed.</param>
    /// <param name="contentPushOtp">Returns a one-time-password for calling 'TryPushFileContent'</param>
    /// <returns>Returns true on success, or false if the operation is not permitted, the file does not exist or the file is currently locked by another Content-Access-Operation.</returns>
    bool TryBeginOverwriteFile(string fileKey, string contentType, out string contentPushOtp);

    /// <summary>
    /// Initiates the Content-Access-Operation to append additional content for an exisiting File.
    /// Therefore an one-time-password ('contentPushOtp') will be returned, which is required for using the 'TryPushFileContent' method.
    /// As long as the 'TryPushFileContent' hasnt been called with willContinuePush=false (or 'ContentOperationComplete'),
    /// the Content-Access-Operation will represent an 'lock' which prohibits any write or read access to that file for other parties.
    /// </summary>
    /// <param name="fileKey">The key of the target file for which the content should be appended</param>
    /// <param name="contentPushOtp">Returns a one-time-password for calling 'TryPushFileContent'</param>
    /// <returns>Returns true on success, or false if the operation is not permitted, the file does not exist or the file is currently locked by another Content-Access-Operation.</returns>
    bool TryBeginAppendFileContent(string fileKey, out string contentPushOtp);

    /// <summary>
    /// Initiates the Content-Access-Operation to load the contents of a file.
    /// Therefore an one-time-password ('contentPullOtp') will be returned, which is required for using the 'TryPullFileContent' method.
    /// As long as the 'TryPullFileContent' hasnt been called with willContinuePull=false (or 'ContentOperationComplete'),
    /// the Content-Access-Operation will represent an 'lock' which prohibits any write access to that file for other parties.
    /// </summary>
    /// <param name="fileKey">The key of the file to be accessed.</param>
    /// <param name="contentType">Returns the Content-Type (also known as MIME-Type) of this file.</param>
    /// <param name="contentSizeBytes">Return the size (number of bytes) of the file content.</param>
    /// <param name="contentPullOtp">Returns a one-time-password for calling 'TryPullFileContent'</param>
    /// <returns>Returns true on success, or false if the operation is not permitted, the file does not exist or the file is currently locked by another Content-Access-Operation.</returns>
    bool TryBeginPullFileContent(string fileKey, out string contentType, out long contentSizeBytes, out string contentPullOtp);

    /// <summary>
    /// This method will release exisiting file locks fo not yet completet Content-Access-Operations.
    /// It should be call when having retrieved an 'contentPushOtp' or 'contentPullOtp' which wont be used anymore.
    /// </summary>
    /// <param name="contentPushOrPullOtp">The current OTP (one-time-password) which should be revoked.</param>
    void ContentOperationComplete(string contentPushOrPullOtp);

    /// <summary>
    /// Pushes content (= a block of bytes) into the file which is addressed implicitely via 'contentPushOtp' (Handle). 
    /// </summary>
    /// <param name="contentPushOtp">
    /// The current OTP (one-time-password) which has been retrieved by calling
    /// 'TryBeginCreateNewFile', 'TryBeginOverwriteFile' or 'TryBeginAppendFileContent'.
    /// </param>
    /// <param name="content">the new content, to be appended</param>
    /// <param name="willContinuePush">
    /// IMPORTANT: when providing false, the Content-Access-Operation will be completed automatically.
    /// (like calling the 'ContentOperationComplete' method). You should provide true, if another call
    /// of the 'TryPushFileContent' will follow immediately - this will keep the file-lock AND assign
    /// a mew 'contentPushOtp' over the by-ref param (this can easy be called from via loop).
    /// </param>
    /// <returns>Will return false, if the OTP (one-time-password) has expired or was already used</returns>
    bool TryPushFileContent(ref string contentPushOtp, byte[] content, bool willContinuePush);

    /// <summary>
    /// Pulls content (= a block of bytes) from the file which is addressed implicitely via 'contentPullOtp' (Handle). 
    /// </summary>
    /// <param name="contentPullOtp">
    /// The current OTP (one-time-password) which has been retrieved by calling 'TryBeginPullFileContent'.
    /// </param>
    /// <param name="content">Returns the retrieved block of content</param>
    /// <param name="willContinuePull">
    /// IMPORTANT: when providing false, the Content-Access-Operation will be completed automatically.
    /// (like calling the 'ContentOperationComplete' method). You should provide true, if another call
    /// of the 'TryPullFileContent' will follow immediately - this will keep the file-lock AND assign
    /// a mew 'contentPullOtp' over the by-ref param (this can easy be called from via loop).
    /// </param>
    /// <param name="byteOffset">Start-postion (0 based index) to address that block of bytes to request</param>
    /// <param name="byteCount">Number of bytes (=size of the block) to request.
    /// To retrieve the whole content within one request, just provide the 'contentSizeBytes' from the 'TryBeginPullFileContent'-call.</param>
    /// <returns>Will return false, if the OTP (one-time-password) has expired or was already used</returns>
    bool TryPullFileContent(ref string contentPullOtp, out byte[] content, bool willContinuePull, long byteOffset, long byteCount);

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
