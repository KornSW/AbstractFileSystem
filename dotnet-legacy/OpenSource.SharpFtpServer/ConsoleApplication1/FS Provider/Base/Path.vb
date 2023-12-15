Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.Text

''' <summary>
''' Represents a path to an absolute position in a directory tree 
''' </summary>
<DebuggerDisplay("{DefaultString}")>
Public NotInheritable Class AbsolutePath
  Implements IEquatable(Of AbsolutePath)

#Region " Factories "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Shared _RootDirectory As AbsolutePath = Nothing

  ''' <summary>
  ''' Returns a singleton instance of a AbsolutePath representing the root directory in a directory tree
  ''' </summary>
  Public Shared ReadOnly Property RootDirectory As AbsolutePath
    Get
      If (_RootDirectory Is Nothing) Then
        _RootDirectory = AbsolutePath.Parse(IO.Path.DirectorySeparatorChar)
      End If
      Return _RootDirectory
    End Get
  End Property

  ''' <summary>
  ''' Parses an AbsolutePath path from a string using the default 'DirectorySeparatorChar' of the 'IO.Path' class
  ''' </summary>
  Public Shared Function Parse(pathString As String) As AbsolutePath
    Return Parse(pathString, IO.Path.DirectorySeparatorChar)
  End Function

  ''' <summary>
  ''' Parses an AbsolutePath path from a string using the sepcified 'DirectorySeparatorChar'
  ''' </summary>
  ''' <param name="directorySeparatorChar">This char will be used to split the path's directories</param>
  Public Shared Function Parse(pathString As String, directorySeparatorChar As Char) As AbsolutePath
    Dim parts As String()

    If (String.IsNullOrEmpty(pathString)) Then
      Return RootDirectory
    End If

    parts = RemoveEmptyParts(pathString.Split(directorySeparatorChar))

    If (parts(0) = _ParentDirectoryString OrElse parts(0) = _ThisDirectoryString) Then
      Throw New ArgumentException(String.Format("An AbsolutePath cannot start with '{0}', please use a 'RelativePath'!", parts(0)))
    End If

    Return New AbsolutePath(parts)
  End Function

#End Region

#Region " Constructor & Declarations "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Const _ThisDirectoryString = "."

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Const _ParentDirectoryString = ".."

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _Parts As String()

  Public Sub New(ParamArray parts As String())
    If parts Is Nothing Then
      _Parts = New String() {}
    Else
      _Parts = NormalizeParts(parts)
    End If
    Me.Elevation.CompareTo(0) 'triggers the check
  End Sub

  Private Sub New(parts As String(), append As String)
    Me.New(AppendPart(parts, append))
  End Sub

#End Region

#Region " Info "

  ''' <summary>
  ''' Returns the elevation of a path. Each subfolder increments the value, each navigation to a parentfolder decrements it.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public ReadOnly Property Elevation As Integer
    Get
      Dim elev As Integer = 0
      For i As Integer = 0 To _Parts.Length - 1
        Select Case _Parts(i)
          Case _ThisDirectoryString
          Case _ParentDirectoryString : elev -= 1
          Case Else : elev += 1
        End Select
        If (elev < 0) Then
          Throw New ApplicationException(String.Format("The path '{0}' is not absolute because it stepts out over the root directroy!", Me.CombinedString))
        End If
      Next
      Return elev
    End Get
  End Property

  Public ReadOnly Property Parts() As String()
    Get
      Return _Parts
    End Get
  End Property

  Public Function IsSubPathOf(path As AbsolutePath) As Boolean

    If (_Parts.Length < path.Parts.Length) Then
      Return False
    End If

    For i As Integer = 0 To path.Parts.Length - 1
      If (Not _Parts(i).ToLower() = path.Parts(i).ToLower()) Then
        Return False
      End If
    Next

    Return True
  End Function

  ''' <summary>
  ''' Returns the full path as one string.
  ''' The specified 'directorySeparatorChar' will be inserted between the directory names and appended at the end.
  ''' </summary>
  ''' <param name="directorySeparatorChar"></param>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Overloads Function ToString(directorySeparatorChar As Char) As String
    Dim sb As New StringBuilder

    If (_Parts.Length = 0) Then
      Return directorySeparatorChar
    End If

    For i As Integer = 0 To _Parts.Length
      sb.Append(_Parts(i))
      sb.Append(directorySeparatorChar)
    Next

    Return sb.ToString()
  End Function

  ''' <summary>
  ''' Returns the full path as one string.
  ''' The default 'directorySeparatorChar' of the System will be inserted between the directory names and appended at the end.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Overrides Function ToString() As String
    Return Me.ToString(Path.DirectorySeparatorChar)
  End Function

  ''' <summary>
  ''' Returns the full path as one string.
  ''' The default 'directorySeparatorChar' of the System will be inserted between the directory names and appended at the end.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public ReadOnly Property CombinedString
    Get
      Return Me.ToString()
    End Get
  End Property

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Overrides Function Equals(obj As Object) As Boolean
    If (TypeOf (obj) Is AbsolutePath) Then
      Return (DirectCast(obj, AbsolutePath).ToString().ToLower() = Me.ToString().ToLower())
    Else
      Return False
    End If
  End Function

  Private Function EqualsAbsolutePath(other As AbsolutePath) As Boolean Implements IEquatable(Of AbsolutePath).Equals
    Return Me.Equals(other)
  End Function

  Public Overrides Function GetHashCode() As Integer
    Return Me.ToString().ToLower().GetHashCode()
  End Function

#End Region

#Region " Navigation "

  Public Function GetParentDirectory(Optional steps As Integer = 1) As AbsolutePath
    If (Me.Equals(RootDirectory)) Then
      Throw New InvalidOperationException("The current path already represents the root directory")
    Else
      Select Case steps
        Case Is < 0 : Throw New ArgumentException("The param Steps must be >=0!", "steps")
        Case 0 : Return Me
        Case Else
          Dim directParent As AbsolutePath = New AbsolutePath(_Parts, _ParentDirectoryString)
          If steps = 1 Then
            Return directParent
          Else
            Return directParent.GetParentDirectory(steps - 1)
          End If
      End Select
    End If
  End Function

  Public Function GetSubDirectory(directoryName As String) As AbsolutePath
    Return New AbsolutePath(_Parts, directoryName)
  End Function

  Public Function Concat(path As RelativePath) As AbsolutePath
    Return New AbsolutePath(ConcatParts(_Parts, path.Parts))
  End Function

  Public Function DecreaseRoot(pathToRemove As AbsolutePath) As AbsolutePath
    If (Me.IsSubPathOf(pathToRemove)) Then
      Dim directoriesToRemove As Integer
      directoriesToRemove = pathToRemove.Elevation

      Select Case directoriesToRemove
        Case Is < 0
          Throw New ArgumentException("To decrease the root, the specified 'pathToRemove' mustnot be relative", "pathToRemove")
        Case Is > 0
          Dim upsteppingParts(directoriesToRemove - 1) As String
          For i As Integer = 0 To directoriesToRemove - 1
            upsteppingParts(i) = _ParentDirectoryString
          Next
          Return New AbsolutePath(ConcatParts(upsteppingParts, _Parts))
        Case Else
          Return Me
      End Select

    Else
      Throw New ArgumentException("To decrease the root, this instance have to be a sub path of the specified 'pathToRemove'", "pathToRemove")
    End If
  End Function

  Public Function IncreaseRoot(pathToPrepend As AbsolutePath) As AbsolutePath
    Return New AbsolutePath(ConcatParts(pathToPrepend.Parts, _Parts))
  End Function

#End Region

#Region " Operators "

  Public Shared Operator +(ByVal path1 As AbsolutePath, ByVal path2 As RelativePath) As AbsolutePath
    Return New AbsolutePath(ConcatParts(path1.Parts, path2.Parts))
  End Operator

  Public Shared Operator &(ByVal path1 As AbsolutePath, ByVal path2 As RelativePath) As AbsolutePath
    Return New AbsolutePath(ConcatParts(path1.Parts, path2.Parts))
  End Operator

  Public Shared Operator +(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As AbsolutePath
    Return New AbsolutePath(ConcatParts(path1.Parts, path2.Parts))
  End Operator

  Public Shared Operator &(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As AbsolutePath
    Return New AbsolutePath(ConcatParts(path1.Parts, path2.Parts))
  End Operator

  Public Shared Operator -(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As AbsolutePath
    Return path1.DecreaseRoot(path2)
  End Operator

  Public Shared Operator =(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As Boolean
    Return path1.Equals(path2)
  End Operator

  Public Shared Operator <>(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As Boolean
    Return Not path1.Equals(path2)
  End Operator

  Public Shared Operator <(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As Boolean
    Return path1.IsSubPathOf(path2)
  End Operator

  Public Shared Operator >(ByVal path1 As AbsolutePath, ByVal path2 As AbsolutePath) As Boolean
    Return path2.IsSubPathOf(path1)
  End Operator

#End Region

#Region " Helpers "

  Private Shared Function AppendPart(parts() As String, newPart As String) As String()
    Dim newLen As Integer = parts.Length + 1
    Dim newParts(newLen - 1) As String
    For i As Integer = 0 To parts.Length - 1
      newParts(i) = parts(i)
    Next
    newParts(newLen - 1) = newPart
    Return newParts
  End Function

  Private Shared Function NormalizeParts(parts As String()) As String()
    Dim processed As Boolean = False

    Do 'loop as long as we have not reachtet the begin of the path
      Dim upIdx As Integer = -1
      processed = True

      For i As Integer = parts.Length - 1 To 0 Step -1
        Select Case parts(i)
          Case _ThisDirectoryString : parts(i) = String.Empty
          Case _ParentDirectoryString : upIdx = i 'pick up navigations
          Case String.Empty
          Case Else
            If (upIdx > -1) Then 'and seach the down navigation, which can be removed
              parts(i) = String.Empty
              parts(upIdx) = String.Empty
              processed = False
              Exit For
            End If
        End Select
      Next

    Loop Until processed

    Return RemoveEmptyParts(parts)
  End Function

  Private Shared Function RemoveEmptyParts(parts As String()) As String()

    Dim i As Integer
    Dim count As Integer = 0

    For i = 0 To parts.Length - 1
      If (Not String.IsNullOrWhiteSpace(parts(i))) Then
        count += 1
      End If
    Next

    If (count = 0) Then
      Return New String() {}
    ElseIf (count = parts.Length) Then
      Return parts
    End If

    Dim newParts(count - 1) As String
    count = 0
    For i = 0 To parts.Length - 1
      If (Not String.IsNullOrWhiteSpace(parts(i))) Then
        newParts(count) = parts(i)
        count += 1
      End If
    Next

    Return newParts
  End Function

  Private Shared Function ConcatParts(parts1 As String(), parts2 As String()) As String()
    Dim i As Integer
    Dim finalLength As Integer = (parts1.Length + parts2.Length)
    Dim combined(finalLength - 1) As String

    For i = 0 To parts1.Length - 1
      combined(i) = parts1(i)
    Next
    For i = 0 To parts2.Length - 1
      combined(i + parts1.Length) = parts2(i)
    Next

    Return combined
  End Function

#End Region

End Class

''' <summary>
''' Represents a path to an relative position in a directory tree 
''' </summary>
<DebuggerDisplay("{DefaultString}")>
Public NotInheritable Class RelativePath
  Implements IEquatable(Of RelativePath)

#Region " Factories "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Shared _ThisDirectory As RelativePath = Nothing

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Shared _ParentDirectory As RelativePath = Nothing

  ''' <summary>
  ''' Returns a singleton instance of a RelativePath representing the current position in a directory tree
  ''' </summary>
  Public Shared ReadOnly Property ThisDirectory As RelativePath
    Get
      If (_ThisDirectory Is Nothing) Then
        _ThisDirectory = RelativePath.Parse(_ThisDirectoryString & IO.Path.DirectorySeparatorChar)
      End If
      Return _ThisDirectory
    End Get
  End Property

  ''' <summary>
  ''' Returns a singleton instance of a RelativePath representing the parent directory of the current position in a directory tree
  ''' </summary>
  Public Shared ReadOnly Property ParentDirectory As RelativePath
    Get
      If (_ParentDirectory Is Nothing) Then
        _ParentDirectory = RelativePath.Parse(_ParentDirectoryString, Path.DirectorySeparatorChar)
      End If
      Return _ParentDirectory
    End Get
  End Property

  ''' <summary>
  ''' Parses an AbsolutePath path from a string using the default 'DirectorySeparatorChar' of the 'IO.Path' class
  ''' </summary>
  Public Shared Function Parse(pathString As String) As RelativePath
    Return Parse(pathString, IO.Path.DirectorySeparatorChar)
  End Function

  ''' <summary>
  ''' Parses an AbsolutePath path from a string using the sepcified 'DirectorySeparatorChar'
  ''' </summary>
  ''' <param name="directorySeparatorChar">This char will be used to split the path's directories</param>
  Public Shared Function Parse(pathString As String, directorySeparatorChar As Char) As RelativePath
    Dim parts As String()

    If (String.IsNullOrEmpty(pathString)) Then
      Return ThisDirectory
    End If

    If (pathString.StartsWith(directorySeparatorChar)) Then
      pathString = _ThisDirectoryString & pathString
    End If

    parts = RemoveEmptyParts(pathString.Split(directorySeparatorChar))

    Return New RelativePath(parts)
  End Function

#End Region

#Region " Constructor & Declarations "

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Const _ThisDirectoryString = "."

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private Const _ParentDirectoryString = ".."

  <DebuggerBrowsable(DebuggerBrowsableState.Never)>
  Private _Parts As String()

  Public Sub New(ParamArray parts As String())
    If parts Is Nothing Then
      _Parts = New String() {}
    Else
      _Parts = NormalizeParts(parts)
    End If
  End Sub

  Private Sub New(parts As String(), append As String)
    Me.New(AppendPart(parts, append))
  End Sub

#End Region

#Region " Info "

  ''' <summary>
  ''' Returns the elevation of a path. Each subfolder increments the value, each navigation to a parentfolder decrements it.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public ReadOnly Property Elevation As Integer
    Get
      Dim elev As Integer = 0
      For i As Integer = 0 To _Parts.Length - 1
        Select Case _Parts(i)
          Case _ThisDirectoryString
          Case _ParentDirectoryString : elev -= 1
          Case Else : elev += 1
        End Select
      Next
      Return elev
    End Get
  End Property

  Public ReadOnly Property Parts() As String()
    Get
      Return _Parts
    End Get
  End Property

  ''' <summary>
  ''' Returns the full path as one string.
  ''' The specified 'directorySeparatorChar' will be inserted between the directory names and appended at the end.
  ''' </summary>
  ''' <param name="directorySeparatorChar"></param>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Overloads Function ToString(directorySeparatorChar As Char) As String
    Dim sb As New StringBuilder

    If (_Parts.Length = 0 OrElse Not _Parts(0) = _ParentDirectoryString) Then
      sb.Append(_ThisDirectoryString)
      sb.Append(directorySeparatorChar)
    End If

    For i As Integer = 0 To _Parts.Length
      sb.Append(_Parts(i))
      sb.Append(directorySeparatorChar)
    Next

    Return sb.ToString()
  End Function

  ''' <summary>
  ''' Returns the full path as one string.
  ''' The default 'directorySeparatorChar' of the System will be inserted between the directory names and appended at the end.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Overrides Function ToString() As String
    Return Me.ToString(Path.DirectorySeparatorChar)
  End Function

  ''' <summary>
  ''' Returns the full path as one string.
  ''' The default 'directorySeparatorChar' of the System will be inserted between the directory names and appended at the end.
  ''' </summary>
  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public ReadOnly Property CombinedString
    Get
      Return Me.ToString()
    End Get
  End Property

  <EditorBrowsable(EditorBrowsableState.Advanced)>
  Public Overrides Function Equals(obj As Object) As Boolean
    If (TypeOf (obj) Is RelativePath) Then
      Return (DirectCast(obj, RelativePath).ToString().ToLower() = Me.ToString().ToLower())
    Else
      Return False
    End If
  End Function

  Private Function EqualsRelativePath(other As RelativePath) As Boolean Implements IEquatable(Of RelativePath).Equals
    Return Me.Equals(other)
  End Function

  Public Overrides Function GetHashCode() As Integer
    Return Me.ToString().ToLower().GetHashCode()
  End Function

#End Region

#Region " Navigation "

  Public Function GetParentDirectory(Optional steps As Integer = 1) As RelativePath
    Select Case steps
      Case Is < 0 : Throw New ArgumentException("The param Steps must be >=0!", "steps")
      Case 0 : Return Me
      Case Else
        Dim directParent As RelativePath = New RelativePath(_Parts, _ParentDirectoryString)
        If steps = 1 Then
          Return directParent
        Else
          Return directParent.GetParentDirectory(steps - 1)
        End If
    End Select
  End Function

  Public Function GetSubDirectory(directoryName As String) As RelativePath
    Return New RelativePath(_Parts, directoryName)
  End Function

  Public Function Concat(path As RelativePath) As RelativePath
    Return New RelativePath(ConcatParts(_Parts, path.Parts))
  End Function

  Public Function ToAbsolutePath(anchorPoint As AbsolutePath) As AbsolutePath
    Return anchorPoint & Me
  End Function

#End Region

#Region " Operators "

  Public Shared Operator +(ByVal path1 As RelativePath, ByVal path2 As RelativePath) As RelativePath
    Return New RelativePath(ConcatParts(path1.Parts, path2.Parts))
  End Operator

  Public Shared Operator &(ByVal path1 As RelativePath, ByVal path2 As RelativePath) As RelativePath
    Return New RelativePath(ConcatParts(path1.Parts, path2.Parts))
  End Operator

  Public Shared Operator =(ByVal path1 As RelativePath, ByVal path2 As RelativePath) As Boolean
    Return path1.Equals(path2)
  End Operator

  Public Shared Operator <>(ByVal path1 As RelativePath, ByVal path2 As RelativePath) As Boolean
    Return Not path1.Equals(path2)
  End Operator

#End Region

#Region " Helpers "

  Private Shared Function AppendPart(parts() As String, newPart As String) As String()
    Dim newLen As Integer = parts.Length + 1
    Dim newParts(newLen - 1) As String
    For i As Integer = 0 To parts.Length - 1
      newParts(i) = parts(i)
    Next
    newParts(newLen - 1) = newPart
    Return newParts
  End Function

  Private Shared Function NormalizeParts(parts As String()) As String()
    Dim processed As Boolean = False

    Do 'loop as long as we have not reachtet the begin of the path
      Dim upIdx As Integer = -1
      processed = True

      For i As Integer = parts.Length - 1 To 0 Step -1
        Select Case parts(i)
          Case _ThisDirectoryString : parts(i) = String.Empty
          Case _ParentDirectoryString : upIdx = i 'pick up navigations
          Case String.Empty
          Case Else
            If (upIdx > -1) Then 'and seach the down navigation, which can be removed
              parts(i) = String.Empty
              parts(upIdx) = String.Empty
              processed = False
              Exit For
            End If
        End Select
      Next

    Loop Until processed

    Return RemoveEmptyParts(parts)
  End Function

  Private Shared Function RemoveEmptyParts(parts As String()) As String()

    Dim i As Integer
    Dim count As Integer = 0

    For i = 0 To parts.Length - 1
      If (Not String.IsNullOrWhiteSpace(parts(i))) Then
        count += 1
      End If
    Next

    If (count = 0) Then
      Return New String() {}
    ElseIf (count = parts.Length) Then
      Return parts
    End If

    Dim newParts(count - 1) As String
    count = 0
    For i = 0 To parts.Length - 1
      If (Not String.IsNullOrWhiteSpace(parts(i))) Then
        newParts(count) = parts(i)
        count += 1
      End If
    Next

    Return newParts
  End Function

  Private Shared Function ConcatParts(parts1 As String(), parts2 As String()) As String()
    Dim i As Integer
    Dim finalLength As Integer = (parts1.Length + parts2.Length)
    Dim combined(finalLength - 1) As String

    For i = 0 To parts1.Length - 1
      combined(i) = parts1(i)
    Next
    For i = 0 To parts2.Length - 1
      combined(i + parts1.Length) = parts2(i)
    Next

    Return combined
  End Function

#End Region

End Class
